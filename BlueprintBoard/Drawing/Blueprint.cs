using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BlueprintBoard.Drawing
{
    /// <summary>
    /// Represents a blueprint document.
    /// </summary>
    [Serializable]
    public class Blueprint
    {
        /// <summary>
        /// Get or sets the paths for this blueprint.
        /// </summary>
        public Path[] Paths { get; set; }

        /// <summary>
        /// Get or sets the Grid dimensions for this blueprint.
        /// </summary>
        [XmlAttribute]
        public int GridDimension { get; set; }

        /// <summary>
        /// Get or sets the canvas size for this blueprint.
        /// </summary>
        public Size CanvasSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Blueprint"/> class.
        /// </summary>
        public Blueprint() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Blueprint"/> class,
        /// with the specified arguments.
        /// </summary>
        /// <param name="paths">The paths for this blueprint.</param>
        /// <param name="gridDims">The Grid dimensions for this blueprint.</param>
        /// <param name="canvasSize">The canvas size for this blueprint.</param>
        public Blueprint(IEnumerable<Path> paths, int gridDims, Size canvasSize)
        {
            Paths = paths.ToArray();
            GridDimension = gridDims;
            CanvasSize = canvasSize;
        }

        /// <summary>
        /// Saves this blueprint to file.
        /// </summary>
        /// <param name="fileName">The path to save to.</param>
        public void Save(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                var xml = new XmlSerializer(GetType());
                xml.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Loads a blueprint from file.
        /// </summary>
        /// <param name="fileName">The path of the file to load from.</param>
        public static Blueprint Load(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var xml = new XmlSerializer(typeof(Blueprint));
                return (Blueprint)xml.Deserialize(stream);
            }
        }
    }
}
