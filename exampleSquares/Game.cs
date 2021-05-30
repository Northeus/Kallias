using System.Collections.Generic;
using Kallias.Game;
using Kallias.Game.Graphic;

namespace exampleSquare
{
    public class Game : IGame
    {
        public List<Move> Moves => new List<Move>()
        {
            new Move("🟥"),
            new Move("🟩"),
            new Move("🟦")
        };

        public void MakeMove(Move move)
        {

        }

        public Canvas Render()
            => new Canvas(new string[1] { "RGB" });

        public object Clone()
            => new Game();
    }
}