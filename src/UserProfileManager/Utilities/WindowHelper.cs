using System.Runtime.InteropServices;

namespace UserProfileManager.Utilities;

public static class WindowHelper
{
    private const int SW_RESTORE = 9;

    [DllImport("user32.dll", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode, ExactSpelling = true)]
    private static extern IntPtr FindWindow( string? lpClassName,string? lpWindowName);

    [DllImport( "user32.dll", EntryPoint = "ShowWindow", ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport( "user32.dll",EntryPoint = "SetForegroundWindow",ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    public static void BringToFront(string windowTitle)
    {
        IntPtr hWnd = FindWindow(null, windowTitle);

        if (hWnd == IntPtr.Zero)
        {
            return;
        }

        ShowWindow(hWnd, SW_RESTORE);
        SetForegroundWindow(hWnd);
    }
}