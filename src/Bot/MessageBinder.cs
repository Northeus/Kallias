using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord.Rest;
using Discord.WebSocket;
using Kallias.Game;
using Kallias.Data;

namespace Kallias.Bot {
    /// <summary>
    /// Handler for every incoming message, which will check,
    /// whether any user want to create new game.
    /// </summary>
    internal class CommandHandler
    {
        private static readonly Regex CommandValidator = new Regex(@"^\.new(\s.*)?$");

        private readonly DiscordSocketClient _client;

        /// <summary>
        /// Create new command handler with bound socket client.
        /// </summary>
        /// <param name="client">Discord socket client for reading messages.</param>
        public CommandHandler(DiscordSocketClient client)
            => _client = client;
        
        /// <summary>
        /// Load message handler into client event handler, so it will read incoming messages.
        /// </summary>
        public void SetUp()
            => _client.MessageReceived += HandleCommand;

        /// <summary>
        /// Check, whether message is supposed to create new game and then process it.
        /// We allow creating messages with bots, as it might be interesting to
        /// create bot which will play those games.
        /// </summary>
        /// <param name="message">Incoming message from handler.</param>
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

        private static void ProcessCommand(SocketMessage message)
            => ThreadPool.QueueUserWorkItem(async delegate
            {
                await CreateNewGameAsync(message);

                await message.DeleteAsync();
            });

        private static async Task CreateNewGameAsync(SocketMessage messageCommand)
        {
            var game = GameFactory.Instance.CreateGame();

            var messageGame = await messageCommand.Channel.SendMessageAsync((string) game.Render());

            await AddReactionsAsync(messageGame, game);

            DatabaseGames.Insert(
                messageGame.Id,
                game,
                messageGame,
                messageCommand.Author.Id
            );
        } 

        /// <summary>
        /// Function will slowly put reactions to message one by one to ensure their order.
        /// </summary>
        private static async Task AddReactionsAsync(RestUserMessage message, IGame game)
        {
            foreach (var move in game.Moves)
            {
                await message.AddReactionAsync(move.Emote);
            }
        }
    }
}