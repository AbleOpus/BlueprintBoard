using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BlueprintBoard.Drawing
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Returns a random <see cref="Color"/>.
        /// </summary>
        internal static Color NextColor(this Random random) => 
            Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

        /// <summary>
        /// Draws a 1-pixel border inside around the edges of the drawing surface.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color">The color of the border.</param>
        /// <param name="size">The size of the border.</param>
        internal static void DrawInsetBorder(this Graphics graphics, Color color, Size size)
        {
            // save smoothing mode, draw, then restore.
            SmoothingMode lastSmooth = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.DrawRectangle(new Pen(color), 0, 0, size.Width - 1, size.Height - 1);
            graphics.SmoothingMode = lastSmooth;
        }
    }
}
