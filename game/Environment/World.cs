namespace game.Environment
{
    /// <summary>
    /// Representation of world environment for player.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Create new world with specified size.
        /// Also will ensure, that top left block is empty, so bot can be placed there.
        /// </summary>
        /// <param name="width">Width of new world.</param>
        /// <param name="height">Height of new world.</param>
        public World(int width, int height)
        {
            TileMap = WorldGenerator.CreateNewTilemap(width, height);

            TileMap[0, 0] = Tile.Grass;

            Size = (width, height);
        }

        /// <summary>
        /// Access tilemap of given world.
        /// </summary>
        public Tile[,] TileMap { get; }

        /// <summary>
        /// Access size of current world.
        /// </summary>
        public (int Width, int Height) Size { get; }

        /// <summary>
        /// Check, whether given position is placed inside world (tilemap).
        /// </summary>
        /// <param name="x">X coordinate of position.</param>
        /// <param name="y">Y coordinate of position.</param>
        /// <returns>True, if given position is inside world, false otherwise.</returns>
        public bool IsValidCoords(int x, int y)
            => 0 <= x 
                && x < Size.Width
                && 0 <= y
                && y < Size.Height; 
    }
}