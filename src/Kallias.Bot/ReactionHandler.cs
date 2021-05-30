using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kallias.Data;
using Kallias.Game;

namespace Kallias.Bot
{
    internal class ReactionHandler
    {
        private readonly DiscordSocketClient _client;

        public ReactionHandler(DiscordSocketClient client)
            => _client = client;

        public void SetUp()
            =>_client.ReactionAdded += HandleReactionAsync;

        private async Task HandleReactionAsync(
            Cacheable<IUserMessage, ulong> messageCached, ISocketMessageChannel channel, SocketReaction reaction
        )
        {
            var message = await GetMessageAsync(messageCached, channel);

            if (! ShouldProcess(message, reaction))
            {
                return;
            }
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(async delegate(object state)
            {
                await GameFactory.Instance.ProcessReactionAsync(message.Id, reaction);

                await message.RemoveReactionAsync(reaction.Emote, reaction.UserId);
            }), null);
        }

        private static bool IsHandledGame(IMessage message)
            => message != null && DatabaseGames.TryGet(message.Id, out _);

        private static async Task<IMessage> GetMessageAsync(
            Cacheable<IUserMessage, ulong> messageCached, ISocketMessageChannel channel
        ) => messageCached.HasValue
                ? messageCached.Value
                : await channel.GetMessageAsync(messageCached.Id);

        private bool IsInitialReaction(SocketReaction reaction)
            => reaction.UserId == _client.CurrentUser.Id;

        private bool ShouldProcess(IMessage message, SocketReaction reaction)
            => IsHandledGame(message) && ! IsInitialReaction(reaction);
    }
}
