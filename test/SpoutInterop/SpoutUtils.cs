
using System.Runtime.InteropServices;
using System.Text;

namespace SpoutInterop;

[StructLayout(LayoutKind.Sequential)]
public struct POINT
{
    public int X;
    public int Y;
}

public static class SpoutUtils
{
    public static string GetSDKversion()
    {
        StdString stdString = SpoutNative.spoututils_GetSDKversion();
        string result = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static string GetSpoutVersion()
    {
        StdString stdString = SpoutNative.spoututils_GetSpoutVersion();
        string result = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static bool IsLaptop()
    {
        return SpoutNative.spoututils_IsLaptop();
    }

    public static IntPtr GetCurrentModule()
    {
        return SpoutNative.spoututils_GetCurrentModule();
    }

    public static string GetExeVersion(string path)
    {
        StdString stdString = SpoutNative.spoututils_GetExeVersion(path);
        string result = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static string GetExePath()
    {
        StdString stdString = SpoutNative.spoututils_GetExePath();
        string result = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static string GetExeName()
    {
        StdString stdString = SpoutNative.spoututils_GetExeName();
        string result = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static void RemovePath(ref string path)
    {
        StdString stdString = StringToStdString(path);
        SpoutNative.spoututils_RemovePath(ref stdString);
        path = StdStringToString(stdString);
        FreeStdString(ref stdString);
    }

    public static void RemoveName(ref string path)
    {
        StdString stdString = StringToStdString(path);
        SpoutNative.spoututils_RemoveName(ref stdString);
        path = StdStringToString(stdString);
        FreeStdString(ref stdString);
    }

    public static void OpenSpoutConsole()
    {
        SpoutNative.spoututils_OpenSpoutConsole();
    }

    public static void CloseSpoutConsole(bool bWarning)
    {
        SpoutNative.spoututils_CloseSpoutConsole(bWarning);
    }

    public static void EnableSpoutLog()
    {
        SpoutNative.spoututils_EnableSpoutLog();
    }

    public static void EnableSpoutLogFile(string filename, bool bAppend)
    {
        SpoutNative.spoututils_EnableSpoutLogFile(filename, bAppend);
    }

    public static void DisableSpoutLogFile()
    {
        SpoutNative.spoututils_DisableSpoutLogFile();
    }

    public static void DisableSpoutLog()
    {
        SpoutNative.spoututils_DisableSpoutLog();
    }

    public static void SetSpoutLogLevel(SpoutLogLevel level)
    {
        SpoutNative.spoututils_SetSpoutLogLevel(level);
    }

    public static void SpoutLog(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLog(message, __arglist());
    }

    public static void SpoutLogVerbose(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLogVerbose(message, __arglist());
    }

    public static void SpoutLogNotice(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLogNotice(message, __arglist());
    }

    public static void SpoutLogWarning(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLogWarning(message, __arglist());
    }

    public static void SpoutLogError(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLogError(message, __arglist());
    }

    public static void SpoutLogFatal(string format, params object[] args)
    {
        string message = string.Format(format, args);
        SpoutNative.spoututils_SpoutLogFatal(message, __arglist());
    }

    public static int SpoutMessageBox(string message, uint dwMilliseconds)
    {
        return SpoutNative.spoututils_SpoutMessageBox(message, dwMilliseconds);
    }

    public static int SpoutMessageBox(string caption, string format, params object[] args)
    {
        string message = string.Format(format, args);
        return SpoutNative.spoututils_SpoutMessageBox(caption, message, __arglist());
    }

    public static int SpoutMessageBox(string caption, uint uType, string format, params object[] args)
    {
        string message = string.Format(format, args);
        return SpoutNative.spoututils_SpoutMessageBox(caption, uType, message, __arglist());
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint uType, uint dwMilliseconds)
    {
        return SpoutNative.spoututils_SpoutMessageBox(hwnd, message, caption, uType, dwMilliseconds);
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint uType, string instruction, uint dwMilliseconds)
    {
        return SpoutNative.spoututils_SpoutMessageBox(hwnd, message, caption, uType, instruction, dwMilliseconds);
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint uType, ref string text)
    {
        StdString stdString = StringToStdString(text);
        int result = SpoutNative.spoututils_SpoutMessageBox(hwnd, message, caption, uType, ref stdString);
        text = StdStringToString(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint uType, StdVector items, ref int selected)
    {
        return SpoutNative.spoututils_SpoutMessageBox(hwnd, message, caption, uType, items, ref selected);
    }

    public static void SpoutMessageBoxIcon(IntPtr hIcon)
    {
        SpoutNative.spoututils_SpoutMessageBoxIcon(hIcon);
    }

    public static bool SpoutMessageBoxIcon(string iconfile)
    {
        StdString stdString = StringToStdString(iconfile);
        bool result = SpoutNative.spoututils_SpoutMessageBoxIcon(stdString);
        FreeStdString(ref stdString);
        return result;
    }

    public static bool SpoutMessageBoxModeless(bool bMode)
    {
        return SpoutNative.spoututils_SpoutMessageBoxModeless(bMode);
    }

    public static void SpoutMessageBoxWindow(IntPtr hWnd)
    {
        SpoutNative.spoututils_SpoutMessageBoxWindow(hWnd);
    }

    public static void SpoutMessageBoxPosition(POINT pt)
    {
        SpoutNative.spoututils_SpoutMessageBoxPosition(pt);
    }

    public static bool CopyToClipBoard(IntPtr hwnd, string text)
    {
        return SpoutNative.spoututils_CopyToClipBoard(hwnd, text);
    }

    public static bool ReadDwordFromRegistry(IntPtr hKey, string subkey, string valuename, ref uint pValue)
    {
        return SpoutNative.spoututils_ReadDwordFromRegistry(hKey, subkey, valuename, ref pValue);
    }

    public static bool WriteDwordToRegistry(IntPtr hKey, string subkey, string valuename, uint dwValue)
    {
        return SpoutNative.spoututils_WriteDwordToRegistry(hKey, subkey, valuename, dwValue);
    }

    public static bool ReadPathFromRegistry(IntPtr hKey, string subkey, string valuename, StringBuilder filepath, uint dwSize)
    {
        return SpoutNative.spoututils_ReadPathFromRegistry(hKey, subkey, valuename, filepath, dwSize);
    }

    public static bool WritePathToRegistry(IntPtr hKey, string subkey, string valuename, string filepath)
    {
        return SpoutNative.spoututils_WritePathToRegistry(hKey, subkey, valuename, filepath);
    }

    public static bool WriteBinaryToRegistry(IntPtr hKey, string subkey, string valuename, IntPtr hexdata, uint nChars)
    {
        return SpoutNative.spoututils_WriteBinaryToRegistry(hKey, subkey, valuename, hexdata, nChars);
    }

    public static bool RemovePathFromRegistry(IntPtr hKey, string subkey, string valuename)
    {
        return SpoutNative.spoututils_RemovePathFromRegistry(hKey, subkey, valuename);
    }

    public static bool RemoveSubKey(IntPtr hKey, string subkey)
    {
        return SpoutNative.spoututils_RemoveSubKey(hKey, subkey);
    }

    public static bool FindSubKey(IntPtr hKey, string subkey)
    {
        return SpoutNative.spoututils_FindSubKey(hKey, subkey);
    }

    public static double ElapsedMicroseconds()
    {
        return SpoutNative.spoututils_ElapsedMicroseconds();
    }

    public static double GetRefreshRate()
    {
        return SpoutNative.spoututils_GetRefreshRate();
    }

    public static bool ExecuteProcess(string path, string commandline)
    {
        return SpoutNative.spoututils_ExecuteProcess(path, commandline);
    }

    public static bool OpenSpoutPanel(string message)
    {
        return SpoutNative.spoututils_OpenSpoutPanel(message);
    }

    private static string StdStringToString(StdString stdString)
    {
        if (stdString.Size <= 15) // SSO case
        {
            return Encoding.ASCII.GetString(stdString.Buf, 0, (int)stdString.Size);
        }
        else // Heap-allocated case
        {
            // Pin Buf to get its memory address
            GCHandle handle = GCHandle.Alloc(stdString.Buf, GCHandleType.Pinned);
            try
            {
                // Read the pointer from the first 8 bytes of Buf
                IntPtr stringPtr = Marshal.ReadIntPtr(handle.AddrOfPinnedObject());
                if (stringPtr == IntPtr.Zero)
                    return string.Empty;

                byte[] buffer = new byte[stdString.Size];
                Marshal.Copy(stringPtr, buffer, 0, (int)stdString.Size);
                return Encoding.ASCII.GetString(buffer);
            }
            finally
            {
                handle.Free();
            }
        }
    }

    private static StdString StringToStdString(string value)
    {
        StdString stdString = new StdString
        {
            Buf = new byte[16],
            Size = 0,
            Capacity = 15
        };

        if (string.IsNullOrEmpty(value))
        {
            return stdString;
        }

        byte[] bytes = Encoding.ASCII.GetBytes(value);
        stdString.Size = (ulong)bytes.Length;

        if (bytes.Length <= 15) // SSO case
        {
            Array.Copy(bytes, stdString.Buf, bytes.Length);
            stdString.Capacity = 15;
        }
        else // Heap-allocated case
        {
            IntPtr heapPtr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, heapPtr, bytes.Length);
            // Store the pointer in the first 8 bytes of Buf
            GCHandle handle = GCHandle.Alloc(stdString.Buf, GCHandleType.Pinned);
            try
            {
                Span<byte> bufSpan = new Span<byte>(stdString.Buf, 0, 8);
                Span<IntPtr> ptrSpan = new Span<IntPtr>(new[] { heapPtr }, 0, 1);
                MemoryMarshal.AsBytes(ptrSpan).CopyTo(bufSpan);
            }
            finally
            {
                handle.Free();
            }
            stdString.Capacity = (ulong)bytes.Length;
        }

        return stdString;
    }

    private static void FreeStdString(ref StdString stdString)
    {
        if (stdString.Size > 15)
        {
            // Pin Buf to get its memory address
            GCHandle handle = GCHandle.Alloc(stdString.Buf, GCHandleType.Pinned);
            try
            {
                // Read the pointer from the first 8 bytes of Buf
                IntPtr stringPtr = Marshal.ReadIntPtr(handle.AddrOfPinnedObject());
                if (stringPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(stringPtr);
                }
            }
            finally
            {
                handle.Free();
            }
            stdString.Buf = new byte[16];
            stdString.Size = 0;
            stdString.Capacity = 15;
        }
    }
}
