namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Provides a set of indexes that map to a point in a <see cref="PathData"/>.
    /// </summary>
    public struct PointMap
    {
        /// <summary>
        /// Represents an empty <see cref="PointMap"/> with the indexes as -1.
        /// </summary>
        public static PointMap Empty = new PointMap(-1, -1);

        /// <summary>
        /// Gets the index to the path in the <see cref="PathData"/>.
        /// </summary>
        public int PathIndex { get; }

        /// <summary>
        /// Gets the index to the point in the <see cref="PathData"/>.
        /// </summary>
        public int PointIndex { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointMap"/> class,
        /// with the specified arguments.
        /// </summary>
        /// <param name="pathIndex">The index to the path in the <see cref="PathData"/>.</param>
        /// <param name="pointIndex">The index to the point in the <see cref="PathData"/>.</param>
        public PointMap(int pathIndex, int pointIndex) : this()
        {
            PathIndex = pathIndex;
            PointIndex = pointIndex;
        }

        #region Equality
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> and this instance are the same type and represent the same value; otherwise, false. 
        /// </returns>
        /// <param name="other">The <see cref="PointMap"/> to compare with the current instance.</param>
        public bool Equals(PointMap other)
        {
            return PathIndex == other.PathIndex && PointIndex == other.PointIndex;
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
            return obj is PointMap && Equals((PointMap)obj);
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
            unchecked
            {
                return (PathIndex * 397) ^ PointIndex;
            }
        }

        /// <summary></summary>
        public static bool operator ==(PointMap left, PointMap right)
        {
            return left.Equals(right);
        }

        /// <summary></summary>
        public static bool operator !=(PointMap left, PointMap right)
        {
            return !left.Equals(right);
        }
        #endregion
    }
}
