using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using BlueprintBoard.Forms;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents a visible grid for a <see cref="DrawingCanvas"/>.
    /// </summary>
    public sealed class Grid : DataBindingProvider, IDisposable
    {
        private readonly Pen pen = new Pen(Color.FromArgb(50, 240, 240, 255), 1);

        /// <summary>
        /// Gets or sets the width of the grid lines.
        /// </summary>
        public float StrokeWidth
        {
            get { return pen.Width; }
            set
            {
                if (pen.Width != value)
                {
                    pen.Width = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the grid lines.
        /// </summary>
        public Color Color
        {
            get { return pen.Color; }
            set
            {
                if (pen.Color != value)
                {
                    pen.Color = value;
                    OnPropertyChanged();
                }
            }
        }

        private int cellDimensions = 70;
        /// <summary>
        /// Gets or sets the dimensions of the cells.
        /// </summary>
        public int CellDimensions
        {
            get { return cellDimensions; }
            set
            {
                if (cellDimensions != value)
                {
                    cellDimensions = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Draw this grid onto the specified drawing surface.
        /// </summary>
        /// <param name="graphics">The surface to draw to.</param>
        /// <param name="canvasSize">The size of the drawing canvas.</param>
        public void Draw(Graphics graphics, Size canvasSize)
        {
            // Draw vertical lines
            for (int x = 0; x <= canvasSize.Width; x += CellDimensions)
            {
                graphics.DrawLine(pen, x, 0, x, canvasSize.Height);
            }

            // Draw horizontal lines
            for (int y = 0; y <= canvasSize.Height; y += CellDimensions)
            {
                graphics.DrawLine(pen, 0, y, canvasSize.Width, y);
            }
        }

        /// <summary>
        /// Returns points where grid lines intersect.
        /// </summary>
        /// <param name="withinRect">The rectangle that specifies the boundary to look for points.</param>
        public IEnumerable<PointF> GetIntersectingPoints(RectangleF withinRect)
        {
            var points = new Stack<PointF>();

            for (int x = 0; x <= withinRect.Width; x += CellDimensions)
                for (int y = 0; y <= withinRect.Height; y += CellDimensions)
                    points.Push(new PointF(x, y));

            return points.ToArray();
        }

        /// <summary>
        /// Releases all resources used by this <see cref="Grid"/>.
        /// </summary>
        public void Dispose()
        {
           pen.Dispose();
        }
    }
}
