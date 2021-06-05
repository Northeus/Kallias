using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Kallias.Game;
using Discord;
using Discord.WebSocket;

namespace Kallias.Bot
{
    /// <summary>
    /// Main class, which will read token from your directory, where is initially generated .csproj file.
    /// Also will bind game for further use in bot.
    /// </summary>
    public class Bot
    {
        private static readonly string TokenPath = Assembly.GetExecutingAssembly().Location
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}"
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}"
            + "token.txt";

        /// <summary>
        /// Create new instance of bot with bound game.
        /// </summary>
        /// <param name="game">Game, which will be played.</param>
        public Bot(IGame game)
            => GameFactory.Instance.Game = game;

        /// <summary>
        /// Load resources and start bot. function will be held on.
        /// In case of any problems, check again if you have correct
        /// token file in your initial source code directory.
        /// </summary>
        public async Task StartUp()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;

            var token = await File.ReadAllTextAsync(TokenPath);

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            var commandHandler = new CommandHandler(client);
            
            commandHandler.SetUp();

            var reactionHandler = new ReactionHandler(client);
            
            reactionHandler.SetUp();

            await Task.Delay(-1);
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());

            return Task.CompletedTask;
        }
    }
}