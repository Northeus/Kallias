using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kallias.Data
{
    public static class DatabaseEmotes<TKey> where TKey : Enum
    {
        private static Dictionary<TKey, string> _database;

        /// <summary>
        /// Get basic database from file "Assets/emotes.json", which use enum names as keys.
        /// There might be some exceptions in case of missing file!
        /// </summary>
        public static Dictionary<TKey, string> Database
        {
            get => (_database ??= LoadDatabase(Paths.SourceDir + "Assets/emotes.json" )); 
            
            set => _database = value;
        }

        /// <summary>
        /// Attempt to get value from database.
        /// </summary>
        /// <param name="key">Key specifying find given item.</param>
        /// <param name="emoji">String stored under given key if found, null otherwise.</param>
        /// <returns>True, if given key held any entry, false otherwise.</returns>
        public static bool TryGet(TKey key, out string emoji)
            => Database.TryGetValue(key, out emoji);

        /// <summary>
        /// Load <c>database</c> from given path and return it as dictionary.
        /// Be aware of exceptions and errors in case of wrong path or file.
        /// </summary>
        /// <param name="path">Path specifying json file with data to load.</param>
        /// <returns>Database as dictionary from given file, if everything was fine.</returns>
        public static Dictionary<TKey, string> LoadDatabase(string path)
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path))
                .ToDictionary(
                    kvp => (TKey) Enum.Parse(typeof(TKey), kvp.Key, true),
                    kvp => kvp.Value
                );
    }
}