using System;
using System.Drawing;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents an appearance scheme for anchor points.
    /// </summary>
    public class AnchorStyle : IDisposable
    {
        /// <summary>
        /// Gets or sets the diameter of the anchors.
        /// </summary>
        public int Diameter { get; set; }

        /// <summary>
        /// Gets or sets the shape of the anchors.
        /// </summary>
        public Shape Shape { get; set; }

        private readonly SolidBrush brush = new SolidBrush(Color.DimGray);
        /// <summary>
        /// Gets or sets the color of the anchors.
        /// </summary>
        public Color Color
        {
            get { return brush.Color; }
            set { brush.Color = value; }
        }

        private readonly SolidBrush hoverBrush = new SolidBrush(Color.Black);
        /// <summary>
        /// Gets or sets the color of the anchors when the mouse is hovered.
        /// </summary>
        public Color HoverColor
        {
            get { return hoverBrush.Color; }
            set { hoverBrush.Color = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnchorStyle"/> class.
        /// </summary>
        public AnchorStyle()
        {
            Diameter = 10;
            Shape = Shape.Rectangle;
        }

        /// <summary>
        /// Releases all resources used by this <see cref="AnchorStyle"/> object.
        /// </summary>
        public void Dispose()
        {
           brush.Dispose();
           hoverBrush.Dispose();
        }
    }
}
