using System.Windows.Forms;

namespace BlueprintBoard
{
    /// <summary>
    /// Provides keyboard related functionality.
    /// </summary>
    static class Keyboard
    {
        /// <summary>
        /// Gets a value indicating whether the shift key is currently depressed.
        /// </summary>
        public static bool Shifting => IsKeyPressed(Keys.ShiftKey);

        /// <summary>
        /// Gets a value indicating whether the control key is currently depressed.
        /// </summary>
        public static bool Controlling => IsKeyPressed(Keys.ControlKey);

        /// <summary>
        /// Gets a value indicating whether the alt key is currently depressed.
        /// </summary>
        public static bool Alting => IsKeyPressed(Keys.Alt);

        /// <summary>
        /// Gets a value indicating whether a certain key is currently down.
        /// </summary>
        private static bool IsKeyPressed(Keys key)
        {
            short result = NativeMethods.GetKeyState(key);

            switch (result)
            {
                case 0: return false;
                case 1: return false;
                default: return true;
            }
        }
    }
}
