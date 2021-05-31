using System;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game;
using Kallias.Game.Graphic;

namespace exampleSquare
{
    public class Game : IGame
    {
        public Color CurrentColor = Color.Default;

        public List<Move> Moves => new List<Move>()
        {
            new Move("🟥", (int) Color.Red),
            new Move("🟩", (int) Color.Green),
            new Move("🟦", (int) Color.Blue)
        };

        public void MakeMove(Move move)
            => CurrentColor = (Color) move.Id;

        public Canvas Render()
            => Fill(GetFiller());

        public object Clone()
            => new Game();

        private Canvas Fill(string filler)
            => new Canvas(new string[2]
            {
                $"{filler}{filler}",
                $"{filler}{filler}",
            });

        private string GetFiller()
            => DatabaseEmotes<Color>.TryGet(CurrentColor, out string emoji)
                ? emoji
                : "(Not Found)";
    }
}