using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Kallias.Game;

namespace Kallias.Bot {
    internal class CommandHandler
    {
        private static readonly Regex CommandValidator = new Regex(@"^\.new(\s.*)?$");

        private readonly DiscordSocketClient _client;

        public CommandHandler(DiscordSocketClient client)
            => _client = client;
        
        public async Task SetUp()
            => _client.MessageReceived += HandleCommandAsync;

        private async Task HandleCommandAsync(SocketMessage message)
        {
            if (IsSystemMessage(message)
                || ! IsCreateCommand(message))
            {
                return;
            }

            await CreateNewGame(message);

            await message.DeleteAsync();
        }

        private static bool IsSystemMessage(SocketMessage message)
            => (message as SocketUserMessage) == null;

        private static bool IsCreateCommand(SocketMessage message)
            => CommandValidator.IsMatch(message.Content);

        private async Task CreateNewGame(SocketMessage messageCommand)
        {
            var game = GameContext.Instance.CreateGame();

            var messageGame = await messageCommand.Channel.SendMessageAsync((string) game.Render());

            DatabaseGames.Insert(
                messageGame.Id,
                game
            );
        } 
    }
}