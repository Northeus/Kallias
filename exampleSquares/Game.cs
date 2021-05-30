using System;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game;
using Kallias.Game.Graphic;

// For easier presentation is this code in single file.
namespace exampleSquare
{
    // Example of eimple game, which just change color acording to reaction.
    public class Game : IGame
    {
        // Create some bacis state machine
        public Color CurrentColor = Color.Default;

        // Create some basic moves
        public List<Move> Moves => new List<Move>()
        {
            new Move("🟥", (int) Color.Red),
            new Move("🟩", (int) Color.Green),
            new Move("🟦", (int) Color.Blue)
        };

        // Change current state
        public void MakeMove(Move move)
            => CurrentColor = (Color) move.Id;

        // Render something acording to current state
        public Canvas Render()
            => Fill(GetFiller());

        // Create new instance of Game
        public object Clone()
            => new Game();

        // Create simple square from emoji
        private Canvas Fill(string filler)
            => new Canvas(new string[2]
            {
                $"{filler}{filler}",
                $"{filler}{filler}",
            });

        // Get emoji according to current state 
        private string GetFiller()
            => DatabaseEmotes<Color>.TryGet(CurrentColor, out string emoji)
                ? emoji
                : "(Not Found)";
    }
}