using System;
using System.Drawing;
using System.Xml.Serialization;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents an XML serializable <see cref="Color"/>.
    /// </summary>
    public struct XmlColor
    {
        // The underlying color.
        private Color color;

        /// <summary>
        /// Gets or sets the text representation of this color.
        /// </summary>
        [XmlText]
        [XmlAttribute]
        public string Default
        {
            get { return ToString(); }
            set { color = Parse(value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlColor"/> class, 
        /// with the specified argument.
        /// </summary>
        /// <param name="red">The red level of the color.</param>
        /// <param name="green">The green level of the color.</param>
        /// <param name="blue">The blue level of the color.</param>
        /// <param name="alpha">The alpha level of the color.</param>
        public XmlColor(int red, int green, int blue, int alpha = 255)
            : this(Color.FromArgb(alpha, red, green, blue))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlColor"/> class, 
        /// with the specified argument.
        /// </summary>
        private XmlColor(Color c) { color = c; }

        /// <summary></summary>
        public static implicit operator Color(XmlColor x)
        {
            return x.color;
        }

        /// <summary></summary>
        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        public override string ToString()
        {
            if (color.IsNamedColor)
                return color.Name;

            int colorValue = color.ToArgb();

            if (((uint)colorValue >> 24) == 0xFF)
            {
                return $"#{colorValue & 0x00FFFFFF:X6}";
            }
            else
            {
                return $"#{colorValue:X8}";
            }
        }

        /// <summary>
        /// Converts the string representation of a color to a <see cref="Color"/> instance.
        /// </summary>
        /// <param name="value">The string representation.</param>
        public static Color Parse(string value)
        {
            if (value[0] == '#')
            {
                return Color.FromArgb((value.Length <= 7 ? unchecked((int)0xFF000000) : 0) +
                    Int32.Parse(value.Substring(1), System.Globalization.NumberStyles.HexNumber));
            }
            else
            {
                return Color.FromName(value);
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="XmlColor"/> are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> and this instance are the same type and represent the same value; otherwise, false. 
        /// </returns>
        /// <param name="other">The XmlColor<see cref="XmlColor"/> to compare with the current instance.</param>
        public bool Equals(XmlColor other)
        {
            return (color.A == other.color.A &&
                    color.R == other.color.R &&
                    color.G == other.color.G &&
                    color.B == other.color.B);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false. 
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is XmlColor && Equals((XmlColor) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return color.GetHashCode();
        }

        /// <summary></summary>
        public static bool operator ==(XmlColor left, XmlColor right)
        {
            return left.Equals(right);
        }

        /// <summary></summary>
        public static bool operator !=(XmlColor left, XmlColor right)
        {
            return !left.Equals(right);
        }
    }
}
