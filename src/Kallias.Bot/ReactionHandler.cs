using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

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
            var message = messageCached.HasValue
                        ? messageCached.Value
                        : await channel.GetMessageAsync(messageCached.Id);

            if (! IsHandledGame(message)
                || IsInitialReaction(reaction)
            )
            {
                return;
            }

            await message.RemoveReactionAsync(reaction.Emote, reaction.UserId);
        }

        private static bool IsHandledGame(IMessage message)
            => message != null && DatabaseGames.TryGet(message.Id, out _);

        private bool IsInitialReaction(SocketReaction reaction)
            => reaction.UserId == _client.CurrentUser.Id;
    }
}
