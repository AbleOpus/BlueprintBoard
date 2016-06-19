using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Provides a place for global <see cref="Path"/> data and global path functionality.
    /// </summary>
    public class PathData
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the point buffer is empty. 
        /// Yields true if there are only two points and they are the same.
        /// </summary>
        private bool BufferEmpty => PointBuffer.Count == 0;

        /// <summary>
        /// Gets a value indicating whether the point buffer currently contains 
        /// points that will not amount to any visible path.
        /// </summary>
        public bool BufferRedundant
        {
            get
            {
                var pointArray = PointBuffer.ToArray();
                var pointArrayReversed = pointArray.Reverse();

                if (Paths.Any(path => path.Lines == pointArray || path.Lines == pointArrayReversed))
                {
                    return true;
                }

                if (PointBuffer.Count == 0) return true;
                return PointBuffer.All(b => PointBuffer[0] == b);
            }
        }

        /// <summary>
        /// Gets or sets the last draw position. 
        /// </summary>
        public PointF LastDrawPosition { get; set; }

        /// <summary>
        /// Gets a list of unsubmitted points.
        /// </summary>
        public List<PointF> PointBuffer { get; }

        /// <summary>
        /// A list of submitted paths to be drawn.
        /// </summary>
        private readonly List<Path> paths = new List<Path>();
        /// <summary>
        /// Gets paths that have been submitted.
        /// </summary>
        public IEnumerable<Path> Paths => paths;

        /// <summary>
        /// Gets all path points, even those from an unsubmitted path.
        /// </summary>
        public IEnumerable<PointF> AllPoints
        {
            get
            {
                var pointStack = new Stack<PointF>();

                foreach (PointF pos in paths.SelectMany(path => path.Lines))
                {
                    pointStack.Push(pos);
                }

                foreach (PointF pos in PointBuffer)
                    pointStack.Push(pos);

                return pointStack.ToArray();
            }
        }
        #endregion

        /// <summary>
        /// Occurs when a <see cref="Path"/> has been completed and submitted to this <see cref="PathData"/>.
        /// </summary>
        public event EventHandler PathSubmitted = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="PathData"/> class.
        /// </summary>
        public PathData()
        {
            PointBuffer = new List<PointF>();
        }

        /// <summary>
        /// Sets a point from a <see cref="PointMap"/>.
        /// </summary>
        /// <param name="map">The map to the point to set.</param>
        /// <param name="point">The new point.</param>
        public void SetPoint(PointMap map, PointF point)
        {
            paths[map.PathIndex].Lines[map.PointIndex] = point;
        }

        /// <summary>
        /// Gets a point from the <see cref="Path"/> data by way of a <see cref="PointMap"/>.
        /// </summary>
        /// <param name="map">The <see cref="Path"/> and point index.</param>
        public PointF GetPoint(PointMap map)
        {
            return paths[map.PathIndex].Lines[map.PointIndex];
        }

        /// <summary>
        /// Resets all <see cref="Path"/> data to their default values.
        /// </summary>
        public void Reset()
        {
            PointBuffer.Clear();
            paths.Clear();
            LastDrawPosition = PointF.Empty;
        }

        /// <summary>
        /// Draws a single <see cref="Path"/> at the specified index.
        /// </summary>
        public void DrawHoveredPath(Graphics graphics, int index)
        {
            var pen = (XmlPen)paths[index].Pen.Clone();
            pen.Color = Color.White;
            graphics.DrawLines(pen, paths[index].Lines);
        }

        /// <summary>
        /// Draw all submitted lines to the specified graphics object.
        /// </summary>
        /// <param name="graphics">The surface to draw to.</param>
        public void DrawSubmitted(Graphics graphics)
        {
            foreach (var path in Paths)
            {
                graphics.DrawLines(path.Pen, path.Lines);
            }
        }

        /// <summary>
        /// Draw all unsubmitted lines to a specified graphics object.
        /// </summary>
        public void DrawUnsubmitted(Graphics graphics, Pen pen)
        {
            if (PointBuffer.Count > 1)
            {
                graphics.DrawLines(pen, PointBuffer.ToArray());
            }
        }

        /// <summary>
        /// Combines a <see cref="Path"/> where the start point meets the end point of 
        /// another <see cref="Path"/>.
        /// </summary>
        public void Unify()
        {
            for (int i = 0; i < paths.Count - 1; i++)
            {
                PointF p1Last = paths[i].Lines.Last();
                PointF p2First = paths[i + 1].Lines.First();

                if (paths[i].Pen.SimilairTo(paths[i + 1].Pen) && p1Last == p2First)
                {
                    Path newPath = CombinePaths(paths[i], paths[i + 1]);
                    paths.RemoveAt(i + 1);
                    paths.RemoveAt(i);
                    paths.Insert(i, newPath);
                    i = -1;
                }
            }
        }

        /// <summary>
        /// Debug write <see cref="Path"/>s.
        /// </summary>
        public void PrintPaths()
        {
            Debug.WriteLine("");

            for (int i = 0; i < paths.Count; i++)
            {
                Debug.WriteLine("Path: " + i);

                for (int i2 = 0; i2 < paths[i].Lines.Length; i2++)
                {
                    Debug.WriteLine("Point: " + paths[i].Lines[i2]);
                }
            }
        }

        /// <summary>
        /// Offsets a specified <see cref="Path"/> by the specified amount.
        /// </summary>
        /// <param name="index">The index of the path list.</param>
        /// <param name="xOffset">The offset amount on the x axis.</param>
        /// <param name="yOffset">The offset amount on the y axis.</param>
        public void OffsetPath(int index, float xOffset, float yOffset)
        {
            for (int i = 0; i < paths[index].Lines.Length; i++)
            {
                PointF pos = paths[index].Lines[i];
                pos = new PointF(pos.X + xOffset, pos.Y + yOffset);
                paths[index].Lines[i] = pos;
            }
        }

        /// <summary>
        /// Gets the submitted <see cref="Path"/> points as well as the first point in the
        /// point buffer. The user cannot snap to other points in the point buffer
        /// because this is problematic.
        /// </summary>
        public IEnumerable<PointF> GetSnapPoints(PointF[] excludePoints)
        {
            var pointStack = new Stack<PointF>();

            for (int i = 0; i < paths.Count; i++)
            {
                foreach (PointF pos in paths[i].Lines)
                {
                    if (!excludePoints.Any(p => p == pos))
                    {
                        pointStack.Push(pos);
                    }
                }
            }

            if (!BufferEmpty)
                pointStack.Push(PointBuffer[0]);

            return pointStack.ToArray();
        }

        /// <summary>
        /// Gets the <see cref="Path"/> point nearest to the specified point.
        /// </summary>
        /// <param name="pos">The point to look around.</param>
        /// <returns>Returns the original point, if no point exist.</returns>
        public PointF GetNearPathPoint(PointF pos)
        {
            float lastDist = float.MaxValue;
            PointF closestPoint = PointF.Empty;

            foreach (PointF temp in AllPoints)
            {
                var dist = Path.DistanceTo(temp, pos);

                if (dist < lastDist)
                {
                    lastDist = dist;
                    closestPoint = temp;
                }
            }

            return lastDist == float.MaxValue ? pos : closestPoint;
        }

        /// <summary>
        /// Gets the <see cref="Path"/> point nearest to the specified point in a specific <see cref="Path"/>.
        /// </summary>
        /// <param name="position">The point to look around.</param>
        /// <param name="pathIndex">The index of the path to look around.</param>
        /// <returns>The original point, if no point exist.</returns>
        public PointF GetNearPathPoint(PointF position, int pathIndex)
        {
            float lastDist = float.MaxValue;
            PointF closestPoint = PointF.Empty;

            foreach (PointF temp in paths[pathIndex].Lines)
            {
                var dist = Path.DistanceTo(temp, position);

                if (dist < lastDist)
                {
                    lastDist = dist;
                    closestPoint = temp;
                }
            }

            return lastDist == float.MaxValue ? position : closestPoint;
        }

        /// <summary>
        /// Combines multiple <see cref="Path"/>s and removes redundant points.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private static Path CombinePaths(Path path, params Path[] appendPaths)
        {
            var pointList = new List<PointF>(path.Lines);

            foreach (Path iterated in appendPaths)
            {
                if (!iterated.Pen.SimilairTo(path.Pen))
                    throw new ArgumentException("Paths should have the same pen if they are to be combined.");

                pointList.AddRange(iterated.Lines);
            }

            // Remove redundant points
            for (int i = 0; i < pointList.Count - 1; i++)
            {
                if (pointList[i] == pointList[i + 1])
                {
                    pointList.RemoveAt(i);
                    i--;
                }
            }

            return new Path(path.Pen, pointList.ToArray());
        }

        /// <summary>
        /// Removes the very last point placed.
        /// </summary>
        public void RemoveLastPoint()
        {
            if (paths.Count == 0) return;

            var lastPath = paths[paths.Count - 1];

            if (lastPath.Lines.Length <= 2)
            {
                paths.Remove(lastPath);
            }
            else
            {
                var pointList = new List<PointF>(lastPath.Lines);
                pointList.RemoveAt(lastPath.Lines.Length - 1);
                lastPath.Lines = pointList.ToArray();
            }
        }

        /// <summary>
        /// Clear all submitted <see cref="Path"/>s.
        /// </summary>
        public void ClearSubmitted()
        {
            paths.Clear();
        }

        /// <summary>
        /// Adds a range of <see cref="Path"/>s to the path list.
        /// </summary>
        public void AddPaths(params Path[] newPaths)
        {
            if (newPaths.Length > 0)
            {
                paths.AddRange(newPaths);
                PathSubmitted(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes a submitted <see cref="Path"/> at a specified index.
        /// </summary>
        public void RemovePathAt(int index)
        {
            paths.RemoveAt(index);
        }
    }
}
