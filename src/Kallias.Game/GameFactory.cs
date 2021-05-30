using System.Linq;
using System.Threading;
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
                .FirstOrDefault(move => move.Emote.Name == emote.Name);

        public async Task ProcessReactionAsync(ulong messageId, SocketReaction reaction)
        {
            var move = FindMove(reaction.Emote);

            DatabaseGames.TryGet(messageId, out var gameContext);

            if (move == null || gameContext?.TryLock() != true)
            {
                return;
            }

            var (game, message) = (gameContext.Game, gameContext.Message);

            game.MakeMove(move);

            await message.ModifyAsync(msg => msg.Content = (string) game.Render());

            gameContext.Unlock();
        }
    }
}