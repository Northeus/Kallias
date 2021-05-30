using System.Threading;
using Discord;
using Discord.Rest;

namespace Kallias.Game
{
    internal class GameContext
    {
        private int _locked;

        public GameContext(IGame game, RestUserMessage message)
            => (Game, Message) = (game, message);

        public IGame Game { get; }

        public RestUserMessage Message { get; }

        public bool TryLock()
            => Interlocked.Exchange(ref _locked, 1) == 0;

        public void Unlock()
            => _locked = 0;
    }
}