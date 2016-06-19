using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BlueprintBoard.Annotations;
using BlueprintBoard.Drawing;
using PathData = BlueprintBoard.Drawing.PathData;

namespace BlueprintBoard.Forms
{
    /// <summary>
    /// Represents a drawing canvas to doodle on.
    /// </summary>
    [DefaultEvent("PointDrawn")]
    public class DrawingCanvas : Control, INotifyPropertyChanged
    {
#if DEBUG
        private readonly Random random = new Random();
#endif
        private readonly RedoUndoManager<Path[]> redoUndo = new RedoUndoManager<Path[]>();
        private readonly PathData pathData = new PathData();
        private readonly AnchorPoints anchorPoints;
        private PointMap dragPointMap = PointMap.Empty;
        private PointMap lastHoverMap = PointMap.Empty;
        private bool erasing, pathCanceled, pointsErased;
        private Point lastPos; // Last pos, typically for drag ops
        private int pathEditIndex = -1;

        #region Properties
        /// <summary>
        /// Gets the cursor position in client coordinates.
        /// </summary>
        private Point CursorPosition => PointToClient(Cursor.Position);

        /// <summary>
        /// Gets or sets the pen used to draw with.
        /// </summary>
        [Browsable(false)]
        public Pen DrawPen { get; set; } = new Pen(Color.FromArgb(185, 197, 217), 10);

        /// <summary>
        /// Gets the eraser tool.
        /// </summary>
        [Browsable(false)]
        public Eraser Eraser { get; }

        /// <summary>
        /// Gets the grid associated with the canvas.
        /// </summary>
        [Browsable(false)]
        public Grid Grid { get; } = new Grid();

        private bool showGrid;
        /// <summary>
        /// Gets or sets a value indicating whether to show the grid.
        /// </summary>
        [Category("Appearance"), DefaultValue(false)]
        [Description("Determines whether to show the grid.")]
        public bool ShowGrid
        {
            get { return showGrid; }
            set
            {
                if (value != showGrid)
                {
                    showGrid = value;
                    OnPropertyChanged();
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the stroke of drawn lines.
        /// </summary>
        [Category("Drawing"), DefaultValue(10f)]
        [Description("Determines the stroke of drawn lines.")]
        public float Stroke
        {
            get { return DrawPen.Width; }
            set
            {
                if (value != DrawPen.Width)
                {
                    DrawPen.Width = value;
                    OnPropertyChanged();
                }
            }
        }

        private int lineDetail = 20;
        /// <summary>
        /// Gets or sets how detailed free-form drawn lines are, in pixels.
        /// </summary>
        [Category("Drawing"), DefaultValue(20)]
        [Description("How detailed free-form drawn lines are, in pixels.")]
        public int LineDetail
        {
            get { return lineDetail; }
            set
            {
                if (value != lineDetail)
                {
                    lineDetail = value;
                    OnPropertyChanged();
                }
            }
        }

        private int snapDistance = 20;
        /// <summary>
        /// Gets or sets how far a line can snap to other points.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [Category("Drawing"), DefaultValue(10)]
        [Description("How far a line can snap to other points.")]
        public int SnapDistance
        {
            get { return snapDistance; }
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than 1.");

                if (value != snapDistance)
                {
                    snapDistance = value;
                    OnPropertyChanged();
                }
            }
        }

        private Color focusedBorderColor = Color.FromArgb(80, 80, 80);
        /// <summary>
        /// Gets or sets the color of the 1px border around the control when the control has focus.
        /// </summary>
        [Description("Determines the color of the 1px border around the control when the control has focus.")]
        [Category("Appearance"), DefaultValue(typeof(Color), "80, 80, 80")]
        public Color FocusedBorderColor
        {
            get { return focusedBorderColor; }
            set
            {
                focusedBorderColor = value;
                Invalidate();
            }
        }

        private Color unfocusedborderColor = Color.Gray;
        /// <summary>
        /// Gets or sets the color of the 1px border around the control when the control does not have focus.
        /// </summary>
        [Description("Determines the color of the 1px border around the control when the control does not have focus.")]
        [Category("Appearance"), DefaultValue(typeof(Color), "Gray")]
        public Color UnFocusedBorderColor
        {
            get { return unfocusedborderColor; }
            set
            {
                unfocusedborderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dash style for drawn lines.
        /// </summary>
        [Category("Drawing"), DefaultValue(DashStyle.Solid)]
        [Description("Determines the dash style for drawn lines.")]
        public DashStyle DashStyle
        {
            get { return DrawPen.DashStyle; }
            set { DrawPen.DashStyle = value; }
        }

        /// <summary>
        /// Gets or sets the snap options for this control. This determines the snap mode.
        /// </summary>
        [Category("Drawing"), DefaultValue(SnapOptions.None)]
        [Description("Determines how snapping behaves.")]
        public SnapOptions SnapOptions { get; set; }

        private bool autoUnify;
        /// <summary>
        /// Gets or sets a value indicating whether to unify near paths after submitting them.
        /// </summary>
        [Category("Drawing"), DefaultValue(false)]
        [Description("Determines whether to unify near paths after submitting them.")]
        public bool AutoUnify
        {
            get { return autoUnify; }
            set
            {
                if (autoUnify != value)
                {
                    autoUnify = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool showPathPoints;
        /// <summary>
        /// Gets or sets a value indicating whether to show path points.
        /// </summary>
        [Category("Appearance"), DefaultValue(false)]
        [Description("Determines whether to show path points.")]
        public bool ShowPathPoints
        {
            get { return showPathPoints; }
            set
            {
                showPathPoints = value;
                Invalidate();
            }
        }

        private bool showSizeIndicator;
        /// <summary>
        /// Gets or sets a value indicating whether to show the size at the bottom right corner of the control.
        /// </summary>
        [Description("Determines whether to show the size at the bottom right corner of the control.")]
        [Category("Appearance"), DefaultValue(false)]
        public bool ShowSizeIndicator
        {
            get { return showSizeIndicator; }
            set
            {
                showSizeIndicator = value;
                Invalidate();
            }
        }

        private readonly Pen userMousePen = new Pen(Color.FromArgb(100, 255, 255, 255));
        /// <summary>
        /// Gets or sets the color of the client cursor position indicator.
        /// </summary>
        [Description("Determines the color of the client cursor position indicator.")]
        [Category("Appearance"), DefaultValue(typeof(Color), "100, 255, 255, 255")]
        public Color UserMouseColor
        {
            get { return userMousePen.Color; }
            set
            {
                userMousePen.Color = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets what is modified from right click operations.
        /// </summary>
        [Description("Determines what is modified from right click operations.")]
        [Category("Drawing"), DefaultValue(EditMode.Disabled)]
        public EditMode EditMode { get; set; }

        /// <summary>
        /// Gets or sets what is created from left click operations.
        /// </summary>
        [Description("Determines what is created from left click operations.")]
        [Category("Drawing"), DefaultValue(CreateMode.FreeForm)]
        public CreateMode CreateMode { get; set; } = CreateMode.FreeForm;

        /// <summary>
        /// Gets whether an undo can be performed.
        /// </summary>
        [Browsable(false)]
        public bool CanUndo => redoUndo.CanUndo;

        /// <summary>
        /// Gets whether a redo can be performed.
        /// </summary>
        [Browsable(false)]
        public bool CanRedo => redoUndo.CanRedo;

        /// <summary>
        /// Occurs when the value of the <see cref="CanUndo"/> property has changed.
        /// </summary>
        [Description("Occurs when the value of the CanUndo property has changed.")]
        [Category("Property Changed")]
        public event EventHandler CanUndoChanged
        {
            add { redoUndo.CanUndoChanged += value; }
            remove { redoUndo.CanUndoChanged -= value; }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="CanRedo"/> property has changed.
        /// </summary>
        [Description("Occurs when the value of the CanRedo property has changed.")]
        [Category("Property Changed")]
        public event EventHandler CanRedoChanged
        {
            add { redoUndo.CanRedoChanged += value; }
            remove { redoUndo.CanRedoChanged -= value; }
        }

        /// <summary>
        /// Occurs when a path is submitted to the canvas.
        /// </summary>
        [Description("Occurs when a path is submitted to the canvas.")]
        [Category("Action")]
        public event EventHandler PathSubmitted
        {
            add { pathData.PathSubmitted += value; }
            remove { pathData.PathSubmitted -= value; }
        }
        #endregion

        /// <summary>
        /// Occurs when the mouse hovers over an anchor.
        /// </summary>
        [Description("Occurs when the mouse hovers over an anchor.")]
        [Category("Appearance")]
        public event EventHandler<PointMap> AnchorHovered = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawingCanvas"/> class.
        /// </summary>
        public DrawingCanvas()
        {
            base.DoubleBuffered = true;
            ResizeRedraw = true;
            Eraser = new Eraser(pathData);
            anchorPoints = new AnchorPoints(pathData);
            base.BackColor = Color.FromArgb(86, 140, 202);
            base.Cursor = Cursors.Cross;
            SnapDistance = 10;
            DrawPen.StartCap = LineCap.Round;
            DrawPen.EndCap = LineCap.Round;
            DrawPen.LineJoin = LineJoin.Round;
            SetStyle(ControlStyles.Selectable, true);
            redoUndo.AddState(pathData.Paths.ToArray());
            Grid.PropertyChanged += InvalidateHandler;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Overrides
        /// <summary>
        /// Gets or sets the cursor that is displayed when the mouse pointer is over the control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.Cursor"/> that represents the cursor to display when the mouse pointer is over the control.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DefaultValue(typeof(Cursor), "Cross")]
        public override Cursor Cursor
        {
            get { return base.Cursor; }
            set { base.Cursor = value; }
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DefaultValue(typeof(Color), "86, 140, 202")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        /// <returns>
        /// The default <see cref="T:System.Drawing.Size"/> of the control.
        /// </returns>
        protected override Size DefaultSize => new Size(250, 250);

        /// <summary>
        /// Gets the length and height, in pixels, that is specified as the default minimum size of a control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> representing the size of the control.
        /// </returns>
        protected override Size DefaultMinimumSize => new Size(32, 32);

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e) =>
            DrawAll(e.Graphics, ShowGrid);

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                lastPos = e.Location;

                if (Keyboard.Shifting)
                {
                    if (EditMode == EditMode.Path)
                    {
                        pathEditIndex = anchorPoints.GetPathIndexFromPoint(e.Location);
                    }
                    else if (EditMode == EditMode.Point)
                    {
                        dragPointMap = anchorPoints.GetHoverPointMap(e.Location);
                    }

                    return;
                }

                PointF startPos = GetSnapPoint(true);

                if (CreateMode == CreateMode.FreeForm)
                {
                    anchorPoints.DrawHovered = false;
                    pathData.LastDrawPosition = startPos;
                    pathData.PointBuffer.Add(startPos);
                }
                else if (CreateMode == CreateMode.StraightLine)
                {
                    pathData.PointBuffer.Add(startPos);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Path creation canceled
                pathData.PointBuffer.Clear();
                pathCanceled = true;
            }

            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            PointMap hoverMap = anchorPoints.GetHoverPointMap(e.Location);

            if (hoverMap != lastHoverMap && anchorPoints.DrawHovered)
            {
                lastHoverMap = hoverMap;
                AnchorHovered(this, lastHoverMap);
                Invalidate();
            }

            // Only draw if beyond the line detail threshold.
            if (e.Button == MouseButtons.Left)
            {
                // If pathDrag index is not -1 then we have used shift onMouseDown.
                if (EditMode == EditMode.Path && pathEditIndex != -1)
                {
                    int xOff = e.X - lastPos.X;
                    int yOff = e.Y - lastPos.Y;
                    pathData.OffsetPath(pathEditIndex, xOff, yOff);
                }
                else if (EditMode == EditMode.Point && dragPointMap != PointMap.Empty)
                {
                    pathData.SetPoint(dragPointMap, e.Location);
                }
                else if (CreateMode == CreateMode.FreeForm &&
                    Path.DistanceTo(e.Location, pathData.LastDrawPosition) > LineDetail)
                {
                    pathData.PointBuffer.Add(e.Location);
                    pathData.LastDrawPosition = e.Location;
                }

                Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!erasing && Eraser.Cursor != null)
                    Cursor = Eraser.Cursor;

                Path[] oldPaths = ClonePaths(pathData.Paths);
                erasing = true;
                bool pointRemoved = Eraser.PerformErase(e.Location);

                if (pointRemoved)
                {
                    // if (Eraser.Mode == EraseMode.Path)
                    redoUndo.AddState(oldPaths);
                }

                Invalidate();
            }

            lastPos = e.Location;
        }

        private static Path[] ClonePaths(IEnumerable<Path> paths) =>
            paths.Select(p => p.Clone()).ToArray();

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Right)
            {
                if (pointsErased)
                {
                    redoUndo.AddState(pathData.Paths.ToArray());
                }

                erasing = false;
                pathCanceled = false;
                pointsErased = false;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (pathCanceled)
                {
                    pathCanceled = false;
                    return;
                }

                if (dragPointMap != PointMap.Empty)
                {
                    PointF excludePoint = pathData.GetPoint(dragPointMap);
                    PointF pos = GetSnapPoint(false, excludePoint);
                    pathData.SetPoint(dragPointMap, pos);
                    // We were recently dragging a point, snap it (if necessary).
                    dragPointMap = PointMap.Empty;
                    return;
                }

                if (pathEditIndex != -1)
                {
                    // We were recently dragging a path, snap it (if necessary).
                    PointF pos = GetSnapPoint(false, pathEditIndex);
                    PointF nearPoint = pathData.GetNearPathPoint(e.Location, pathEditIndex);
                    float xOff = pos.X - nearPoint.X;
                    float yOff = pos.Y - nearPoint.Y;
                    pathData.OffsetPath(pathEditIndex, xOff, yOff);
                    pathEditIndex = -1;

                    if (AutoUnify)
                        UnifyPaths();

                    Invalidate();
                    return;
                }

                if (CreateMode == CreateMode.FreeForm || CreateMode == CreateMode.StraightLine)
                {
                    CommitCurrentPath();
                }
            }

            Cursor = Cursors.Cross;
            anchorPoints.DrawHovered = true;
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs"/> that contains the event data.</param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Back: StepBack(); break;

                case Keys.Z | Keys.Control:
                    if (CanUndo) Undo();
                    break;

                case Keys.Y | Keys.Control:
                    if (CanRedo) Redo();
                    break;
            }

            base.OnPreviewKeyDown(e);
        }
        #endregion

        #region Public Helpers
        /// <summary>
        /// Draws the canvas to bitmap. The configuration of the canvas is used
        /// for the majority of the result.
        /// </summary>
        /// <param name="drawGrid">Whether to draw the grid.</param>
        public Bitmap Export(bool drawGrid)
        {
            var bitmap = new Bitmap(Width, Height);

            using (Graphics graphics = Graphics.FromImage(bitmap))
                DrawAll(graphics, drawGrid);

            return bitmap;
        }

        /// <summary>
        /// Creates a <see cref="Blueprint"/> from the current state of the <see cref="DrawingCanvas"/>.
        /// </summary>
        public Blueprint CreateBlueprint()
        {
            return new Blueprint(pathData.Paths.ToArray(), Grid.CellDimensions, Size);
        }

        /// <summary>
        /// Clears the current canvas and loads a blueprint instance.
        /// </summary>
        /// <param name="blueprint">The blueprint to load.</param>
        public void LoadBlueprint(Blueprint blueprint)
        {
            pathData.PointBuffer.Clear();
            pathData.ClearSubmitted();
            pathData.AddPaths(blueprint.Paths);
            Grid.CellDimensions = blueprint.GridDimension;
            Size = blueprint.CanvasSize;
            Invalidate();
        }

        /// <summary>
        /// Reverts to the most recent set state.
        /// </summary>
        public void Undo()
        {
            pathData.ClearSubmitted();
            pathData.AddPaths(redoUndo.Undo());
            Invalidate();
        }

        /// <summary>
        /// Reverts to the most recent undone state.
        /// </summary>
        public void Redo()
        {
            pathData.ClearSubmitted();
            pathData.AddPaths(redoUndo.Redo());
            Invalidate();
        }

        /// <summary>
        /// Adds a polygon programmatically.
        /// </summary>
        public void AddPolygon(PointF[] poly)
        {
            pathData.AddPaths(new Path(DrawPen, poly));
            Invalidate();
        }

        /// <summary>
        /// Combines a path where the start point meets the end point of another path,
        /// and in reverse as well.
        /// </summary>
        private void UnifyPaths()
        {
            pathData.Unify();
            Invalidate();
        }

        /// <summary>
        /// Removes all shapes from the canvas.
        /// </summary>
        public void Clear()
        {
            pathData.Reset();
            Invalidate();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Removes the last placed point. If the last point is the second point
        /// in a path, the entire path is removed.
        /// </summary>
        private void StepBack()
        {
            pathData.RemoveLastPoint();
            Invalidate();
        }

        /// <summary>
        /// Gets a snap point if a point is within snapping range. If no point is 
        /// close enough, the cursor position is returned.
        /// </summary>
        /// <param name="excludePoints">The points that should not be snapped to.</param>
        /// <param name="startPoint">Whether the start or end point is being snapped.</param>
        private PointF GetSnapPoint(bool startPoint, params PointF[] excludePoints)
        {
            if (SnapOptions == SnapOptions.None)
            {
                return CursorPosition;
            }

            float closest = float.MaxValue;
            PointF result = new PointF();
            var points = new List<PointF>();
            bool gridEnd = SnapOptions.HasFlag(SnapOptions.GridAtEnd);

            if (startPoint && SnapOptions.HasFlag(SnapOptions.PathAtStart) ||
                !startPoint && SnapOptions.HasFlag(SnapOptions.PathAtEnd))
            {
                points.AddRange(pathData.GetSnapPoints(excludePoints));
            }

            if (showGrid && ((startPoint && SnapOptions.HasFlag(SnapOptions.GridAtStart)) ||
                (!startPoint && gridEnd)))
            {
                // Add intersecting grid points.
                points.AddRange(Grid.GetIntersectingPoints(ClientRectangle));
            }

            foreach (PointF pos in points)
            {
                var distance = Path.DistanceTo(pos, CursorPosition);

                if (distance < closest)
                {
                    closest = distance;
                    result = pos;
                }
            }

            return (closest <= SnapDistance) ? result : CursorPosition;
        }

        /// <summary>
        /// Gets a snap point if a point is within snapping range. If no point is 
        /// close enough, the cursor position is returned.
        /// </summary>
        /// <param name="excludePathIndex">The path to exclude. This makes sure none of the points from 
        /// this path will be set as a snap point.</param>
        /// <param name="startPoint">Whether the start or end point is being snapped.</param>
        private PointF GetSnapPoint(bool startPoint, int excludePathIndex)
        {
            var lines = pathData.Paths.ElementAt(excludePathIndex).Lines;
            return GetSnapPoint(startPoint, lines);
        }

        /// <summary>
        /// Dumps the point buffer into the path list.
        /// </summary>
        private void CommitCurrentPath()
        {
            var snapPoint = GetSnapPoint(false);
            pathData.PointBuffer.Add(snapPoint);

            // So we don't submit an invisible line
            if (pathData.BufferRedundant)
            {
                pathData.PointBuffer.Clear();
                return;
            }

            // We need to clone the pen so each path has a unique one
            var pen = (Pen) DrawPen.Clone();

#if DEBUG
            pen.Color = random.NextColor();
#endif

            var path = new Path(pen, pathData.PointBuffer.ToArray());
            pathData.AddPaths(path);
            pathData.PointBuffer.Clear();

            if (AutoUnify)
                pathData.Unify();

            redoUndo.AddState(pathData.Paths.ToArray());
        }

        /// <summary>
        /// Draws all graphics for this control.
        /// </summary>
        /// <param name="graphics">The surface to draw to.</param>
        /// <param name="drawGrid">Determines whether to draw the grid.</param>
        private void DrawAll(Graphics graphics, bool drawGrid)
        {
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Point cursorPos = CursorPosition;
            if (drawGrid) Grid.Draw(graphics, ClientSize);
            pathData.DrawSubmitted(graphics);
            pathData.DrawUnsubmitted(graphics, DrawPen);

            if (lastHoverMap != PointMap.Empty && lastHoverMap.PathIndex < pathData.Paths.Count())
                pathData.DrawHoveredPath(graphics, lastHoverMap.PathIndex);

            // Draw an uncommitted line so the user can see it.
            if (pathData.PointBuffer.Count == 1 && CreateMode != CreateMode.FreeForm)
            {
                graphics.DrawLine(DrawPen, pathData.PointBuffer[0], cursorPos);
            }
            else if (erasing)
            {
                Eraser.DrawTool(cursorPos, graphics);
            }

            if (showPathPoints)
            {
                anchorPoints.Draw(graphics, cursorPos);
            }

            if (showSizeIndicator)
            {
                const int PADDING = 3;
                string text = Width + ", " + Height;
                SizeF textSize = graphics.MeasureString(text, Font);

                graphics.FillRectangle(
                    Brushes.Gray, Width - textSize.Width - PADDING,
                     Height - textSize.Height - PADDING,
                    textSize.Width + PADDING,
                    textSize.Height + PADDING);

                graphics.DrawString(text, Font, Brushes.White,
                    Width - textSize.Width - PADDING / 2,
                    Height - textSize.Height - PADDING / 2);
            }

            Color borderColor = (ContainsFocus) ? focusedBorderColor : unfocusedborderColor;
            graphics.DrawInsetBorder(borderColor, ClientSize);
        }

        /// <summary>
        /// A simple handler to invalidate this control.
        /// </summary>
        private void InvalidateHandler(object sender, EventArgs e)
        {
            Invalidate();
        }
        #endregion
    }
}