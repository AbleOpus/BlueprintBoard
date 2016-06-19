using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BlueprintBoard.Forms;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents an erase tool for a <see cref="DrawingCanvas"/>.
    /// </summary>
    public class Eraser : DataBindingProvider
    {
        private readonly PathData pathData;
        #region Properties
        /// <summary>
        /// Gets or sets the shape to use with the eraser.
        /// </summary>
        public Shape Shape { get; set; }

        private int radius = 20;
        /// <summary>
        /// Gets or sets the radius of the eraser.
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set
            {
                if (value != radius)
                {
                    radius = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the pen to draw the shape.
        /// </summary>
        public Pen ToolPen { get; set; }

        /// <summary>
        /// Gets or sets the mode in which to erase.
        /// </summary>
        public EraseMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the cursor to use when erasing. If null, then the default cursor
        /// for the control is used.
        /// </summary>
        public Cursor Cursor { get; set; }
        #endregion

        /// <summary>
        /// Occurs when a point is erased using the eraser tool.Yields the index
        /// of the point that has been erased.
        /// </summary>
        [Description("Occurs when a point is erased using the eraser tool.")]
        public event EventHandler<PointMap> PointErased = delegate { };

        /// <summary>
        /// Occurs when a path is erased using the eraser tool. Yields the index
        /// of the path that has been erased.
        /// </summary>
        [Description("Occurs when a path is erased using the eraser tool.")]
        public event EventHandler<int> PathErased = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="Eraser"/> class, with 
        /// the specified argument.
        /// </summary>
        public Eraser(PathData pathData)
        {
            this.pathData = pathData;
            ToolPen = new Pen(Color.FromArgb(100, 255, 255, 255), 2f);
            Shape = Shape.Ellipse;
            Mode = EraseMode.Point;
        }

        /// <summary>
        /// Gets the bounds of the eraser.
        /// </summary>
        /// <param name="cursorPosition">The cursor position in client coordinates.</param>
        private RectangleF GetEraserBounds(Point cursorPosition)
        {
            return new RectangleF(cursorPosition.X - Radius,
                cursorPosition.Y - Radius, Radius * 2f, Radius * 2f);
        }

        /// <summary>
        /// Draws the tool to indicate that the eraser is being used.
        /// </summary>
        /// <param name="cursorPosition">The position of the cursor in client coordinates.</param>
        /// <param name="graphics">The surface to draw to.</param>
        public void DrawTool(Point cursorPosition, Graphics graphics)
        {
            var rect = GetEraserBounds(cursorPosition);

            if (Shape == Shape.Ellipse)
            {
                graphics.DrawEllipse(ToolPen, rect);
            }
            else if (Shape == Shape.Rectangle)
            {
                graphics.DrawRectangle(ToolPen, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }

        /// <summary>
        /// Performs an erase operation according to the properties set in this instance.
        /// </summary>
        /// <param name="cursorPosition">The cursor position in client coordinates.</param>
        public bool PerformErase(Point cursorPosition)
        {
            switch (Mode)
            {
                case EraseMode.Point:
                    return EraseNearPoints(cursorPosition);

                    case EraseMode.Path:
                    return EraseNearPaths(cursorPosition);

                default: return false; // erase disabled
            }
        }

        /// <summary>
        /// Erase points around the cursor position and within the erasers range.
        /// </summary>
        /// <param name="cursorPos">The cursor position in client coordinates.</param>
        /// <returns>Returns true if anything erased.</returns>
        private bool EraseNearPoints(Point cursorPos)
        {
            bool erased = false;
            var rect = GetEraserBounds(cursorPos);

            for (int i2 = 0; i2 < pathData.Paths.Count(); i2++)
            {
                Path path = pathData.Paths.ElementAt(i2);
                var points = path.Lines.ToList();

                for (int i = 0; i < points.Count; i++)
                {
                    if (Shape == Shape.Rectangle && !rect.Contains(points[i]))
                    {
                        continue;
                    }

                    if (Shape == Shape.Ellipse && Path.DistanceTo(cursorPos, points[i]) > Radius)
                    {
                        continue;
                    }

                    if (points.Count > 2)
                    {
                        points.RemoveAt(i);
                        PointErased(this, new PointMap(i2, i));
                        erased = true;
                        i--;
                    }
                    else
                    {
                        pathData.RemovePathAt(i2);
                        PointErased(this, new PointMap(i2, i));
                        PathErased(this, i2);
                        i2--;
                        erased = true;
                        break;
                    }
                }

                path.Lines = points.ToArray();
            }

            return erased;
        }

        /// <summary>
        /// Erases the points from the points list that are within the range of the eraser.
        /// </summary>
        /// <param name="cursorPos">The cursor position on the canvas in client coordinates.</param>
        /// <returns>True, if a path has been erased, otherwise false.</returns>
        private bool EraseNearPaths(Point cursorPos)
        {
            RectangleF rect = GetEraserBounds(cursorPos);
            bool erased = false;

            for (int i = 0; i < pathData.Paths.Count(); i++)
            {
                var path = pathData.Paths.ElementAt(i);

                foreach (PointF point in path.Lines)
                {
                    if ((Shape == Shape.Rectangle && rect.Contains(point)) || 
                        (Shape == Shape.Ellipse && Path.DistanceTo(cursorPos, point) <= Radius))
                    {
                        pathData.RemovePathAt(i);
                        i--;
                        erased = true;
                        break;
                    }
                }
            }

            return erased;
        }
    }
}
