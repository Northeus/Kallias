using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kallias.Data
{
    public static class DatabaseEmotes<TKey> where TKey : Enum
    {
        private static readonly string _databasePath =
            Assembly.GetExecutingAssembly().Location
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}Assets{Path.DirectorySeparatorChar}emotes.json";

        private static Dictionary<TKey, string> _database;

        public static Dictionary<TKey, string> Database
        {
            get => (_database ??= LoadDatabase()); 
            
            set => _database = value;
        }

        public static bool TryGet(TKey key, out string emoji)
            => Database.TryGetValue(key, out emoji);

        private static Dictionary<TKey, string> LoadDatabase()
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_databasePath))
                .ToDictionary(
                    kvp => (TKey) Enum.Parse(typeof(TKey), kvp.Key, true),
                    kvp => kvp.Value
                );
    }
}