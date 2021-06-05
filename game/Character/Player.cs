using game.Environment;

namespace game.Character
{
    /// <summary>
    /// Class representing player with his position in world.
    /// </summary>
    public class Player
    {
        private readonly World _world;

        /// <summary>
        /// Create new player with world bound to him.
        /// </summary>
        /// <param name="world">World, in which is player placed.</param>
        public Player(World world)
            => _world = world;

        /// <summary>
        /// Current position of player.
        /// </summary>
        public (int X, int Y) Position { get; private set; }

        /// <summary>
        /// Attempt to do given <c>action</c>.
        /// </summary>
        /// <param name="action">Action to be made.</param>
        /// <returns>True, if action was successfully made.</returns>
        public bool DoAction(PlayerAction action)
            => action switch
            {
                PlayerAction.MoveUp       => TryMove(Position.X, Position.Y - 1),
                PlayerAction.MoveDown     => TryMove(Position.X, Position.Y + 1),
                PlayerAction.MoveLeft     => TryMove(Position.X - 1, Position.Y),
                PlayerAction.MoveRight    => TryMove(Position.X + 1, Position.Y),
                _ => throw new UnhandledActionException("Do action is missing pattern for some actions.")
            };

        /// <summary>
        /// Attempt to move on given coordinates.
        /// </summary>
        /// <param name="x">X coordinate of new position.</param>
        /// <param name="y">Y coordinate of new position.</param>
        /// <returns>True, if character was moved or false, if position is invalid.</returns>
        private bool TryMove(int x, int y)
        {
            if (_world.IsValidCoords(x, y) && _world.TileMap[y, x] == Tile.Grass)
            {
                Position = (x, y);

                return true;
            }

            return false;
        }
    }
}