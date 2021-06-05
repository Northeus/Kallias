using Kallias.Data;
using Kallias.Game.Graphic;
using game.Environment;
using game.Extensions;

namespace game.Render
{
    /// <summary>
    /// Create renderer creating canvas representing given world.
    /// </summary>
    public static class WorldRenderer
    {
        /// <summary>
        /// Get canvas representation of given world.
        /// </summary>
        /// <param name="world">World to be represented.</param>
        /// <returns>Canvas representing graphical view of world.</returns>
        public static Canvas GetCanvas(World world)
            => new (GetSpriteMap(world.TileMap)
                .Map(sprite => DatabaseEmotes<Sprite>.Database[sprite])
                .GetAllRows()
            );

        private static Sprite[,] GetSpriteMap(Tile[,] tilemap)
            => tilemap.Map(FindSprite);

        private static Sprite FindSprite(Tile tile)
            => tile switch 
            {
                Tile.Grass      => Sprite.GreenSquare,
                Tile.Planks     => Sprite.BrownSquare,
                Tile.Tree       => Sprite.Tree,
                _               => throw new UnhandledSpriteException("Missing texture bind for tile.")
            };
    }
}