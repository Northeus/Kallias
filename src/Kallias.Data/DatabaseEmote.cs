using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kallias.Data
{
    public static class DatabaseEmote<T> where T : Enum
    {
        private static readonly string _databasePath =
            Assembly.GetExecutingAssembly().Location
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}Assets{Path.DirectorySeparatorChar}emotes.json";

        private static Dictionary<T, string> _database;

        public static Dictionary<T, string> Database
        {
            get => (_database ??= LoadDatabase()); 
            
            set => _database = value;
        }

        private static Dictionary<T, string> LoadDatabase()
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_databasePath))
                .ToDictionary(
                    kvp => (T) Enum.Parse(typeof(T), kvp.Key, true),
                    kvp => kvp.Value
                );
    }
}