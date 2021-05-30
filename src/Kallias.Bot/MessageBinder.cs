using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Kallias.Game;
using Kallias.Data;

namespace Kallias.Bot {
    internal class CommandHandler
    {
        private static readonly Regex CommandValidator = new Regex(@"^\.new(\s.*)?$");

        private readonly DiscordSocketClient _client;

        public CommandHandler(DiscordSocketClient client)
            => _client = client;
        
        public void SetUp()
            => _client.MessageReceived += HandleCommand;

        private Task HandleCommand(SocketMessage message)
        {
            if (IsSystemMessage(message)
                || ! IsCreateCommand(message))
            {
                return Task.CompletedTask;
            }

            ProcessCommand(message);

            return Task.CompletedTask;
        }

        private static bool IsSystemMessage(SocketMessage message)
            => (message as SocketUserMessage) == null;

        private static bool IsCreateCommand(SocketMessage message)
            => CommandValidator.IsMatch(message.Content);

        private static bool ProcessCommand(SocketMessage message)
            => ThreadPool.QueueUserWorkItem(new WaitCallback(async delegate(object state)
            {
                await CreateNewGameAsync(message);

                await message.DeleteAsync();
            }), null);

        private static async Task CreateNewGameAsync(SocketMessage messageCommand)
        {
            var game = GameFactory.Instance.CreateGame();

            var messageGame = await messageCommand.Channel.SendMessageAsync((string) game.Render());

            await AddReactionsAsync(messageGame, game);

            DatabaseGames.Insert(
                messageGame.Id,
                game,
                messageGame
            );
        } 

        // Ensure order of reactions!
        private static async Task AddReactionsAsync(RestUserMessage message, IGame game)
        {
            foreach (var move in game.Moves)
            {
                await message.AddReactionAsync(move.Emote);
            }
        }
    }
}