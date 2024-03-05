using System;
using System.Runtime.InteropServices;

namespace BLRPC.Internal;

// FUCK THE QUEST! FUCK THE QUEST! I HATE ANDROID! FUCK THE QUEST!
internal static class DllTools
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    public static extern bool FreeLibrary(IntPtr hModule);
}