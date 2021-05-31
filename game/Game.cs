using System;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game;
using Kallias.Game.Graphic;
using game.Environment;
using game.Render;

namespace game
{
    public class Game : IGame
    {
        private World _world = new World(7, 5);

        public List<Move> Moves => new List<Move>()
        {
            // new Move("🟥", (int) Color.Red),
        };

        public void MakeMove(Move move)
        {

        }

        public Canvas Render()
            => WorldRenderer.GetCanvas(_world);

        public object Clone()
            => new Game();
    }
}