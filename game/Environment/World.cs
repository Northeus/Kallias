namespace game.Environment
{
    public class World
    {
        public World(int width, int height)
            => TileMap = WorldGenerator.CreateNewTilemap(width, height);

        public Tile[,] TileMap { get; }
    }
}