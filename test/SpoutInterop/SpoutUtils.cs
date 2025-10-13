using System.Runtime.InteropServices;

namespace SpoutInterop;

public static class SpoutUtils
{
    public static void CloseSpoutConsole(bool wait)
    {
        SpoutNative.SpoutUtils.CloseSpoutConsole(wait);
    }

    public static bool CopyToClipBoard(IntPtr hwnd, string text)
    {
        IntPtr textPtr = Marshal.StringToHGlobalAnsi(text);
        try
        {
            return SpoutNative.SpoutUtils.CopyToClipBoard(hwnd, textPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(textPtr);
        }
    }

    public static void DisableLogs()
    {
        SpoutNative.SpoutUtils.DisableLogs();
    }

    public static void DisableSpoutLog()
    {
        SpoutNative.SpoutUtils.DisableSpoutLog();
    }

    public static void DisableSpoutLogFile()
    {
        SpoutNative.SpoutUtils.DisableSpoutLogFile();
    }

    public static double ElapsedMicroseconds()
    {
        return SpoutNative.SpoutUtils.ElapsedMicroseconds();
    }

    public static void EnableLogs()
    {
        SpoutNative.SpoutUtils.EnableLogs();
    }

    public static void EnableSpoutLog(string file)
    {
        IntPtr filePtr = Marshal.StringToHGlobalAnsi(file);
        try
        {
            SpoutNative.SpoutUtils.EnableSpoutLog(filePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(filePtr);
        }
    }

    public static void EnableSpoutLogFile(string file, bool append)
    {
        IntPtr filePtr = Marshal.StringToHGlobalAnsi(file);
        try
        {
            SpoutNative.SpoutUtils.EnableSpoutLogFile(filePtr, append);
        }
        finally
        {
            Marshal.FreeHGlobal(filePtr);
        }
    }

    public static double EndTiming(bool b, bool c)
    {
        return SpoutNative.SpoutUtils.EndTiming(b, c);
    }

    public static bool FindSubKey(IntPtr hkey, string subkey)
    {
        IntPtr subkeyPtr = Marshal.StringToHGlobalAnsi(subkey);
        try
        {
            return SpoutNative.SpoutUtils.FindSubKey(hkey, subkeyPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(subkeyPtr);
        }
    }

    public static double GetCounter()
    {
        return SpoutNative.SpoutUtils.GetCounter();
    }

    public static IntPtr GetCurrentModule()
    {
        return SpoutNative.SpoutUtils.GetCurrentModule();
    }

    public static string GetExeName()
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutUtils.GetExeName(strPtr);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public static string GetExePath(bool b)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutUtils.GetExePath(strPtr, b);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public static string GetExeVersion(string path)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            SpoutNative.SpoutUtils.GetExeVersion(strPtr, pathPtr);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public static double GetRefreshRate()
    {
        return SpoutNative.SpoutUtils.GetRefreshRate();
    }

    public static string GetSDKversion(out int version)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            version = 0;
            SpoutNative.SpoutUtils.GetSDKversion(strPtr, ref version);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public static string GetSpoutLog(string file)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        IntPtr filePtr = Marshal.StringToHGlobalAnsi(file);
        try
        {
            SpoutNative.SpoutUtils.GetSpoutLog(strPtr, filePtr);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
            Marshal.FreeHGlobal(filePtr);
        }
    }

    public static string GetSpoutLogPath()
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutUtils.GetSpoutLogPath(strPtr);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public static string GetSpoutVersion(out int version)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            version = 0;
            SpoutNative.SpoutUtils.GetSpoutVersion(strPtr, ref version);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public static bool IsLaptop()
    {
        return SpoutNative.SpoutUtils.IsLaptop();
    }

    public static bool LogFileEnabled()
    {
        return SpoutNative.SpoutUtils.LogFileEnabled();
    }

    public static bool LogsEnabled()
    {
        return SpoutNative.SpoutUtils.LogsEnabled();
    }

    public static void OpenSpoutConsole(string title)
    {
        IntPtr titlePtr = Marshal.StringToHGlobalAnsi(title);
        try
        {
            SpoutNative.SpoutUtils.OpenSpoutConsole(titlePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(titlePtr);
        }
    }

    public static bool OpenSpoutLogs()
    {
        return SpoutNative.SpoutUtils.OpenSpoutLogs();
    }

    public static bool ReadDwordFromRegistry(IntPtr hkey, string subkey, string value, out uint dword)
    {
        IntPtr subkeyPtr = Marshal.StringToHGlobalAnsi(subkey);
        IntPtr valuePtr = Marshal.StringToHGlobalAnsi(value);
        try
        {
            dword = 0;
            return SpoutNative.SpoutUtils.ReadDwordFromRegistry(hkey, subkeyPtr, valuePtr, ref dword);
        }
        finally
        {
            Marshal.FreeHGlobal(subkeyPtr);
            Marshal.FreeHGlobal(valuePtr);
        }
    }

    public static bool ReadPathFromRegistry(IntPtr hkey, string subkey, string value, out string path)
    {
        IntPtr subkeyPtr = Marshal.StringToHGlobalAnsi(subkey);
        IntPtr valuePtr = Marshal.StringToHGlobalAnsi(value);
        IntPtr pathPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutUtils.ReadPathFromRegistry(hkey, subkeyPtr, valuePtr, pathPtr, 256);
            path = success ? Marshal.PtrToStringAnsi(pathPtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(subkeyPtr);
            Marshal.FreeHGlobal(valuePtr);
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public static bool RemovePathFromRegistry(IntPtr hkey, string subkey, string value)
    {
        IntPtr subkeyPtr = Marshal.StringToHGlobalAnsi(subkey);
        IntPtr valuePtr = Marshal.StringToHGlobalAnsi(value);
        try
        {
            return SpoutNative.SpoutUtils.RemovePathFromRegistry(hkey, subkeyPtr, valuePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(subkeyPtr);
            Marshal.FreeHGlobal(valuePtr);
        }
    }

    public static void RemoveSpoutLogFile(string file)
    {
        IntPtr filePtr = Marshal.StringToHGlobalAnsi(file);
        try
        {
            SpoutNative.SpoutUtils.RemoveSpoutLogFile(filePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(filePtr);
        }
    }

    public static bool RemoveSubKey(IntPtr hkey, string subkey)
    {
        IntPtr subkeyPtr = Marshal.StringToHGlobalAnsi(subkey);
        try
        {
            return SpoutNative.SpoutUtils.RemoveSubKey(hkey, subkeyPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(subkeyPtr);
        }
    }

    public static void SetSpoutLogLevel(SpoutLogLevel level)
    {
        SpoutNative.SpoutUtils.SetSpoutLogLevel(level);
    }

    public static void ShowSpoutLogs()
    {
        SpoutNative.SpoutUtils.ShowSpoutLogs();
    }

    public static void SpoutLog(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLog(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static void SpoutLogError(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLogError(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static void SpoutLogFatal(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLogFatal(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static void SpoutLogNotice(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLogNotice(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static void SpoutLogVerbose(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLogVerbose(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static void SpoutLogWarning(string format, params object[] args)
    {
        IntPtr formatPtr = Marshal.StringToHGlobalAnsi(string.Format(format, args));
        try
        {
            SpoutNative.SpoutUtils.SpoutLogWarning(formatPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(formatPtr);
        }
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint type, uint idHelp, uint language)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(message);
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        try
        {
            return SpoutNative.SpoutUtils.SpoutMessageBox1(hwnd, messagePtr, captionPtr, type, idHelp, language);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint type, ref string help)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(message);
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        try
        {
            return SpoutNative.SpoutUtils.SpoutMessageBox2(hwnd, messagePtr, captionPtr, type, ref help);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint type, uint language)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(message);
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        try
        {
            return SpoutNative.SpoutUtils.SpoutMessageBox3(hwnd, messagePtr, captionPtr, type, language);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }

    public static int SpoutMessageBox(IntPtr hwnd, string message, string caption, uint type, List<string> vector, out int result)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(message);
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        // Note: Converting List<string> to native vector is complex and may require additional marshaling logic.
        // For simplicity, assuming vector is handled by the native layer as a placeholder.
        IntPtr vectorPtr = IntPtr.Zero; // Placeholder, actual implementation may vary.
        try
        {
            result = 0;
            return SpoutNative.SpoutUtils.SpoutMessageBox4(hwnd, messagePtr, captionPtr, type, vectorPtr, ref result);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }

    public static int SpoutMessageBox(string message, string caption, params object[] args)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(string.Format(message, args));
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        try
        {
            return SpoutNative.SpoutUtils.SpoutMessageBox5(messagePtr, captionPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }

    public static int SpoutMessageBox(string message, uint type, string caption, params object[] args)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(string.Format(message, args));
        IntPtr captionPtr = Marshal.StringToHGlobalAnsi(caption);
        try
        {
            return SpoutNative.SpoutUtils.SpoutMessageBox6(messagePtr, type, captionPtr, IntPtr.Zero);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
            Marshal.FreeHGlobal(captionPtr);
        }
    }
}