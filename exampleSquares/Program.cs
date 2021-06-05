using System.Threading.Tasks;
using Kallias.Bot;

namespace exampleSquare
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Load our game instance into bot
            var bot = new Bot(new Game());

            // Run bot
            await bot.StartUp();
        }
    }
}
