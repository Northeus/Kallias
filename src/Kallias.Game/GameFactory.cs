using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Kallias.Data;

namespace Kallias.Game
{
    internal class GameFactory
    {
        private static GameFactory _instance;

        private GameFactory()
        {

        }

        public static GameFactory Instance
        {
            get => (_instance ??= new GameFactory());

            set => _instance = value;
        }
        
        public IGame Game { get; set; }

        public IGame CreateGame()
            => (IGame) Game.Clone();

        public Move FindMove(IEmote emote)
            => Game.Moves
                .FirstOrDefault(move => move.Emote == emote);

        public async Task ProcessReactionAsync(ulong messageId, SocketReaction reaction)
        {
            var move = FindMove(reaction.Emote);

            var gotGame = DatabaseGames.TryGet(messageId, out var gameContext);

            if (move == null || ! gotGame)
            {
                return;
            }

            var (game, message) = (gameContext.Game, gameContext.Message);

            game.MakeMove(move);

            await message.ModifyAsync(msg => msg.Content = (string) gameContext.Game.Render());
        }
    }
}