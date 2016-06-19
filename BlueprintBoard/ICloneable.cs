namespace BlueprintBoard
{
    /// <summary>
    /// Implements cloning functionality.
    /// </summary>
    /// <typeparam name="T">The Type of object that will be cloned.</typeparam>
    internal interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        T Clone();
    }
}
