using System;
using System.Collections.Generic;
using Kallias.Game.Graphic;

namespace Kallias.Game
{
    /// <summary>
    /// Represent game instance.
    /// <c>Clone</c> method should return instance of new game.
    /// </summary>
    public interface IGame : ICloneable
    {
        /// <summary>
        /// Get list of all possible moves in specified order,
        /// how the should be displayed.
        /// </summary>
        IEnumerable<Move> Moves { get; }

        /// <summary>
        /// Attempt to make move.
        /// </summary>
        /// <param name="move">Specified move to be attempted.</param>
        void MakeMove(Move move);

        /// <summary>
        /// Create current graphic representation of game, which should be displayed.
        /// </summary>
        /// <returns>Current graphic representation.</returns>
        Canvas Render();
    }
}
