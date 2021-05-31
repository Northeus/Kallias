using System;
using System.Linq;
using System.Collections.Generic;

namespace Kallias.Game.Graphic
{
    public class Canvas
    {
        private IEnumerable<IEnumerable<string>> _view;

        public Canvas(IEnumerable<IEnumerable<string>> view)
            => _view = view;

        public static explicit operator string(Canvas canvas)
            => string.Join($"{Environment.NewLine}", canvas.JoinColumns());

        public static Canvas operator +(Canvas background, Canvas foreground)
            => new Canvas(JoinLayers(background, foreground));

        private static IEnumerable<IEnumerable<string>> JoinLayers(Canvas background, Canvas foreground)
            => foreground._view
                .Zip(background._view, MaskSpaces);

        private static IEnumerable<string> MaskSpaces(
            IEnumerable<string> foregroundRow, IEnumerable<string> backgroundRow
        ) => foregroundRow 
                .Zip(backgroundRow, MaskSpace);

        private static string MaskSpace(string foregroundStr, string backgroundStr)
            => foregroundStr == " "
                ? backgroundStr
                : foregroundStr;

        private IEnumerable<string> JoinColumns()
            => _view
                .Select(row => row
                    .Aggregate(
                        "",
                        (strBase, strCurr) => strBase + strCurr
                ));
    }
}