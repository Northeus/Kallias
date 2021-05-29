using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kallias.Data
{
    public static class DatabaseEmote
    {
        public static readonly string _databasePath = //TODO make private
            Assembly.GetExecutingAssembly().Location
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
            + $"{Path.DirectorySeparatorChar}Assets{Path.DirectorySeparatorChar}emotes.json";

        private static Dictionary<Emote, string> _database;

        public static Dictionary<Emote, string> Database
        {
            get => (_database ??= LoadDatabase()); 
            
            set => _database = value;
        }

        private static Dictionary<Emote, string> LoadDatabase()
            => JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_databasePath))
                .ToDictionary(
                    kvp => (Emote) Enum.Parse(typeof(Emote), kvp.Key, true),
                    kvp => kvp.Value
                );
    }
}