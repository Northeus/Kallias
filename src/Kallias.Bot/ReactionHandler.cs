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

        private Task HandleReactionAsync(
            Cacheable<IUserMessage, ulong> messageCached, ISocketMessageChannel channel, SocketReaction reaction
        )
        {
            if (! ShouldProcess(messageCached, reaction))
            {
                return Task.CompletedTask;
            }
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(async delegate(object state)
            {
                await GameFactory.Instance.ProcessReactionAsync(messageCached.Id, reaction);

                DatabaseGames.TryGet(messageCached.Id, out var gameContext);

                await gameContext.Message.RemoveReactionAsync(reaction.Emote, reaction.UserId);
            }), null);

            return Task.CompletedTask;
        }

        private bool IsInitialReaction(SocketReaction reaction)
            => reaction.UserId == _client.CurrentUser.Id;

        private bool ShouldProcess(Cacheable<IUserMessage, ulong> messageCached, SocketReaction reaction)
            => ! IsInitialReaction(reaction) && DatabaseGames.Contains(messageCached.Id);
    }
}
