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

        private IGame _game;

        public Bot(IGame game)
            => _game = game;

        public async Task StartUp()
        {
            var client = new DiscordSocketClient();
            var commandService = new CommandService();

            client.Log += Log;

            var token = File.ReadAllText(TokenPath);

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            var commandHandler = new CommandHandler(client, commandService);
            
            await commandHandler.SetUp();

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