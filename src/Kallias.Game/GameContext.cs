namespace Kallias.Game
{
    internal class GameContext
    {
        private static GameContext _instance;

        private GameContext()
        {

        }

        public static GameContext Instance
        {
            get => (_instance ??= new GameContext());

            set => _instance = value;
        }
        
        public IGame Game { get; set; }
    }
}