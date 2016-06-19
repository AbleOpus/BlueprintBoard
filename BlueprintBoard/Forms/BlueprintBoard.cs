using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BlueprintBoard.Drawing;

namespace BlueprintBoard.Forms
{
    /// <summary>
    /// Represents a board for designing blueprints.
    /// </summary>
    public class BlueprintBoard : Panel
    {
        private Point lastCursorPos;
        private Size lastSize;
        private bool dragging;

        #region Properties
        /// <summary>
        /// Gets or sets the style of the border.
        /// </summary>
        [Description("Indicates the border style for the control.")]
        [DefaultValue(BorderStyle.FixedSingle), Category("Appearance")]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        /// <summary>
        /// Gets the painting surface for this control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DrawingCanvas Canvas { get; } = new DrawingCanvas();

        private CanvasAnchor activeAnchor;
        /// <summary>
        /// Gets or sets the currently active anchor.
        /// </summary>
        private CanvasAnchor ActiveAnchor
        {
            get { return activeAnchor; }
            set
            {
                if (activeAnchor != value)
                {
                    activeAnchor = value;
                    SetCursorActiveAnchor();
                }
            }
        }

        private readonly AnchorStyle anchorStyle = new AnchorStyle();
        /// <summary>
        /// Gets or sets the color of the canvas control anchors.
        /// </summary>
        [Description("The color of the canvas control anchors.")]
        [DefaultValue(typeof (Color), "DimGray"), Category("Appearance")]
        public Color AnchorColor
        {
            get { return anchorStyle.Color; }
            set { anchorStyle.Color = value; }
        }

        /// <summary>
        /// Gets or sets the width and height of the grid cells.
        /// </summary>
        [Category("Appearance")]
        [Description("The width and height of the grid cells.")]
        public int GridDimensions
        {
            get { return Canvas.Grid.CellDimensions; }
            set
            {
                Canvas.Grid.CellDimensions = value;
                Invalidate();
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueprintBoard"/> class.
        /// </summary>
        public BlueprintBoard()
        {
            BorderStyle = BorderStyle.FixedSingle;
            base.BackColor = Color.FromKnownColor(KnownColor.AppWorkspace);
            SetStyle(ControlStyles.Selectable, true);
            Canvas.SizeChanged += delegate { Invalidate(); };
            Controls.Add(Canvas);
        }

        /// <summary>
        /// Converts the paths drawn to C# point array source code., with absolute positioning.
        /// </summary>
        /// <param name="usePercent">Whether to use percentages than absolute position.</param>
        /// <param name="decimalPlaces">When using percent, specifies how many decimal places the result should have.</param>
        /// <param name="idFormat">The format of the point array's variable identifier.
        /// Formatting index '0' will be the path number offset around 1.</param>
        public string ToCSharpSource(bool usePercent, int decimalPlaces = 0, string idFormat = "var path{0} = new []")
        {
            var blueprint = Canvas.CreateBlueprint();
            var paths = blueprint.Paths;
            var builder = new StringBuilder();

            for (int pathIndex = 0; pathIndex < paths.Length; pathIndex++)
            {
                builder.AppendLine(String.Format(idFormat, pathIndex + 1));
                builder.AppendLine("{");
                var pointCount = paths[pathIndex].Lines.Length;

                for (int pointIndex = 0; pointIndex < pointCount; pointIndex++)
                {
                    float x = paths[pathIndex].Lines[pointIndex].X;
                    float y = paths[pathIndex].Lines[pointIndex].Y;

                    if (usePercent)
                    {
                        x = (float) Math.Round(x/Width, decimalPlaces);
                        y = (float) Math.Round(y/Height, decimalPlaces);
                    }

                    builder.Append($@"    new PointF({x}f, {y}f)");

                    if (pointIndex == pointCount - 1)
                        builder.AppendLine();
                    else
                        builder.AppendLine(",");
                }

                builder.AppendLine("};").AppendLine();
            }


            return builder.ToString().TrimEnd();
        }

        /// <summary>
        /// Saves the current blueprint to file.
        /// </summary>
        /// <param name="fileName">The path of the file to save to.</param>
        public void SaveCurrentBluepint(string fileName)
        {
            Blueprint blueprint = Canvas.CreateBlueprint();
            blueprint.Save(fileName);
        }

        /// <summary>
        /// Exports the loaded blueprint to a bitmap.
        /// </summary>
        /// <returns>The bitmap containing the blueprint canvas.</returns>
        public Bitmap Export()
        {
            var bitmap = new Bitmap(Canvas.Width, Canvas.Height);
            Canvas.DrawToBitmap(bitmap, Canvas.ClientRectangle);
            return bitmap;
        }

        /// <summary>
        /// Loads a blueprint into the canvas from file
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        public void LoadBlueprint(string fileName)
        {
            Blueprint blueprint = Blueprint.Load(fileName);
            Canvas.LoadBlueprint(blueprint);
        }

        private void SetCursorActiveAnchor()
        {
            switch (activeAnchor)
            {
                case CanvasAnchor.None:
                    Cursor = Cursors.Default;
                    break;
                case CanvasAnchor.BottomRight:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case CanvasAnchor.Right:
                    Cursor = Cursors.SizeWE;
                    break;
                case CanvasAnchor.Bottom:
                    Cursor = Cursors.SizeNS;
                    break;
            }
        }

        #region Overrides
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DefaultValue(typeof (Color), "AppWorkspace")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate(true);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Enter"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Leave"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ContainsFocus)
            {
                var brush = new SolidBrush(anchorStyle.Color);
                e.Graphics.FillRectangle(brush, GetAnchorBounds(CanvasAnchor.BottomRight));
                e.Graphics.FillRectangle(brush, GetAnchorBounds(CanvasAnchor.Bottom));
                e.Graphics.FillRectangle(brush, GetAnchorBounds(CanvasAnchor.Right));
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                dragging = true;

                if (GetAnchorBounds(CanvasAnchor.BottomRight).Contains(e.Location))
                {
                    ActiveAnchor = CanvasAnchor.BottomRight;
                    Canvas.ShowSizeIndicator = true;
                }
                else if (GetAnchorBounds(CanvasAnchor.Bottom).Contains(e.Location))
                {
                    ActiveAnchor = CanvasAnchor.Bottom;
                    Canvas.ShowSizeIndicator = true;
                }
                else if (GetAnchorBounds(CanvasAnchor.Right).Contains(e.Location))
                {
                    ActiveAnchor = CanvasAnchor.Right;
                    Canvas.ShowSizeIndicator = true;
                }

                lastSize = Canvas.Size;
                lastCursorPos = e.Location;
            }
            else if (e.Button == MouseButtons.Right && dragging)
            {
                Canvas.Size = lastSize;
                ActiveAnchor = CanvasAnchor.None;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                switch (ActiveAnchor)
                {
                    case CanvasAnchor.BottomRight:
                        Canvas.Width += e.X - lastCursorPos.X;
                        Canvas.Height += e.Y - lastCursorPos.Y;
                        break;

                    case CanvasAnchor.Bottom:
                        Canvas.Height += e.Y - lastCursorPos.Y;
                        break;

                    case CanvasAnchor.Right:
                        Canvas.Width += e.X - lastCursorPos.X;
                        break;
                }

                lastCursorPos = e.Location;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                ActiveAnchor = CanvasAnchor.None;
                dragging = false;
                Canvas.ShowSizeIndicator = false;
            }
        }

        /// <summary>
        /// Gets the default size of the control.
        /// </summary>
        /// <returns>
        /// The default <see cref="T:System.Drawing.Size"/> of the control.
        /// </returns>
        protected override Size DefaultSize => new Size(300, 300);

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs"/> that contains the event data. </param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            Type controlType = e.Control.GetType();

            if (controlType != typeof (DrawingCanvas))
            {
                throw new Exception(
                   $@"Cannot add a ""{controlType.Name}"" directly to this control, the appropriate control type is DrawingCanvas.");
            }

            base.OnControlAdded(e);
        }

        #endregion

        /// <summary>
        /// Gets the bounds of the specified anchor.
        /// </summary>
        private Rectangle GetAnchorBounds(CanvasAnchor anchor)
        {
            switch (anchor)
            {
                case CanvasAnchor.BottomRight:
                    return new Rectangle(Canvas.Width, Canvas.Height,
                        anchorStyle.Diameter, anchorStyle.Diameter);

                case CanvasAnchor.Right:
                    int y = Canvas.Height/2 - anchorStyle.Diameter/2;
                    return new Rectangle(Canvas.Width, y,
                        anchorStyle.Diameter, anchorStyle.Diameter);

                case CanvasAnchor.Bottom:
                    int x = Canvas.Width/2 - anchorStyle.Diameter/2;
                    return new Rectangle(x, Canvas.Height,
                        anchorStyle.Diameter, anchorStyle.Diameter);

                default: return Rectangle.Empty;
            }
        }
    }
}
