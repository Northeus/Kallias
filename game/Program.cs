using System.Threading.Tasks;
using Kallias.Bot;

namespace game
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Create new instance of bot with bound game.
            var bot = new Bot(new Game());

            // Run bot.
            await bot.StartUp();
        }
    }
}
