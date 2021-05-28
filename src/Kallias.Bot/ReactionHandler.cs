using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Kallias.Bot
{
    public internal class ReactionHandler
    {
        private readonly DiscordSocketClient _client;

        public ReactionHandler(DiscordSocketClient client)
            => _client = client;

        public void SetUp()
            =>_client.ReactionAdded += HandleReactionAsync;

        private async Task HandleReactionAsync(
            Cacheable<IUserMessage, ulong> message, ISocketMessageChannel channel, SocketReaction reaction
        )
        {
            // Don't process the command if it was a system message
            var     msg = message.HasValue
                        ? message.Value
                        : await channel.GetMessageAsync(message.Id);

            // TODO process only if message is game

/*
            if (msg == null
                || msg.Author.Id != _client.CurrentUser.Id
                || channel.Name != Channels.Events
                || reaction.Emote.Name != _reactionEmote.Name
            )
            {
                return;
            }
*/
            await msg.RemoveReactionAsync(reaction.Emote, reaction.UserId);
        }
    }
}
