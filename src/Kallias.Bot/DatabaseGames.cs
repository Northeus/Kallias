using System.Collections.Generic;
using Kallias.Game;

namespace Kallias.Bot
{
    public static class DatabaseGames
    {
        private static Dictionary<ulong, IGame> _database = new ();

        public static bool TryGet(ulong id, out IGame game)
            => _database.TryGetValue(id, out game);

        public static void Insert(ulong id, IGame game)
            => _database[id] = game;
    }
}