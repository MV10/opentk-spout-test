
using System.Runtime.InteropServices;

namespace SpoutInterop;

public class SpoutReceiver : IDisposable
{
    private IntPtr _nativePtr;
    private bool _disposed;

    public SpoutReceiver()
    {
        _nativePtr = SpoutNative.SpoutReceiver.ctor(Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>()));
        if (_nativePtr == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create SpoutReceiver instance.");
    }

    public SpoutReceiver(SpoutReceiver other)
    {
        if (other == null || other._nativePtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(other));
        _nativePtr = SpoutNative.SpoutReceiver.ctor_copy(Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>()), other._nativePtr);
        if (_nativePtr == IntPtr.Zero)
            throw new InvalidOperationException("Failed to copy SpoutReceiver instance.");
    }

    public void Dispose()
    {
        if (_disposed) return;
        if (_nativePtr != IntPtr.Zero)
        {
            SpoutNative.SpoutReceiver.dtor(_nativePtr);
            Marshal.FreeHGlobal(_nativePtr);
            _nativePtr = IntPtr.Zero;
        }
        _disposed = true;
    }

    public SpoutReceiver Assign(SpoutReceiver other)
    {
        if (other == null || other._nativePtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(other));
        SpoutNative.SpoutReceiver.assign(_nativePtr, other._nativePtr);
        return this;
    }

    public string AdapterName()
    {
        IntPtr ptr = SpoutNative.SpoutReceiver.AdapterName(_nativePtr);
        return Std.GetString(ptr);
    }

    public bool BindSharedTexture()
    {
        return SpoutNative.SpoutReceiver.BindSharedTexture(_nativePtr);
    }

    public bool CheckReceiver(out string name, out uint width, out uint height, out bool connected)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            width = 0; height = 0; connected = false;
            bool success = SpoutNative.SpoutReceiver.CheckReceiver(_nativePtr, namePtr, ref width, ref height, ref connected);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool CheckSenderPanel(out string name, int maxchars)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(maxchars);
        try
        {
            bool success = SpoutNative.SpoutReceiver.CheckSenderPanel(_nativePtr, namePtr, maxchars);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void CloseFrameSync()
    {
        SpoutNative.SpoutReceiver.CloseFrameSync(_nativePtr);
    }

    public bool CopyTexture(uint srcID, uint dstID, uint srcTarget, uint dstTarget, uint width, uint height, bool invert, uint hostFBO)
    {
        return SpoutNative.SpoutReceiver.CopyTexture(_nativePtr, srcID, dstID, srcTarget, dstTarget, width, height, invert, hostFBO);
    }

    public bool CreateOpenGL()
    {
        return SpoutNative.SpoutReceiver.CreateOpenGL(_nativePtr);
    }

    public bool CreateReceiver(string name, ref uint width, ref uint height)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.CreateReceiver(_nativePtr, namePtr, ref width, ref height);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public DXGI_FORMAT DX11format(int format)
    {
        return SpoutNative.SpoutReceiver.DX11format(_nativePtr, format);
    }

    public void DisableFrameCount()
    {
        SpoutNative.SpoutReceiver.DisableFrameSync(_nativePtr);
    }

    public void EnableFrameSync(bool enable)
    {
        SpoutNative.SpoutReceiver.EnableFrameSync(_nativePtr, enable);
    }

    public int GLDXformat(DXGI_FORMAT format)
    {
        return SpoutNative.SpoutReceiver.GLDXformat(_nativePtr, format);
    }

    public int GLformat(uint internalFormat, uint externalFormat)
    {
        return SpoutNative.SpoutReceiver.GLformat(_nativePtr, internalFormat, externalFormat);
    }

    public string GLformatName(int format)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutReceiver.GLformatName(_nativePtr, strPtr, format);
            string result = Std.GetString(strPtr);
            Std.string_dtor(strPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(strPtr);
        }
    }

    public bool GetActiveSender(out string name)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetActiveSender(_nativePtr, namePtr);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public int GetAdapter()
    {
        return SpoutNative.SpoutReceiver.GetAdapter(_nativePtr);
    }

    public bool GetAdapterInfo(int index, out string name, out string description)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        IntPtr descPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetAdapterInfo1(_nativePtr, index, namePtr, descPtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            description = success ? Marshal.PtrToStringAnsi(descPtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(descPtr);
        }
    }

    public bool GetAdapterInfo(out string name, out string description)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        IntPtr descPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetAdapterInfo2(_nativePtr, namePtr, descPtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            description = success ? Marshal.PtrToStringAnsi(descPtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
            Marshal.FreeHGlobal(descPtr);
        }
    }

    public bool GetAdapterName(int index, out string name)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetAdapterName(_nativePtr, index, namePtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetAutoShare()
    {
        return SpoutNative.SpoutReceiver.GetAutoShare(_nativePtr);
    }

    public bool GetBufferMode()
    {
        return SpoutNative.SpoutReceiver.GetBufferMode(_nativePtr);
    }

    public int GetBuffers()
    {
        return SpoutNative.SpoutReceiver.GetBuffers(_nativePtr);
    }

    public bool GetCPUmode()
    {
        return SpoutNative.SpoutReceiver.GetCPUmode(_nativePtr);
    }

    public bool GetCPUshare()
    {
        return SpoutNative.SpoutReceiver.GetCPUshare(_nativePtr);
    }

    public DXGI_FORMAT GetDX11format()
    {
        return SpoutNative.SpoutReceiver.GetDX11format(_nativePtr);
    }

    public bool GetDX9()
    {
        return SpoutNative.SpoutReceiver.GetDX9(_nativePtr);
    }

    public bool GetHostPath(string senderName, out string path)
    {
        IntPtr senderNamePtr = Marshal.StringToHGlobalAnsi(senderName);
        IntPtr pathPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetHostPath(_nativePtr, senderNamePtr, pathPtr, 256);
            path = success ? Marshal.PtrToStringAnsi(pathPtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(senderNamePtr);
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public int GetMaxSenders()
    {
        return SpoutNative.SpoutReceiver.GetMaxSenders(_nativePtr);
    }

    public int GetMemoryBufferSize(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.GetMemoryBufferSize(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetMemoryShareMode()
    {
        return SpoutNative.SpoutReceiver.GetMemoryShareMode(_nativePtr);
    }

    public int GetNumAdapters()
    {
        return SpoutNative.SpoutReceiver.GetNumAdapters(_nativePtr);
    }

    public int GetPerformancePreference(string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutReceiver.GetPerformancePreference(_nativePtr, pathPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public bool GetPreferredAdapterName(int index, out string name)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetPreferredAdapterName(_nativePtr, index, namePtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetReceiverName(out string name)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetReceiverName(_nativePtr, namePtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetSender(int index, out string name)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutReceiver.GetSender(_nativePtr, index, namePtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetSenderCPU()
    {
        return SpoutNative.SpoutReceiver.GetSenderCPU(_nativePtr);
    }

    public int GetSenderCount()
    {
        return SpoutNative.SpoutReceiver.GetSenderCount(_nativePtr);
    }

    public uint GetSenderFormat()
    {
        return SpoutNative.SpoutReceiver.GetSenderFormat(_nativePtr);
    }

    public double GetSenderFps()
    {
        return SpoutNative.SpoutReceiver.GetSenderFps(_nativePtr);
    }

    public int GetSenderFrame()
    {
        return SpoutNative.SpoutReceiver.GetSenderFrame(_nativePtr);
    }

    public bool GetSenderGLDX()
    {
        return SpoutNative.SpoutReceiver.GetSenderGLDX(_nativePtr);
    }

    public IntPtr GetSenderHandle()
    {
        return SpoutNative.SpoutReceiver.GetSenderHandle(_nativePtr);
    }

    public uint GetSenderHeight()
    {
        return SpoutNative.SpoutReceiver.GetSenderHeight(_nativePtr);
    }

    public bool GetSenderInfo(string name, out uint width, out uint height, out IntPtr handle, out uint format)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            width = 0;
            height = 0;
            handle = IntPtr.Zero;
            format = 0;
            return SpoutNative.SpoutReceiver.GetSenderInfo(_nativePtr, namePtr, ref width, ref height, ref handle, ref format);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public List<string> GetSenderList()
    {
        IntPtr vecPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutReceiver.GetSenderList(_nativePtr, vecPtr);
            List<string> result = Std.GetVectorString(vecPtr);
            Std.vector_string_dtor(vecPtr);
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(vecPtr);
        }
    }

    public string GetSenderName()
    {
        IntPtr ptr = SpoutNative.SpoutReceiver.GetSenderName(_nativePtr);
        return Marshal.PtrToStringAnsi(ptr);
    }

    public uint GetSenderWidth()
    {
        return SpoutNative.SpoutReceiver.GetSenderWidth(_nativePtr);
    }

    public int GetShareMode()
    {
        return SpoutNative.SpoutReceiver.GetShareMode(_nativePtr);
    }

    public uint GetSharedTextureID()
    {
        return SpoutNative.SpoutReceiver.GetSharedTextureID(_nativePtr);
    }

    public int GetSpoutVersion()
    {
        return SpoutNative.SpoutReceiver.GetSpoutVersion(_nativePtr);
    }

    public int GetVerticalSync()
    {
        return SpoutNative.SpoutReceiver.GetVerticalSync(_nativePtr);
    }

    public void HoldFps(int fps)
    {
        SpoutNative.SpoutReceiver.HoldFps(_nativePtr, fps);
    }

    public bool IsApplicationPath(string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutReceiver.IsApplicationPath(_nativePtr, pathPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public bool IsConnected()
    {
        return SpoutNative.SpoutReceiver.IsConnected(_nativePtr);
    }

    public bool IsFrameCountEnabled()
    {
        return SpoutNative.SpoutReceiver.IsFrameCountEnabled(_nativePtr);
    }

    public bool IsFrameNew()
    {
        return SpoutNative.SpoutReceiver.IsFrameNew(_nativePtr);
    }

    public bool IsFrameSyncEnabled()
    {
        return SpoutNative.SpoutReceiver.IsFrameSyncEnabled(_nativePtr);
    }

    public bool IsGLDXready()
    {
        return SpoutNative.SpoutReceiver.IsGLDXready(_nativePtr);
    }

    public bool IsPreferenceAvailable()
    {
        return SpoutNative.SpoutReceiver.IsPreferenceAvailable(_nativePtr);
    }

    public int ReadMemoryBuffer(string name, IntPtr data, int length)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.ReadMemoryBuffer(_nativePtr, namePtr, data, length);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool ReadTextureData(uint textureID, uint width, IntPtr pixels, uint stride, uint internalFormat, uint externalFormat, uint format, bool invert, uint hostFBO)
    {
        return SpoutNative.SpoutReceiver.ReadTextureData(_nativePtr, textureID, width, pixels, stride, internalFormat, externalFormat, format, invert, hostFBO);
    }

    public bool ReceiveImage(string name, ref uint width, ref uint height, IntPtr pixels, uint stride, bool invert, uint glFormat)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.ReceiveImage(_nativePtr, namePtr, ref width, ref height, pixels, stride, invert, glFormat);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool ReceiveImage(IntPtr pixels, uint stride, bool invert, uint glFormat)
    {
        return SpoutNative.SpoutReceiver.ReceiveImageOverload2(_nativePtr, pixels, stride, invert, glFormat);
    }

    public bool ReceiveTexture(uint textureID, bool invert, uint glFormat)
    {
        return SpoutNative.SpoutReceiver.ReceiveTexture1(_nativePtr, textureID, invert, glFormat);
    }

    public bool ReceiveTexture(string name, ref uint width, ref uint height, uint textureID, bool invert, uint glFormat)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.ReceiveTexture2(_nativePtr, namePtr, ref width, ref height, textureID, invert, glFormat);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool ReceiveTexture()
    {
        return SpoutNative.SpoutReceiver.ReceiveTexture3(_nativePtr);
    }

    public void ReleaseReceiver()
    {
        SpoutNative.SpoutReceiver.ReleaseReceiver(_nativePtr);
    }

    public bool SelectSender(IntPtr hwnd)
    {
        return SpoutNative.SpoutReceiver.SelectSender(_nativePtr, hwnd);
    }

    public bool SelectSenderPanel(string message)
    {
        IntPtr messagePtr = Marshal.StringToHGlobalAnsi(message);
        try
        {
            return SpoutNative.SpoutReceiver.SelectSenderPanel(_nativePtr, messagePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(messagePtr);
        }
    }

    public bool SetActiveSender(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.SetActiveSender(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void SetAutoShare(bool autoShare)
    {
        SpoutNative.SpoutReceiver.SetAutoShare(_nativePtr, autoShare);
    }

    public void SetBufferMode(bool bufferMode)
    {
        SpoutNative.SpoutReceiver.SetBufferMode(_nativePtr, bufferMode);
    }

    public void SetBuffers(int buffers)
    {
        SpoutNative.SpoutReceiver.SetBuffers(_nativePtr, buffers);
    }

    public bool SetCPUmode(bool cpu)
    {
        return SpoutNative.SpoutReceiver.SetCPUmode(_nativePtr, cpu);
    }

    public void SetCPUshare(bool cpu)
    {
        SpoutNative.SpoutReceiver.SetCPUshare(_nativePtr, cpu);
    }

    public void SetDX11format(DXGI_FORMAT format)
    {
        SpoutNative.SpoutReceiver.SetDX11format(_nativePtr, format);
    }

    public bool SetDX9(bool dx9)
    {
        return SpoutNative.SpoutReceiver.SetDX9(_nativePtr, dx9);
    }

    public void SetFrameCount(bool enable)
    {
        SpoutNative.SpoutReceiver.SetFrameCount(_nativePtr, enable);
    }

    public void SetFrameSync(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            SpoutNative.SpoutReceiver.SetFrameSync(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void SetMaxSenders(int max)
    {
        SpoutNative.SpoutReceiver.SetMaxSenders(_nativePtr, max);
    }

    public bool SetMemoryShareMode(bool memory)
    {
        return SpoutNative.SpoutReceiver.SetMemoryShareMode(_nativePtr, memory);
    }

    public bool SetPerformancePreference(int preference, string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutReceiver.SetPerformancePreference(_nativePtr, preference, pathPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public bool SetPreferredAdapter(int index)
    {
        return SpoutNative.SpoutReceiver.SetPreferredAdapter(_nativePtr, index);
    }

    public void SetReceiverName(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            SpoutNative.SpoutReceiver.SetReceiverName(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void SetShareMode(int mode)
    {
        SpoutNative.SpoutReceiver.SetShareMode(_nativePtr, mode);
    }

    public bool SetVerticalSync(bool on)
    {
        return SpoutNative.SpoutReceiver.SetVerticalSync(_nativePtr, on);
    }

    public bool UnBindSharedTexture()
    {
        return SpoutNative.SpoutReceiver.UnBindSharedTexture(_nativePtr);
    }

    public bool WaitFrameSync(string name, uint timeout)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutReceiver.WaitFrameSync(_nativePtr, namePtr, timeout);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool IsUpdated()
    {
        return SpoutNative.SpoutReceiver.IsUpdated(_nativePtr);
    }

    public uint GetWidth()
    {
        return SpoutNative.SpoutReceiver.GetWidth(_nativePtr);
    }

    public uint GetHeight()
    {
        return SpoutNative.SpoutReceiver.GetHeight(_nativePtr);
    }

    public bool ReceiveTexture(uint textureID, uint textureTarget, bool bInvert, uint hostFbo)
    {
        return SpoutNative.SpoutReceiver.ReceiveTexture(_nativePtr, textureID, textureTarget, bInvert, hostFbo);
    }
}
