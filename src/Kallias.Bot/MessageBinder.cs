using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using Kallias.Game;

namespace Kallias.Bot {
    internal class CommandHandler
    {
        private static readonly Regex CommandValidator = new Regex(@"^\.new(\s.*)?$");

        private readonly DiscordSocketClient _client;

        public CommandHandler(DiscordSocketClient client)
            => _client = client;
        
        public void SetUp()
            => _client.MessageReceived += HandleCommandAsync;

        private async Task HandleCommandAsync(SocketMessage message)
        {
            if (IsSystemMessage(message)
                || ! IsCreateCommand(message))
            {
                return;
            }

            await CreateNewGameAsync(message);

            await message.DeleteAsync();
        }

        private static bool IsSystemMessage(SocketMessage message)
            => (message as SocketUserMessage) == null;

        private static bool IsCreateCommand(SocketMessage message)
            => CommandValidator.IsMatch(message.Content);

        private static async Task CreateNewGameAsync(SocketMessage messageCommand)
        {
            var game = GameContext.Instance.CreateGame();

            var messageGame = await messageCommand.Channel.SendMessageAsync((string) game.Render());

            AddReactions(messageGame, game);

            DatabaseGames.Insert(
                messageGame.Id,
                game
            );
        } 

        private static void AddReactions(RestUserMessage message, IGame game)
        {
            game.Moves
                .Select(async move => await message.AddReactionAsync(move.Emote));
        }
    }
}