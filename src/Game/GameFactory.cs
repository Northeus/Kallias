using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Kallias.Data;

namespace Kallias.Game
{
    /// <summary>
    /// Factory which handle creating new game contexts.
    /// </summary>
    internal class GameFactory
    {
        private static GameFactory _instance;

        /// <summary>
        /// Hide constructor.
        /// </summary>
        private GameFactory()
        {

        }

        /// <summary>
        /// Get instance of factory (singleton).
        /// </summary>
        public static GameFactory Instance
            => (_instance ??= new GameFactory());

        /// <summary>
        /// Store Game instance, which can be used for playing.
        /// </summary>
        public IGame Game { get; set; }

        /// <summary>
        /// Create new game instance.
        /// </summary>
        /// <returns>New game instance.</returns>
        public IGame CreateGame()
            => (IGame) Game.Clone();
        
        /// <summary>
        /// Process reaction as move for game context under given id of message.
        /// Only author of game can make moves.
        /// </summary>
        /// <param name="messageId">Message, which got reaction.</param>
        /// <param name="reaction">Reaction for message.</param>
        public async Task ProcessReactionAsync(ulong messageId, SocketReaction reaction)
        {
            var move = FindMove(reaction.Emote);

            DatabaseGames.TryGet(messageId, out var gameContext);

            if (move == null || gameContext.AuthorId != reaction.UserId || gameContext.TryLock() != true)
            {
                return;
            }

            var (game, message) = (gameContext.Game, gameContext.Message);

            game.MakeMove(move);

            await message.ModifyAsync(msg => msg.Content = (string) game.Render());

            gameContext.Unlock();
        }
        
        private Move FindMove(IEmote emote)
            => Game.Moves
                .FirstOrDefault(move => move.Emote.Name == emote.Name);
    }
}