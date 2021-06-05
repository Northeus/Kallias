using System;
using game.Extensions;

namespace game.Environment
{
    /// <summary>
    /// Class handling tilemap (world) generation.
    /// </summary>
    public static class WorldGenerator
    {
        private const double TreeProbability = 0.1; 

        private static readonly Random Rng = new ();

        /// <summary>
        /// Create new tilemap of specific size.
        /// </summary>
        /// <param name="width">Width of new world.</param>
        /// <param name="height">Height of new world.</param>
        /// <returns>New world.</returns>
        public static Tile[,] CreateNewTilemap(int width, int height)
            => new Tile[height, width].Map(_ => GenerateRandomTile());

        private static Tile GenerateRandomTile()
            => Rng.NextDouble() < TreeProbability
                ? Tile.Tree
                : Tile.Grass;
    }
}