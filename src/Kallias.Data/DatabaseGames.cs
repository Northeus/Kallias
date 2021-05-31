using System.Collections.Generic;
using Discord.Rest;
using Kallias.Game;

namespace Kallias.Data
{
    internal static class DatabaseGames
    {
        private static Dictionary<ulong, GameContext> _database = new ();

        public static bool TryGet(ulong messageId, out GameContext gameContext)
            => _database.TryGetValue(messageId, out gameContext);

        public static void Insert(ulong messageId, IGame game, RestUserMessage message, ulong authorId)
            => _database[messageId] = new GameContext(game, message, authorId);

        public static bool Contains(ulong messageId)
            => _database.ContainsKey(messageId);
    }
}