using game.Environment;

namespace game.Character
{
    public class Player
    {
        private readonly World _world;

        public Player(World world)
            => _world = world;

        public (int X, int Y) Position { get; private set; }

        public bool DoAction(PlayerAction action)
            => action switch
            {
                PlayerAction.MoveUp       => TryMove(Position.X, Position.Y - 1),
                PlayerAction.MoveDown     => TryMove(Position.X, Position.Y + 1),
                PlayerAction.MoveLeft     => TryMove(Position.X - 1, Position.Y),
                PlayerAction.MoveRight    => TryMove(Position.X + 1, Position.Y),
                _ => throw new UnhandledActionException("Do action is missing pattern for osme actions.")
            };

        private bool TryMove(int x, int y)
        {
            if (_world.IsValidCoords(x, y) && _world.TileMap[x, y] == Tile.Grass)
            {
                Position = (x, y);

                return true;
            }

            return false;
        }
    }
}