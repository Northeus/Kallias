using System.Collections.Generic;
using Discord.Rest;
using Kallias.Game;

namespace Kallias.Data
{
    /// <summary>
    /// Class which store every game currently played.
    /// </summary>
    internal static class DatabaseGames
    {
        private static readonly Dictionary<ulong, GameContext> Database = new ();

        /// <summary>
        /// Attempt to load game from database.
        /// </summary>
        /// <param name="messageId">Id of message, which should handle game.</param>
        /// <param name="gameContext">Game context for given message, if exists.</param>
        /// <returns>True, if message container context, which was returned.</returns>
        public static bool TryGet(ulong messageId, out GameContext gameContext)
            => Database.TryGetValue(messageId, out gameContext);

        public static void Insert(ulong messageId, IGame game, RestUserMessage message, ulong authorId)
            => Database[messageId] = new GameContext(game, message, authorId);

        public static bool Contains(ulong messageId)
            => Database.ContainsKey(messageId);
    }
}