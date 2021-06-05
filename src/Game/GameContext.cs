using System.Threading;
using Discord.Rest;

namespace Kallias.Game
{
    /// <summary>
    /// Cass which bind message, game and author (player) to single entity.
    /// </summary>
    internal class GameContext
    {
        private int _locked;

        /// <summary>
        /// Create new context with bound game, message and author (player).
        /// </summary>
        /// <param name="game">New game to be played.</param>
        /// <param name="message">Message, which will handle rendering.</param>
        /// <param name="authorId">Author of command, which created game.</param>
        public GameContext(IGame game, RestUserMessage message, ulong authorId)
            => (Game, Message, AuthorId) = (game, message, authorId);

        /// <summary>
        /// Store game for given context.
        /// </summary>
        public IGame Game { get; }

        /// <summary>
        /// Store message, which handle rendering for given context.
        /// </summary>
        public RestUserMessage Message { get; }

        /// <summary>
        /// Store author (player) for given context.
        /// </summary>
        public ulong AuthorId { get; }

        /// <summary>
        /// Attempt to lock context, so only one thread may work with it.
        /// </summary>
        /// <remarks>
        /// Don't forget to unlock locked context via method <c>Unlock</c> afterwards.
        /// </remarks>
        /// <returns>Returns true, if context was successfully locked.</returns>
        public bool TryLock()
            => Interlocked.Exchange(ref _locked, 1) == 0;

        /// <summary>
        /// Unlock locked context.
        /// </summary>
        public void Unlock()
            => _locked = 0;
    }
}