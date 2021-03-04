using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace Aranslow.Rendering
{
    internal class RenderFactory
    {
        internal static Line LineInstance = new Line();

        internal static void DrawLine(Point startPosition, Point endPosition, int thickness, Brush lColor)
        {
            LineInstance.X1 = startPosition.X;
            LineInstance.Y1 = startPosition.Y;
            LineInstance.X2 = endPosition.X;
            LineInstance.Y2 = endPosition.Y;

            LineInstance.StrokeThickness = thickness;
            LineInstance.Stroke = lColor;
        }

        internal static void Draw2DBox(Point startPosition, Size boxSize, int thickness, Brush lColor)
        {

        }
    }
}
