using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game;
using Kallias.Game.Graphic;

// For easier presentation is this code in single file.
namespace exampleSquare
{
    // Example of simple game, which just change color according to reaction.
    public class Game : IGame
    {
        // Create some basic state machine
        private Color _currentColor = Color.Default;

        // Create some basic moves
        public List<Move> Moves => new List<Move>()
        {
            new Move("🟥", (int) Color.Red),
            new Move("🟩", (int) Color.Green),
            new Move("🟦", (int) Color.Blue)
        };

        // Change current state
        public void MakeMove(Move move)
            => _currentColor = (Color) move.Id;

        // Render color according to current state
        public Canvas Render()
            => Fill(GetFiller());

        // Create new instance of Game
        public object Clone()
            => new Game();

        // Create simple square from filler string
        private Canvas Fill(string filler)
            => new Canvas(new []
            {
                new [] { filler, filler },
                new [] { filler, filler }
            });

        // Get emoji according to current state 
        private string GetFiller()
            => DatabaseEmotes<Color>.TryGet(_currentColor, out var emoji)
                ? emoji
                : "(Not Found)";
    }
}