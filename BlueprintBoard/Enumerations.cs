using System;

namespace BlueprintBoard
{
    /// <summary>
    /// Specifies anchor positions for adjusting the size of a canvas.
    /// </summary>
    enum CanvasAnchor
    {
        /// <summary>
        /// No anchor point specified.
        /// </summary>
        None,
        /// <summary>
        /// The right anchor point, for adjusting the width of the canvas.
        /// </summary>
        Right,
        /// <summary>
        /// The bottom anchor point, for adjusting the height of the canvas.
        /// </summary>
        Bottom,
        /// <summary>
        /// The bottom-right anchor point, for adjusting the width and height of the canvas.
        /// </summary>
        BottomRight
    }

    /// <summary>
    /// Specifies a circle, rectangle or no shape.
    /// </summary>
    public enum Shape
    {
        /// <summary>
        /// No shape.
        /// </summary>
        None,
        /// <summary>
        /// Elliptical shape.
        /// </summary>
        Ellipse,
        /// <summary>
        /// Rectangular shape.
        /// </summary>
        Rectangle
    }

    /// <summary>
    /// Specifies different snap options for line drawing.
    /// </summary>
    [Flags]
    public enum SnapOptions
    {
        /// <summary>
        /// Do not snap to anything.
        /// </summary>
        None = 0,
        /// <summary>
        /// Snap to intersecting grid points when placing the first point.
        /// </summary>
        GridAtStart = 1,
        /// <summary>
        /// Snap to intersecting grid points when finishing a path.
        /// </summary>
        GridAtEnd = 2,
        /// <summary>
        /// Snap to other path points when starting a path.
        /// </summary>
        PathAtStart = 4,
        /// <summary>
        /// Snap to other path points when finishing a path.
        /// </summary>
        PathAtEnd = 8,
        /// <summary>
        /// Snap to grid at start and end.
        /// </summary>
        Grid = GridAtStart | GridAtEnd,
        /// <summary>
        /// Snap to path at start and end.
        /// </summary>
        Path = PathAtStart | PathAtEnd,
        /// <summary>
        /// Snap to all points.
        /// </summary>
        All = Grid | Path
    }

    /// <summary>
    /// Specifies a mode of editing.
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// Editing will be disabled.
        /// </summary>
        Disabled,
        /// <summary>
        /// Shapes will be edited by point.
        /// </summary>
        Point,
        /// <summary>
        /// Shapes will be edited by path.
        /// </summary>
        Path
    }

    /// <summary>
    /// Specifies what mode to use to create objects.
    /// </summary>
    public enum CreateMode
    {
        /// <summary>
        /// Nothing can be created.
        /// </summary>
        Disabled,
        /// <summary>
        /// Paths will be created in a free-form manner.
        /// </summary>
        FreeForm,
        /// <summary>
        /// Straight lines will be created.
        /// </summary>
        StraightLine
    }

    /// <summary>
    /// Specifies how to erase with right mouse.
    /// </summary>
    public enum EraseMode
    {
        /// <summary>
        /// Do not allow erasing.
        /// </summary>
        Disable,
        /// <summary>
        /// Erase shapes by point.
        /// </summary>
        Point, 
        /// <summary>
        /// Erase shapes by path.
        /// </summary>
        Path
    }
}
