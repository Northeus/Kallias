using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Kallias.Game;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Kallias.Bot
{
    public class Bot
    {
        private static readonly string TokenPath = Assembly.GetExecutingAssembly().Location
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}"
            + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}"
            + "token.txt";

        public Bot(IGame game)
            => GameFactory.Instance.Game = game;

        public async Task StartUp()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;

            var token = File.ReadAllText(TokenPath);

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