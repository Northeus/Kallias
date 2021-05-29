using System;
using System.Collections.Generic;

namespace Kallias.Game.Graphic
{
    public class Canvas
    {
        private IEnumerable<string> _view;

        public Canvas(IEnumerable<string> view)
            => _view = view;

        public static explicit operator string(Canvas canvas)
            => string.Join($"{Environment.NewLine}", canvas._view);
    }
}