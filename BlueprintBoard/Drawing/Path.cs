using System;
using System.Drawing;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents a styled path.
    /// </summary>
    [Serializable]
    public class Path : ICloneable<Path>
    {
        // Needs to be serializable, we cannot just use pen.

        /// <summary>
        /// Gets the style of the <see cref="Pen"/> used to draw this path.
        /// </summary>
        public XmlPen Pen { get; set; }

        /// <summary>
        /// Gets the draw points of the path.
        /// </summary>
        public PointF[] Lines { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        public Path() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class,
        /// with the specified arguments.
        /// </summary>
        /// <param name="pen">The pen used to draw this path.</param>
        /// <param name="lines">The lines of this path.</param>
        public Path(Pen pen, PointF[] lines)
        {
            Pen = pen;
            Lines = lines;
        }

        /// <summary>
        /// Gets the distance between two points.
        /// </summary>
        public static float DistanceTo(PointF p1, PointF p2)
        {
            double a = p1.X - p2.X;
            double b = p1.Y - p2.Y;
            return (float)Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Gets the total length of all lines in this path.
        /// </summary>
        public float GetLength()
        {
            float totalLentgh = 0;

            for (int i = 0; i < Lines.Length - 1; i++)
            {
                totalLentgh += DistanceTo(Lines[i], Lines[i + 1]);
            }

            return totalLentgh;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public Path Clone()
        {
            return new Path((XmlPen)Pen.Clone(), (PointF[])Lines.Clone());
        }
    }
}
