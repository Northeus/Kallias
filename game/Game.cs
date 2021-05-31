using System;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game;
using Kallias.Game.Graphic;
using game.Character;
using game.Environment;
using game.Render;

namespace game
{
    public class Game : IGame
    {
        private const int Width = 16;
        
        private const int Height = 9;

        private World _world;

        private Player _player;

        public Game()
        {
            _world = new World(Width, Height);
            _player = new Player(_world);
        }

        public List<Move> Moves => new List<Move>()
        {
            new Move(DatabaseEmotes<Sprite>.Database[Sprite.MoveLeft], (int) PlayerAction.MoveLeft),
            new Move(DatabaseEmotes<Sprite>.Database[Sprite.MoveUp], (int) PlayerAction.MoveUp),
            new Move(DatabaseEmotes<Sprite>.Database[Sprite.MoveDown], (int) PlayerAction.MoveDown),
            new Move(DatabaseEmotes<Sprite>.Database[Sprite.MoveRight], (int) PlayerAction.MoveRight)
        };

        public void MakeMove(Move move)
            => _player.DoAction((PlayerAction) move.Id);

        public Canvas Render()
            => WorldRenderer.GetCanvas(_world) + PlayerRenderer.GetCanvas(_player, _world);

        public object Clone()
            => new Game();
    }
}