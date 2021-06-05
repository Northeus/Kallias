using System;
using System.Linq;
using System.Collections.Generic;
// ReSharper disable InvalidXmlDocComment

namespace Kallias.Game.Graphic
{
    /// <summary>
    /// Handle 2D graphic window in text representation. 
    /// </summary>
    public class Canvas
    {
        private readonly IEnumerable<IEnumerable<string>> _view;

        /// <summary>
        /// Create 2D canvas using 2D <c>IEnumerable</c> of strings.
        /// </summary>
        /// <example>
        /// var canvas = new List<List<string>>()
        /// {
        /// new List<string>() { "^", "^" },
        /// new List<string>() { " ", " " }
        /// };
        /// </example>
        /// <param name="view"></param>
        public Canvas(IEnumerable<IEnumerable<string>> view)
            => _view = view;

        /// <summary>
        /// Create string representation of canvas
        /// </summary>
        /// <param name="canvas">Canvas which should be casted to string.</param>
        /// <returns></returns>
        public static explicit operator string(Canvas canvas)
            => string.Join($"{Environment.NewLine}", canvas.JoinColumns());

        /// <summary>
        /// Join two canvases into one, where every space characters in second canvas is filled with
        /// content from the same place in first canvas (like placing canvas with holes over another).
        /// </summary>
        /// <param name="background">First canvas used as bottom layer.</param>
        /// <param name="foreground">Second canvas used as top layer.</param>
        /// <returns>Combined canvas from given two canvases.</returns>
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