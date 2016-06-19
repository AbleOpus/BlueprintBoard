using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BlueprintBoardDemo.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.radioErasePath = new System.Windows.Forms.RadioButton();
            this.radioErasePoint = new System.Windows.Forms.RadioButton();
            this.numberBoxGridDimensions = new System.Windows.Forms.NumericUpDown();
            this.trackBarStroke = new System.Windows.Forms.TrackBar();
            this.numberBoxSnapDistance = new System.Windows.Forms.NumericUpDown();
            this.trackBarEraserRadius = new System.Windows.Forms.TrackBar();
            this.numberBoxLineDetail = new System.Windows.Forms.NumericUpDown();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExportAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemViewVertices = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSharpSource = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditDisabled = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditPath = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawLine = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDrawFreeform = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEraserShape = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEraserShapeSquare = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEraserShapeCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDashStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDashStyleNone = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDashStyleDash = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDashStyleDot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAutoUnify = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxGridSnap = new System.Windows.Forms.CheckBox();
            this.checkBoxPathSnap = new System.Windows.Forms.CheckBox();
            this.numberBoxCanvasWidth = new System.Windows.Forms.NumericUpDown();
            this.numberBoxCanvasHeight = new System.Windows.Forms.NumericUpDown();
            this.blueprintBoard = new BlueprintBoard.Forms.BlueprintBoard();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            groupBox1 = new System.Windows.Forms.GroupBox();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxGridDimensions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxSnapDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEraserRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxLineDetail)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxCanvasWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxCanvasHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(600, 145);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(83, 13);
            label2.TabIndex = 8;
            label2.Text = "Grid Dimensions";
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(626, 244);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(72, 13);
            label3.TabIndex = 11;
            label3.Text = "Stroke Width:";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(606, 119);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 13);
            label1.TabIndex = 3;
            label1.Text = "Snap Distance";
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(626, 180);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(76, 13);
            label4.TabIndex = 14;
            label4.Text = "Eraser Radius:";
            // 
            // label5
            // 
            label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(602, 93);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 13);
            label5.TabIndex = 17;
            label5.Text = "FreeForm Detail";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new System.Drawing.Size(160, 6);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.radioErasePath);
            groupBox1.Controls.Add(this.radioErasePoint);
            groupBox1.Location = new System.Drawing.Point(603, 368);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(127, 50);
            groupBox1.TabIndex = 32;
            groupBox1.TabStop = false;
            groupBox1.Text = "Eraser Mode";
            // 
            // radioErasePath
            // 
            this.radioErasePath.AutoSize = true;
            this.radioErasePath.Location = new System.Drawing.Point(61, 19);
            this.radioErasePath.Name = "radioErasePath";
            this.radioErasePath.Size = new System.Drawing.Size(47, 17);
            this.radioErasePath.TabIndex = 1;
            this.radioErasePath.TabStop = true;
            this.radioErasePath.Text = "Path";
            this.toolTip.SetToolTip(this.radioErasePath, "The eraser will erase an entire path if one of the paths\'\r\npoints fall within the" +
        " eraser\'s region.");
            this.radioErasePath.UseVisualStyleBackColor = true;
            this.radioErasePath.CheckedChanged += new System.EventHandler(this.radioErasePath_CheckedChanged);
            // 
            // radioErasePoint
            // 
            this.radioErasePoint.AutoSize = true;
            this.radioErasePoint.Location = new System.Drawing.Point(6, 19);
            this.radioErasePoint.Name = "radioErasePoint";
            this.radioErasePoint.Size = new System.Drawing.Size(49, 17);
            this.radioErasePoint.TabIndex = 0;
            this.radioErasePoint.TabStop = true;
            this.radioErasePoint.Text = "Point";
            this.toolTip.SetToolTip(this.radioErasePoint, "The eraser will erase points that fall within its region.");
            this.radioErasePoint.UseVisualStyleBackColor = true;
            this.radioErasePoint.CheckedChanged += new System.EventHandler(this.radioErasePoint_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // label6
            // 
            label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(609, 41);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(74, 13);
            label6.TabIndex = 34;
            label6.Text = "Canvas Width";
            // 
            // label7
            // 
            label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(606, 67);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(77, 13);
            label7.TabIndex = 38;
            label7.Text = "Canvas Height";
            // 
            // numberBoxGridDimensions
            // 
            this.numberBoxGridDimensions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxGridDimensions.Location = new System.Drawing.Point(689, 143);
            this.numberBoxGridDimensions.Name = "numberBoxGridDimensions";
            this.numberBoxGridDimensions.Size = new System.Drawing.Size(59, 20);
            this.numberBoxGridDimensions.TabIndex = 7;
            // 
            // trackBarStroke
            // 
            this.trackBarStroke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarStroke.Location = new System.Drawing.Point(575, 260);
            this.trackBarStroke.Maximum = 70;
            this.trackBarStroke.Minimum = 1;
            this.trackBarStroke.Name = "trackBarStroke";
            this.trackBarStroke.Size = new System.Drawing.Size(173, 45);
            this.trackBarStroke.TabIndex = 10;
            this.trackBarStroke.TickFrequency = 10;
            this.trackBarStroke.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarStroke.Value = 1;
            // 
            // numberBoxSnapDistance
            // 
            this.numberBoxSnapDistance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxSnapDistance.Location = new System.Drawing.Point(689, 117);
            this.numberBoxSnapDistance.Name = "numberBoxSnapDistance";
            this.numberBoxSnapDistance.Size = new System.Drawing.Size(59, 20);
            this.numberBoxSnapDistance.TabIndex = 2;
            this.toolTip.SetToolTip(this.numberBoxSnapDistance, "The distance to snap the start and end of drawn paths\r\nto either grid intersectio" +
        "ns or other paths.");
            // 
            // trackBarEraserRadius
            // 
            this.trackBarEraserRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarEraserRadius.Location = new System.Drawing.Point(575, 196);
            this.trackBarEraserRadius.Maximum = 100;
            this.trackBarEraserRadius.Minimum = 1;
            this.trackBarEraserRadius.Name = "trackBarEraserRadius";
            this.trackBarEraserRadius.Size = new System.Drawing.Size(173, 45);
            this.trackBarEraserRadius.TabIndex = 13;
            this.trackBarEraserRadius.TickFrequency = 10;
            this.trackBarEraserRadius.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarEraserRadius.Value = 1;
            // 
            // numberBoxLineDetail
            // 
            this.numberBoxLineDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxLineDetail.Location = new System.Drawing.Point(689, 91);
            this.numberBoxLineDetail.Name = "numberBoxLineDetail";
            this.numberBoxLineDetail.Size = new System.Drawing.Size(59, 20);
            this.numberBoxLineDetail.TabIndex = 16;
            this.toolTip.SetToolTip(this.numberBoxLineDetail, "The fidelity at which to draw free-form paths.");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemEdit,
            this.menuItemView,
            this.menuItemTools});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(760, 24);
            this.menuStrip.TabIndex = 28;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            toolStripSeparator,
            this.menuItemSaveAs,
            this.menuItemExportAs,
            toolStripSeparator1,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Image = ((System.Drawing.Image)(resources.GetObject("menuItemNew.Image")));
            this.menuItemNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNew.Size = new System.Drawing.Size(163, 22);
            this.menuItemNew.Text = "&New";
            this.menuItemNew.ToolTipText = "Clears the current canvas.";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = ((System.Drawing.Image)(resources.GetObject("menuItemOpen.Image")));
            this.menuItemOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(163, 22);
            this.menuItemOpen.Text = "&Open";
            this.menuItemOpen.ToolTipText = "Opens an existing blueprint XML file.";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSaveAs.Size = new System.Drawing.Size(163, 22);
            this.menuItemSaveAs.Text = "Save As...";
            this.menuItemSaveAs.ToolTipText = "Saves the blueprint to an XML file.";
            this.menuItemSaveAs.Click += new System.EventHandler(this.menuItemSaveAs_Click);
            // 
            // menuItemExportAs
            // 
            this.menuItemExportAs.Name = "menuItemExportAs";
            this.menuItemExportAs.Size = new System.Drawing.Size(163, 22);
            this.menuItemExportAs.Text = "Export As...";
            this.menuItemExportAs.ToolTipText = "Exports the canvas as a bitmap file.";
            this.menuItemExportAs.Click += new System.EventHandler(this.menuItemExportAs_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeyDisplayString = "Alt + F4";
            this.menuItemExit.Size = new System.Drawing.Size(163, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUndo,
            this.menuItemRedo});
            this.menuItemEdit.Name = "menuItemEdit";
            this.menuItemEdit.Size = new System.Drawing.Size(39, 20);
            this.menuItemEdit.Text = "&Edit";
            // 
            // menuItemUndo
            // 
            this.menuItemUndo.Enabled = false;
            this.menuItemUndo.Name = "menuItemUndo";
            this.menuItemUndo.ShortcutKeyDisplayString = "Ctrl+Z";
            this.menuItemUndo.Size = new System.Drawing.Size(152, 22);
            this.menuItemUndo.Text = "&Undo";
            this.menuItemUndo.Click += new System.EventHandler(this.menuItemUndo_Click);
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Enabled = false;
            this.menuItemRedo.Name = "menuItemRedo";
            this.menuItemRedo.ShortcutKeyDisplayString = "Ctrl+Y";
            this.menuItemRedo.Size = new System.Drawing.Size(152, 22);
            this.menuItemRedo.Text = "&Redo";
            this.menuItemRedo.Click += new System.EventHandler(this.menuItemRedo_Click);
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemViewGrid,
            this.menuItemViewVertices,
            toolStripSeparator2,
            this.menuItemSharpSource});
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(44, 20);
            this.menuItemView.Text = "View";
            // 
            // menuItemViewGrid
            // 
            this.menuItemViewGrid.CheckOnClick = true;
            this.menuItemViewGrid.Name = "menuItemViewGrid";
            this.menuItemViewGrid.Size = new System.Drawing.Size(168, 22);
            this.menuItemViewGrid.Text = "Grid";
            this.menuItemViewGrid.CheckedChanged += new System.EventHandler(this.menuItemViewGrid_CheckedChanged);
            // 
            // menuItemViewVertices
            // 
            this.menuItemViewVertices.CheckOnClick = true;
            this.menuItemViewVertices.Name = "menuItemViewVertices";
            this.menuItemViewVertices.Size = new System.Drawing.Size(168, 22);
            this.menuItemViewVertices.Text = "Vertices";
            this.menuItemViewVertices.ToolTipText = "Shows the vertices of drawn paths. This is important to\r\nhave enabled for editing" +
    " modes.";
            this.menuItemViewVertices.CheckedChanged += new System.EventHandler(this.menuItemViewVertices_CheckedChanged);
            // 
            // menuItemSharpSource
            // 
            this.menuItemSharpSource.Name = "menuItemSharpSource";
            this.menuItemSharpSource.Size = new System.Drawing.Size(168, 22);
            this.menuItemSharpSource.Text = "C# Source Code...";
            this.menuItemSharpSource.ToolTipText = "Views a C# representation of the vertexes in the canvas.";
            this.menuItemSharpSource.Click += new System.EventHandler(this.menuItemSharpSource_Click);
            // 
            // menuItemTools
            // 
            this.menuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditMode,
            this.menuItemDrawMode,
            this.menuItemEraserShape,
            this.menuItemDashStyle,
            toolStripSeparator5,
            this.menuItemAutoUnify});
            this.menuItemTools.Name = "menuItemTools";
            this.menuItemTools.Size = new System.Drawing.Size(47, 20);
            this.menuItemTools.Text = "&Tools";
            // 
            // menuItemEditMode
            // 
            this.menuItemEditMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditDisabled,
            this.menuItemEditPoint,
            this.menuItemEditPath});
            this.menuItemEditMode.Name = "menuItemEditMode";
            this.menuItemEditMode.Size = new System.Drawing.Size(152, 22);
            this.menuItemEditMode.Text = "Edit Mode";
            // 
            // menuItemEditDisabled
            // 
            this.menuItemEditDisabled.Name = "menuItemEditDisabled";
            this.menuItemEditDisabled.Size = new System.Drawing.Size(152, 22);
            this.menuItemEditDisabled.Text = "Disabled";
            this.menuItemEditDisabled.ToolTipText = "No editing allowed.";
            this.menuItemEditDisabled.Click += new System.EventHandler(this.menuItemEditModeItems_Click);
            // 
            // menuItemEditPoint
            // 
            this.menuItemEditPoint.Name = "menuItemEditPoint";
            this.menuItemEditPoint.Size = new System.Drawing.Size(152, 22);
            this.menuItemEditPoint.Text = "Point";
            this.menuItemEditPoint.ToolTipText = "Allows individual points to be moved.\r\nHold down shift and drag point by their an" +
    "chors to move them.";
            this.menuItemEditPoint.Click += new System.EventHandler(this.menuItemEditModeItems_Click);
            // 
            // menuItemEditPath
            // 
            this.menuItemEditPath.Name = "menuItemEditPath";
            this.menuItemEditPath.Size = new System.Drawing.Size(152, 22);
            this.menuItemEditPath.Text = "Path";
            this.menuItemEditPath.ToolTipText = "Allows paths to be moved.\r\nHold down shift and drag point on a path to move the e" +
    "ntire path.";
            this.menuItemEditPath.Click += new System.EventHandler(this.menuItemEditModeItems_Click);
            // 
            // menuItemDrawMode
            // 
            this.menuItemDrawMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDrawLine,
            this.menuItemDrawFreeform});
            this.menuItemDrawMode.Name = "menuItemDrawMode";
            this.menuItemDrawMode.Size = new System.Drawing.Size(152, 22);
            this.menuItemDrawMode.Text = "Draw Mode";
            // 
            // menuItemDrawLine
            // 
            this.menuItemDrawLine.Name = "menuItemDrawLine";
            this.menuItemDrawLine.Size = new System.Drawing.Size(152, 22);
            this.menuItemDrawLine.Tag = "0";
            this.menuItemDrawLine.Text = "Line";
            this.menuItemDrawLine.ToolTipText = "Draws one straight line while the mouse is down.";
            this.menuItemDrawLine.Click += new System.EventHandler(this.menuItemDrawModeItems_Click);
            // 
            // menuItemDrawFreeform
            // 
            this.menuItemDrawFreeform.Name = "menuItemDrawFreeform";
            this.menuItemDrawFreeform.Size = new System.Drawing.Size(152, 22);
            this.menuItemDrawFreeform.Text = "Freeform";
            this.menuItemDrawFreeform.ToolTipText = "Draws multiple attached lines with the configured\r\nfidelity to give a free-form d" +
    "rawing effect.";
            this.menuItemDrawFreeform.Click += new System.EventHandler(this.menuItemDrawModeItems_Click);
            // 
            // menuItemEraserShape
            // 
            this.menuItemEraserShape.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEraserShapeSquare,
            this.menuItemEraserShapeCircle});
            this.menuItemEraserShape.Name = "menuItemEraserShape";
            this.menuItemEraserShape.Size = new System.Drawing.Size(152, 22);
            this.menuItemEraserShape.Text = "Eraser Shape";
            // 
            // menuItemEraserShapeSquare
            // 
            this.menuItemEraserShapeSquare.Name = "menuItemEraserShapeSquare";
            this.menuItemEraserShapeSquare.Size = new System.Drawing.Size(152, 22);
            this.menuItemEraserShapeSquare.Text = "Square";
            this.menuItemEraserShapeSquare.ToolTipText = "The eraser will have a square region.";
            this.menuItemEraserShapeSquare.Click += new System.EventHandler(this.menuItemEraserShapeItems_Click);
            // 
            // menuItemEraserShapeCircle
            // 
            this.menuItemEraserShapeCircle.Name = "menuItemEraserShapeCircle";
            this.menuItemEraserShapeCircle.Size = new System.Drawing.Size(152, 22);
            this.menuItemEraserShapeCircle.Text = "Circle";
            this.menuItemEraserShapeCircle.ToolTipText = "The eraser will have a circular region.";
            this.menuItemEraserShapeCircle.Click += new System.EventHandler(this.menuItemEraserShapeItems_Click);
            // 
            // menuItemDashStyle
            // 
            this.menuItemDashStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDashStyleNone,
            this.menuItemDashStyleDash,
            this.menuItemDashStyleDot});
            this.menuItemDashStyle.Name = "menuItemDashStyle";
            this.menuItemDashStyle.Size = new System.Drawing.Size(152, 22);
            this.menuItemDashStyle.Text = "Dash Style";
            // 
            // menuItemDashStyleNone
            // 
            this.menuItemDashStyleNone.Name = "menuItemDashStyleNone";
            this.menuItemDashStyleNone.Size = new System.Drawing.Size(103, 22);
            this.menuItemDashStyleNone.Text = "None";
            this.menuItemDashStyleNone.Click += new System.EventHandler(this.menuItemDashStyleItems_Click);
            // 
            // menuItemDashStyleDash
            // 
            this.menuItemDashStyleDash.Name = "menuItemDashStyleDash";
            this.menuItemDashStyleDash.Size = new System.Drawing.Size(152, 22);
            this.menuItemDashStyleDash.Text = "Dash";
            this.menuItemDashStyleDash.Click += new System.EventHandler(this.menuItemDashStyleItems_Click);
            // 
            // menuItemDashStyleDot
            // 
            this.menuItemDashStyleDot.Name = "menuItemDashStyleDot";
            this.menuItemDashStyleDot.Size = new System.Drawing.Size(103, 22);
            this.menuItemDashStyleDot.Text = "Dot";
            this.menuItemDashStyleDot.Click += new System.EventHandler(this.menuItemDashStyleItems_Click);
            // 
            // menuItemAutoUnify
            // 
            this.menuItemAutoUnify.CheckOnClick = true;
            this.menuItemAutoUnify.Name = "menuItemAutoUnify";
            this.menuItemAutoUnify.Size = new System.Drawing.Size(152, 22);
            this.menuItemAutoUnify.Text = "Auto Unify";
            this.menuItemAutoUnify.ToolTipText = "Automatically combines paths when possible.";
            this.menuItemAutoUnify.CheckedChanged += new System.EventHandler(this.menuItemAutoUnify_CheckedChanged);
            // 
            // checkBoxGridSnap
            // 
            this.checkBoxGridSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxGridSnap.AutoSize = true;
            this.checkBoxGridSnap.Location = new System.Drawing.Point(625, 311);
            this.checkBoxGridSnap.Name = "checkBoxGridSnap";
            this.checkBoxGridSnap.Size = new System.Drawing.Size(73, 17);
            this.checkBoxGridSnap.TabIndex = 29;
            this.checkBoxGridSnap.Text = "Grid Snap";
            this.toolTip.SetToolTip(this.checkBoxGridSnap, "The start and end of drawn paths will snap to grid intersections.");
            this.checkBoxGridSnap.UseVisualStyleBackColor = true;
            this.checkBoxGridSnap.CheckedChanged += new System.EventHandler(this.checkBoxGridSnap_CheckedChanged);
            // 
            // checkBoxPathSnap
            // 
            this.checkBoxPathSnap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPathSnap.AutoSize = true;
            this.checkBoxPathSnap.Location = new System.Drawing.Point(625, 334);
            this.checkBoxPathSnap.Name = "checkBoxPathSnap";
            this.checkBoxPathSnap.Size = new System.Drawing.Size(76, 17);
            this.checkBoxPathSnap.TabIndex = 30;
            this.checkBoxPathSnap.Text = "Path Snap";
            this.toolTip.SetToolTip(this.checkBoxPathSnap, "The start and end of drawn paths will snap to the start or end of another path.");
            this.checkBoxPathSnap.UseVisualStyleBackColor = true;
            this.checkBoxPathSnap.CheckedChanged += new System.EventHandler(this.checkBoxPathSnap_CheckedChanged);
            // 
            // numberBoxCanvasWidth
            // 
            this.numberBoxCanvasWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxCanvasWidth.Location = new System.Drawing.Point(689, 39);
            this.numberBoxCanvasWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberBoxCanvasWidth.Name = "numberBoxCanvasWidth";
            this.numberBoxCanvasWidth.Size = new System.Drawing.Size(59, 20);
            this.numberBoxCanvasWidth.TabIndex = 33;
            // 
            // numberBoxCanvasHeight
            // 
            this.numberBoxCanvasHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxCanvasHeight.Location = new System.Drawing.Point(689, 65);
            this.numberBoxCanvasHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numberBoxCanvasHeight.Name = "numberBoxCanvasHeight";
            this.numberBoxCanvasHeight.Size = new System.Drawing.Size(59, 20);
            this.numberBoxCanvasHeight.TabIndex = 37;
            // 
            // blueprintBoard
            // 
            this.blueprintBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blueprintBoard.GridDimensions = 70;
            this.blueprintBoard.Location = new System.Drawing.Point(12, 27);
            this.blueprintBoard.Name = "blueprintBoard";
            this.blueprintBoard.Size = new System.Drawing.Size(557, 528);
            this.blueprintBoard.TabIndex = 31;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(760, 567);
            this.Controls.Add(this.numberBoxCanvasHeight);
            this.Controls.Add(label7);
            this.Controls.Add(this.numberBoxCanvasWidth);
            this.Controls.Add(label6);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.blueprintBoard);
            this.Controls.Add(this.checkBoxPathSnap);
            this.Controls.Add(this.checkBoxGridSnap);
            this.Controls.Add(this.numberBoxLineDetail);
            this.Controls.Add(label5);
            this.Controls.Add(this.numberBoxSnapDistance);
            this.Controls.Add(label1);
            this.Controls.Add(this.numberBoxGridDimensions);
            this.Controls.Add(label2);
            this.Controls.Add(this.trackBarEraserRadius);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(this.trackBarStroke);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(467, 327);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "BlueprintBoard Demo";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxGridDimensions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxSnapDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarEraserRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxLineDetail)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxCanvasWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxCanvasHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericUpDown numberBoxGridDimensions;
        private TrackBar trackBarStroke;
        private NumericUpDown numberBoxSnapDistance;
        private TrackBar trackBarEraserRadius;
        private NumericUpDown numberBoxLineDetail;
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuItemFile;
        private ToolStripMenuItem menuItemNew;
        private ToolStripMenuItem menuItemOpen;
        private ToolStripMenuItem menuItemExportAs;
        private ToolStripMenuItem menuItemExit;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemUndo;
        private ToolStripMenuItem menuItemRedo;
        private ToolStripMenuItem menuItemTools;
        private ToolStripMenuItem menuItemEditMode;
        private ToolStripMenuItem menuItemEditDisabled;
        private ToolStripMenuItem menuItemEditPoint;
        private ToolStripMenuItem menuItemEditPath;
        private ToolStripMenuItem menuItemDrawMode;
        private ToolStripMenuItem menuItemDrawLine;
        private ToolStripMenuItem menuItemDrawFreeform;
        private ToolStripMenuItem menuItemEraserShape;
        private ToolStripMenuItem menuItemView;
        private ToolStripMenuItem menuItemViewGrid;
        private ToolStripMenuItem menuItemViewVertices;
        private ToolStripMenuItem menuItemDashStyle;
        private ToolStripMenuItem menuItemDashStyleNone;
        private ToolStripMenuItem menuItemDashStyleDash;
        private ToolStripMenuItem menuItemDashStyleDot;
        private ToolStripMenuItem menuItemAutoUnify;
        private CheckBox checkBoxGridSnap;
        private CheckBox checkBoxPathSnap;
        private BlueprintBoard.Forms.BlueprintBoard blueprintBoard;
        private ToolStripMenuItem menuItemSaveAs;
        private RadioButton radioErasePath;
        private RadioButton radioErasePoint;
        private ToolStripMenuItem menuItemEraserShapeSquare;
        private ToolStripMenuItem menuItemEraserShapeCircle;
        private ToolStripMenuItem menuItemSharpSource;
        private NumericUpDown numberBoxCanvasWidth;
        private NumericUpDown numberBoxCanvasHeight;
        private ToolTip toolTip;
    }
}

