using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace BlueprintBoard.Drawing
{
    // In an attempt to reduce verbosity of XML, 
    // many properties have been marked as XML attributes.

    /// <summary>
    /// Represents an XML serializable <see cref="Pen"/>.
    /// </summary>
    [Serializable]
    public class XmlPen : IDisposable
    {
        [NonSerialized]
        private readonly Pen pen = new Pen(System.Drawing.Color.Empty);

        #region Properties
        /// <summary>
        /// Get or sets the color of the <see cref="Pen"/>.
        /// </summary>
        public XmlColor Color
        {
            get { return pen.Color; }
            set { pen.Color = value; }
        }

        /// <summary>
        /// Get or sets the width of the <see cref="Pen"/>.
        /// </summary>
        [XmlAttribute]
        public float Width
        {
            get { return pen.Width; }
            set { pen.Width = value; }
        }

        /// <summary>
        /// Get or sets the LineJoin of the <see cref="Pen"/>.
        /// </summary>
        [XmlAttribute]
        public LineJoin LineJoin
        {
            get { return pen.LineJoin; }
            set { pen.LineJoin = value; }
        }

        /// <summary>
        /// Get or sets the start cap of the <see cref="Pen"/>.
        /// </summary>
        [XmlAttribute]
        public LineCap StartCap
        {
            get { return pen.StartCap; }
            set { pen.StartCap = value; }
        }

        /// <summary>
        /// Get or sets the end cap of the <see cref="Pen"/>.
        /// </summary>
        [XmlAttribute]
        public LineCap EndCap
        {
            get { return pen.EndCap; }
            set { pen.EndCap = value; }
        }

        /// <summary>
        /// Get or sets the dash style of the <see cref="Pen"/>.
        /// </summary>
        [XmlAttribute]
        public DashStyle DashStyle
        {
            get { return pen.DashStyle; }
            set { pen.DashStyle = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        public XmlPen() { }

        // Constructor used only for casting
        private XmlPen(Pen pen)
        {
            DashStyle = pen.DashStyle;
            Width = pen.Width;
            Color = pen.Color;
            LineJoin = pen.LineJoin;
            StartCap = pen.StartCap;
            EndCap = pen.EndCap;
        }

        /// <summary></summary>
        public static implicit operator Pen(XmlPen x)
        {
            return x.pen;
        }

        /// <summary></summary>
        public static implicit operator XmlPen(Pen p)
        {
            return new XmlPen(p);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="XmlPen"/>.
        /// </summary>
        public void Dispose()
        {
            pen.Dispose();
        }

        /// <summary>
        /// Creates and returns a clone of this instance.
        /// </summary>
        public object Clone()
        {
            return new XmlPen((Pen)pen.Clone());
        }

        /// <summary>
        /// Compares the significant aspects of 2 <see cref="XmlPen"/>s.
        /// </summary>
        /// <param name="p">The pen to compare to.</param>
        /// <returns>true, if fundamentally the same, otherwise false.</returns>
        public bool SimilairTo(XmlPen p)
        {
            // Do not compare colors in debug as they will be randomized.
#if DEBUG
            return (DashStyle == p.DashStyle) &&
                   (Width == p.Width);
#endif

#pragma warning disable 162
            return (Color == p.Color) &&
                   (DashStyle == p.DashStyle) &&
                   (Width == p.Width);
#pragma warning restore 162
        }
    }
}
