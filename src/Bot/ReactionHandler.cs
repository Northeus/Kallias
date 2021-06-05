using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Kallias.Data;
using Kallias.Game;

namespace Kallias.Bot
{
    /// <summary>
    /// Handle every incoming reaction and in case
    /// of need send it to game as a new move.
    /// </summary>
    internal class ReactionHandler
    {
        private readonly DiscordSocketClient _client;

        /// <summary>
        /// Create new handler for reactions with bound handler.
        /// </summary>
        /// <param name="client">Handler which is receiving reactions.</param>
        public ReactionHandler(DiscordSocketClient client)
            => _client = client;

        /// <summary>
        /// Bind reaction handler to client events handler for incoming reaction.
        /// </summary>
        public void SetUp()
            =>_client.ReactionAdded += HandleReactionAsync;

        /// <summary>
        /// Handle incoming reaction, check whether it belong to message used as game
        /// and process it in that case.
        /// </summary>
        /// <param name="messageCached">Cached message which got message.</param>
        /// <param name="channel">Chanel, in which was reaction added.</param>
        /// <param name="reaction">Reaction received.</param>
        private Task HandleReactionAsync(
            Cacheable<IUserMessage, ulong> messageCached, ISocketMessageChannel channel, SocketReaction reaction
        )
        {
            if (! ShouldProcess(messageCached, reaction))
            {
                return Task.CompletedTask;
            }

            ThreadPool.QueueUserWorkItem(async delegate
            {
                await GameFactory.Instance.ProcessReactionAsync(messageCached.Id, reaction);

                DatabaseGames.TryGet(messageCached.Id, out var gameContext);

                await gameContext!.Message.RemoveReactionAsync(reaction.Emote, reaction.UserId);
            });

            return Task.CompletedTask;
        }

        private bool IsInitialReaction(SocketReaction reaction)
            => reaction.UserId == _client.CurrentUser.Id;

        private bool ShouldProcess(Cacheable<IUserMessage, ulong> messageCached, SocketReaction reaction)
            => ! IsInitialReaction(reaction) && DatabaseGames.Contains(messageCached.Id);
    }
}
