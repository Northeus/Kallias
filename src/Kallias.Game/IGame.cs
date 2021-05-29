using System;
using System.Collections.Generic;
using Kallias.Game.Graphic;

namespace Kallias.Game
{
    /// <summary>
    /// Should implement <c>Clone</c> method, which will create clear
    /// copy of given object (without copy of current objects state).
    /// </summary>
    public interface IGame : ICloneable
    {   
        IEnumerable<Move> Moves { get; }

        void MakeMove(Move move);

        Canvas Render();
    }
}