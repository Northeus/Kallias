using static System.Environment;
using System.Linq;
using System.Collections.Generic;
using Kallias.Data;
using Kallias.Game.Graphic;
using game.Environment;
using game.Extensions;

namespace game.Render
{
    public static class WorldRenderer
    {
        public static Canvas GetCanvas(World world)
            => new Canvas(GetRows(world));

        private static IEnumerable<string> GetRows(World world)
            => GetSpriteMap(world.TileMap)
                .Map((Sprite sprite) => DatabaseEmotes<Sprite>.Database[sprite])
                .GetAllRows()
                .Select(row => row
                    .Aggregate(
                        "",
                        (baseString, spriteString) => baseString + spriteString
                ));
        
        private static Sprite[,] GetSpriteMap(Tile[,] tilemap)
            => tilemap.Map(FindSprite);

        private static Sprite FindSprite(Tile tile)
            => tile switch 
            {
                Tile.Grass      => Sprite.GreenSquare,
                Tile.Planks     => Sprite.BrownSquare,
                Tile.Tree       => Sprite.Tree,
                Tile.Sapling    => Sprite.Plant,
                _               => throw new UnhandledSpriteException("Missing texture bind for tile.")
            };
    }
}