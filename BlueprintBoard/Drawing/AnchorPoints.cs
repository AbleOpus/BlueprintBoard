using System.Drawing;
using System.Linq;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents all of the vertexes of the drawn paths.
    /// </summary>
    class AnchorPoints
    {
        private readonly PathData pathData;

        /// <summary>
        /// Gets the style or appearance of the anchor.
        /// </summary>
        public AnchorStyle Style { get; }

        /// <summary>
        /// Gets or sets whether to draw the anchor point that is currently hovered.
        /// </summary>
        public bool DrawHovered { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorPoints"/> class,
        /// with the specified argument.
        /// </summary>
        public AnchorPoints(PathData pathData)
        {
            this.pathData = pathData;
            Style = new AnchorStyle();
        }

        /// <summary>
        /// Draws the anchor points onto the specified drawing surface.
        /// </summary>
        /// <param name="graphics">The surface to draw to.</param>
        /// <param name="cursorPosition">The client position of the cursor.</param>
        public void Draw(Graphics graphics, Point cursorPosition)
        {
            foreach (PointF pos in pathData.AllPoints)
            {
                var rect = new RectangleF(pos.X - Style.Diameter / 2f, pos.Y -
                    Style.Diameter / 2f, Style.Diameter, Style.Diameter);

                Color color = (rect.Contains(cursorPosition) && DrawHovered)
                    ? Style.HoverColor : Style.Color;

                var brush = new SolidBrush(color);

                if (Style.Shape == Shape.Ellipse)
                {
                    graphics.FillEllipse(brush, rect);
                }
                else
                {
                    graphics.FillRectangle(brush, rect);
                }
            }
        }

        /// <summary>
        /// Gets the index of a path in the path list from a specified point.
        /// </summary>
        /// <param name="position">The point that a node rectangle must contain.</param>
        /// <returns>Returns -1, if no path found.</returns>
        public int GetPathIndexFromPoint(Point position)
        {
            for (int i = 0; i < pathData.Paths.Count(); i++)
            {
                var path = pathData.Paths.ElementAt(i);

                foreach (PointF temp in path.Lines)
                {
                    var x = temp.X - Style.Diameter/2f;
                    var y = temp.Y - Style.Diameter/2f;
                    var rect = new RectangleF(x, y, Style.Diameter, Style.Diameter);
                    if (rect.Contains(position)) return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Looks under the cursor for a path anchor. If there is one, then a PointMap to the point
        /// associated with the anchor is yielded.
        /// </summary>
        /// <param name="cursorPosition">The cursor client position to look around for a near ??.</param>
        /// <returns></returns>
        public PointMap GetHoverPointMap(Point cursorPosition)
        {
            for (int i = 0; i < pathData.Paths.Count(); i++)
            {
                var path = pathData.Paths.ElementAt(i);

                for (int i2 = 0; i2 < path.Lines.Length; i2++)
                {
                    PointF point = path.Lines[i2];
                    float x = point.X - Style.Diameter/2f;
                    float y = point.Y - Style.Diameter/2f;
                    var rect = new RectangleF(x, y, Style.Diameter, Style.Diameter);

                    if (rect.Contains(cursorPosition))
                    {
                        return new PointMap(i, i2);
                    }
                }
            }

            return PointMap.Empty;
        }
    }
}
