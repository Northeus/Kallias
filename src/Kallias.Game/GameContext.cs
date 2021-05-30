using Discord;
using Discord.Rest;

namespace Kallias.Game
{
    internal class GameContext
    {
        public GameContext(IGame game, RestUserMessage message)
            => (Game, Message) = (game, message);

        public IGame Game { get; }

        public RestUserMessage Message { get; }
    }
}