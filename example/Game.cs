using System.Collections.Generic;
using Kallias.Game;
using Kallias.Game.Graphic;

namespace example
{
    public class Game : IGame
    {
        public List<Move> Moves => new List<Move>();

        public void MakeMove(Move move)
        {

        }

        public Canvas Render()
            => new Canvas(new string[0]);

        public object Clone()
            => new Game();
    }
}