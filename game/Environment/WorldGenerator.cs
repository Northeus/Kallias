using System;
using game.Extensions;

namespace game.Environment
{
    public static class WorldGenerator
    {
        public const double TreeProbability = 0.1; 

        private static readonly Random _rng = new Random();

        public static Tile[,] CreateNewTilemap(int width, int height)
            => new Tile[height, width].Map((_) => GenerateRandomTile());

        private static Tile GenerateRandomTile()
            => _rng.NextDouble() < TreeProbability
                ? Tile.Tree
                : Tile.Grass;
    }
}