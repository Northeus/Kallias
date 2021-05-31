namespace game.Environment
{
    public class World
    {
        public World(int width, int height)
        {
            TileMap = WorldGenerator.CreateNewTilemap(width, height);

            TileMap[0, 0] = Tile.Grass;

            Size = (width, height);
        }

        public Tile[,] TileMap { get; }

        public (int Width, int Height) Size { get; }

        public bool IsValidCoords(int x, int y)
            => 0 <= x 
                && x < Size.Width
                && 0 <= y
                && y < Size.Height; 
    }
}