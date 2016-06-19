using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BlueprintBoard
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern short GetKeyState(Keys keys);
    }
}
