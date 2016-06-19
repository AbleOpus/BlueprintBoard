using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using BlueprintBoard;
using BlueprintBoardDemo.Properties;
using Path = System.IO.Path;

namespace BlueprintBoardDemo.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // Data bindings are done only with the below controls, as the provided menu-items
            // do not support data bindable properties.
            numberBoxCanvasWidth.DataBindings.Add("Value", blueprintBoard.Canvas, "Width", false, DataSourceUpdateMode.OnPropertyChanged);
            numberBoxCanvasHeight.DataBindings.Add("Value", blueprintBoard.Canvas, "Height", false, DataSourceUpdateMode.OnPropertyChanged);
            numberBoxSnapDistance.DataBindings.Add("Value", blueprintBoard.Canvas, "SnapDistance");
            numberBoxLineDetail.DataBindings.Add("Value", blueprintBoard.Canvas, "LineDetail");
            numberBoxGridDimensions.DataBindings.Add("Value", blueprintBoard.Canvas.Grid, "CellDimensions", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarEraserRadius.DataBindings.Add("Value", blueprintBoard.Canvas.Eraser, "Radius");
            trackBarStroke.DataBindings.Add("Value", blueprintBoard.Canvas, "Stroke");
            LoadSettings();

            blueprintBoard.Canvas.SizeChanged += delegate
            {
                // The size binding are not 2 ways, so do this:
                numberBoxCanvasWidth.Value = blueprintBoard.Canvas.Width;
                numberBoxCanvasHeight.Value = blueprintBoard.Canvas.Height;
            };

            blueprintBoard.Canvas.CanRedoChanged += delegate
            { menuItemRedo.Enabled = blueprintBoard.Canvas.CanRedo; };

            blueprintBoard.Canvas.CanUndoChanged += delegate
            { menuItemUndo.Enabled = blueprintBoard.Canvas.CanUndo; };
        }

        private void LoadSettings()
        {
            blueprintBoard.Canvas.SnapDistance = Settings.Default.SnapDistance;
            blueprintBoard.Canvas.LineDetail = Settings.Default.LineDetail;
            blueprintBoard.Canvas.Grid.CellDimensions = Settings.Default.GridDimensions;
            blueprintBoard.Canvas.Eraser.Radius = Settings.Default.EraserRadius;
            blueprintBoard.Canvas.Stroke = Settings.Default.DrawnStroke;
            blueprintBoard.Canvas.Size = Settings.Default.CanvasSize;

            numberBoxCanvasWidth.Minimum = blueprintBoard.Canvas.MinimumSize.Width;
            numberBoxCanvasHeight.Minimum = blueprintBoard.Canvas.MinimumSize.Height;

            menuItemViewGrid.Checked = Settings.Default.ShowGrid;
            menuItemAutoUnify.Checked = Settings.Default.AutoUnify;
            menuItemViewVertices.Checked = Settings.Default.DrawPathPoints;
            checkBoxGridSnap.Checked = Settings.Default.SnapOptions.HasFlag(SnapOptions.Grid);
            checkBoxPathSnap.Checked = Settings.Default.SnapOptions.HasFlag(SnapOptions.Path);

            switch (Settings.Default.EraserShape)
            {
                case Shape.Ellipse: menuItemEraserShapeCircle.PerformClick(); break;
                case Shape.Rectangle: menuItemEraserShapeSquare.PerformClick(); break;
            }

            switch (Settings.Default.EditMode)
            {
                case EditMode.Disabled: menuItemEditDisabled.PerformClick(); break;
                case EditMode.Path: menuItemEditPath.Checked = true; break;
                case EditMode.Point: menuItemEditPoint.Checked = true; break;
            }

            switch (Settings.Default.EraseMode)
            {
                case EraseMode.Path: radioErasePath.Checked = true; break;
                case EraseMode.Point: radioErasePoint.Checked = true; break;
            }

            switch (Settings.Default.DashStyle)
            {
                case DashStyle.Dash: menuItemDashStyleDash.PerformClick(); break;
                case DashStyle.Dot: menuItemDashStyleDot.PerformClick(); break;
                case DashStyle.Solid: menuItemDashStyleNone.PerformClick(); break;
            }

            switch (Settings.Default.CreateMode)
            {
                case CreateMode.FreeForm: menuItemDrawFreeform.PerformClick(); break;
                case CreateMode.StraightLine: menuItemDrawLine.PerformClick(); break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Save settings
            Settings.Default.DashStyle = blueprintBoard.Canvas.DashStyle;
            Settings.Default.EraseMode = blueprintBoard.Canvas.Eraser.Mode;
            Settings.Default.CreateMode = blueprintBoard.Canvas.CreateMode;
            Settings.Default.EditMode = blueprintBoard.Canvas.EditMode;
            Settings.Default.EraserShape = blueprintBoard.Canvas.Eraser.Shape;
            Settings.Default.CanvasSize = blueprintBoard.Canvas.Size;
            Settings.Default.SnapDistance = (int)numberBoxSnapDistance.Value;
            Settings.Default.GridDimensions = (int)numberBoxGridDimensions.Value;
            Settings.Default.LineDetail = (int)numberBoxLineDetail.Value;
            Settings.Default.SnapOptions = blueprintBoard.Canvas.SnapOptions;
            Settings.Default.DrawnStroke = trackBarStroke.Value;
            Settings.Default.EraserRadius = trackBarEraserRadius.Value;
            Settings.Default.DrawPathPoints = menuItemViewVertices.Checked;
            Settings.Default.AutoUnify = menuItemAutoUnify.Checked;
            Settings.Default.ShowGrid = menuItemViewGrid.Checked;
            Settings.Default.Save();
            base.OnClosing(e);
        }

        /// <summary>
        /// Gets the probable format of an image file, from the image file's path.
        /// </summary>
        /// <param name="path">The path to the image file.</param>
        private static ImageFormat ImageFormatFromPath(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            switch (ext)
            {
                case ".bmp": return ImageFormat.Bmp;
                case ".png": return ImageFormat.Png;
                case ".jpeg":
                case ".jpg": return ImageFormat.Jpeg;
                default: return ImageFormat.Png;
            }
        }

        /// <summary>
        /// Unchecks all checkable drop-down children of the specified <see cref="ToolStripDropDownItem"/>.
        /// </summary>
        private static void UncheckMenuItemChildren(ToolStripDropDownItem parentItem)
        {
            foreach (ToolStripMenuItem item in parentItem.DropDownItems)
            {
                item.Checked = false;
            }
        }

        private void checkBoxGridSnap_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxGridSnap.Checked)
            {
                blueprintBoard.Canvas.SnapOptions |= SnapOptions.Grid;
            }
            else
            {
                blueprintBoard.Canvas.SnapOptions &= ~SnapOptions.Grid;
            }
        }

        private void checkBoxPathSnap_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPathSnap.Checked)
            {
                blueprintBoard.Canvas.SnapOptions |= SnapOptions.Path;
            }
            else
            {
                blueprintBoard.Canvas.SnapOptions &= ~SnapOptions.Path;
            }
        }

        private void radioErasePoint_CheckedChanged(object sender, EventArgs e) =>
            blueprintBoard.Canvas.Eraser.Mode = EraseMode.Point;

        private void radioErasePath_CheckedChanged(object sender, EventArgs e) => 
            blueprintBoard.Canvas.Eraser.Mode = EraseMode.Path;

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to start a new blueprint?",
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                blueprintBoard.Canvas.Clear();
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            using (var dialogOpenFile = new OpenFileDialog())
            {
                dialogOpenFile.Filter = "XML File|*.xml|All Files|*.*";

                if (dialogOpenFile.ShowDialog() == DialogResult.OK)
                {
                    blueprintBoard.LoadBlueprint(dialogOpenFile.FileName);
                }
            }
        }

        private void menuItemSaveAs_Click(object sender, EventArgs e)
        {
            using (var dialogSaveFile = new SaveFileDialog())
            {
                dialogSaveFile.Filter = "XML File|*.xml";

                if (dialogSaveFile.ShowDialog() == DialogResult.OK)
                {
                    blueprintBoard.SaveCurrentBluepint(dialogSaveFile.FileName);
                }
            }
        }

        private void menuItemExportAs_Click(object sender, EventArgs e)
        {
            using (var dialogSaveFile = new SaveFileDialog())
            {
                dialogSaveFile.Filter = "Portable Network Graphics|*.png|Bitmap|*.bmp|JPEG|*.jpg";

                if (dialogSaveFile.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = blueprintBoard.Canvas.Export(false);
                    var format = ImageFormatFromPath(dialogSaveFile.FileName);
                    bitmap.Save(dialogSaveFile.FileName, format);
                }
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e) => 
            Close();

        private void menuItemUndo_Click(object sender, EventArgs e) =>
            blueprintBoard.Canvas.Undo();

        private void menuItemRedo_Click(object sender, EventArgs e) => 
            blueprintBoard.Canvas.Redo();

        private void menuItemViewGrid_CheckedChanged(object sender, EventArgs e) => 
            blueprintBoard.Canvas.ShowGrid = menuItemViewGrid.Checked;

        private void menuItemViewVertices_CheckedChanged(object sender, EventArgs e) => 
            blueprintBoard.Canvas.ShowPathPoints = menuItemViewVertices.Checked;

        private void menuItemSharpSource_Click(object sender, EventArgs e)
        {
            using (var dialog = new CSharpSourceDialog())
            {
                dialog.SourceCode = blueprintBoard.ToCSharpSource(true, 3);
                dialog.ShowDialog();
            }
        }

        // Handles multiple items.
        private void menuItemEditModeItems_Click(object sender, EventArgs e)
        {
            UncheckMenuItemChildren(menuItemEditMode);
            var current = (ToolStripMenuItem)sender;
            current.Checked = true;

            if (current == menuItemEditDisabled)
            {
                blueprintBoard.Canvas.EditMode = EditMode.Disabled;
            }
            else if (current == menuItemEditPath)
            {
                blueprintBoard.Canvas.EditMode = EditMode.Path;
            }
            else if (current == menuItemEditPoint)
            {
                blueprintBoard.Canvas.EditMode = EditMode.Point;
            }
        }

        // Handles multiple items.
        private void menuItemDrawModeItems_Click(object sender, EventArgs e)
        {
            UncheckMenuItemChildren(menuItemDrawMode);
            var current = (ToolStripMenuItem)sender;
            current.Checked = true;

            if (current == menuItemDrawFreeform)
            {
                blueprintBoard.Canvas.CreateMode = CreateMode.FreeForm;
            }
            else if (current == menuItemDrawLine)
            {
                blueprintBoard.Canvas.CreateMode = CreateMode.StraightLine;
            }
        }

        // Handles multiple items.
        private void menuItemEraserShapeItems_Click(object sender, EventArgs e)
        {
            UncheckMenuItemChildren(menuItemEraserShape);
            var current = (ToolStripMenuItem)sender;
            current.Checked = true;

            if (current == menuItemEraserShapeCircle)
            {
                blueprintBoard.Canvas.Eraser.Shape = Shape.Ellipse;
            }
            else if (current == menuItemEraserShapeSquare)
            {
                blueprintBoard.Canvas.Eraser.Shape = Shape.Rectangle;
            }
        }

        // Handles multiple items.
        private void menuItemDashStyleItems_Click(object sender, EventArgs e)
        {
            UncheckMenuItemChildren(menuItemDashStyle);
            var current = (ToolStripMenuItem)sender;
            current.Checked = true;

            if (current == menuItemDashStyleDash)
            {
                blueprintBoard.Canvas.DashStyle = DashStyle.Dash;
            }
            else if (current == menuItemDashStyleNone)
            {
                blueprintBoard.Canvas.DashStyle = DashStyle.Solid;
            }
            else if (current == menuItemDashStyleDot)
            {
                blueprintBoard.Canvas.DashStyle = DashStyle.Dot;
            }
        }

        private void menuItemAutoUnify_CheckedChanged(object sender, EventArgs e) => 
            blueprintBoard.Canvas.AutoUnify = menuItemAutoUnify.Checked;
    }
}