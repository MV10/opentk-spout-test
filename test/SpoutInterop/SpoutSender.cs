
using System.Runtime.InteropServices;

namespace SpoutInterop;

public class SpoutSender : IDisposable
{
    private IntPtr _nativePtr;
    private bool _disposed;

    public SpoutSender()
    {
        _nativePtr = SpoutNative.SpoutSender.ctor(Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>()));
        if (_nativePtr == IntPtr.Zero)
            throw new InvalidOperationException("Failed to create SpoutSender instance.");
    }

    public SpoutSender(SpoutSender other)
    {
        if (other == null || other._nativePtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(other));
        _nativePtr = SpoutNative.SpoutSender.ctor_copy(Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>()), other._nativePtr);
        if (_nativePtr == IntPtr.Zero)
            throw new InvalidOperationException("Failed to copy SpoutSender instance.");
    }

    public void Dispose()
    {
        if (_disposed) return;
        if (_nativePtr != IntPtr.Zero)
        {
            SpoutNative.SpoutSender.dtor(_nativePtr);
            Marshal.FreeHGlobal(_nativePtr);
            _nativePtr = IntPtr.Zero;
        }
        _disposed = true;
    }

    public SpoutSender Assign(SpoutSender other)
    {
        if (other == null || other._nativePtr == IntPtr.Zero)
            throw new ArgumentNullException(nameof(other));
        SpoutNative.SpoutSender.assign(_nativePtr, other._nativePtr);
        return this;
    }

    public string AdapterName()
    {
        IntPtr ptr = SpoutNative.SpoutSender.AdapterName(_nativePtr);
        return Std.GetString(ptr);
    }

    public bool BindSharedTexture()
    {
        return SpoutNative.SpoutSender.BindSharedTexture(_nativePtr);
    }

    public void CloseFrameSync()
    {
        SpoutNative.SpoutSender.CloseFrameSync(_nativePtr);
    }

    public bool CopyTexture(uint srcID, uint dstID, uint srcTarget, uint dstTarget, uint width, uint height, bool invert, uint hostFBO)
    {
        return SpoutNative.SpoutSender.CopyTexture(_nativePtr, srcID, dstID, srcTarget, dstTarget, width, height, invert, hostFBO);
    }

    public bool CreateMemoryBuffer(string name, int size)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.CreateMemoryBuffer(_nativePtr, namePtr, size);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool CreateOpenGL()
    {
        return SpoutNative.SpoutSender.CreateOpenGL(_nativePtr);
    }

    public bool CreateSender(string name, uint width, uint height, uint format)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.CreateSender(_nativePtr, namePtr, width, height, format);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public DXGI_FORMAT DX11format(int format)
    {
        return SpoutNative.SpoutSender.DX11format(_nativePtr, format);
    }

    public bool DeleteMemoryBuffer()
    {
        return SpoutNative.SpoutSender.DeleteMemoryBuffer(_nativePtr);
    }

    public void DisableFrameCount()
    {
        SpoutNative.SpoutSender.DisableFrameCount(_nativePtr);
    }

    public void EnableFrameSync(bool enable)
    {
        SpoutNative.SpoutSender.EnableFrameSync(_nativePtr, enable);
    }

    public int GLDXformat(DXGI_FORMAT format)
    {
        return SpoutNative.SpoutSender.GLDXformat(_nativePtr, format);
    }

    public int GLformat(uint internalFormat, uint externalFormat)
    {
        return SpoutNative.SpoutSender.GLformat(_nativePtr, internalFormat, externalFormat);
    }

    public string GLformatName(int format)
    {
        IntPtr strPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>());
        try
        {
            SpoutNative.SpoutSender.GLformatName(_nativePtr, strPtr, format);
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
            bool success = SpoutNative.SpoutSender.GetActiveSender(_nativePtr, namePtr);
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
        return SpoutNative.SpoutSender.GetAdapter(_nativePtr);
    }

    public bool GetAdapterInfo(int index, out string name, out string description)
    {
        IntPtr namePtr = Marshal.AllocHGlobal(256);
        IntPtr descPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutSender.GetAdapterInfo1(_nativePtr, index, namePtr, descPtr, 256);
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
            bool success = SpoutNative.SpoutSender.GetAdapterInfo2(_nativePtr, namePtr, descPtr, 256);
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
            bool success = SpoutNative.SpoutSender.GetAdapterName(_nativePtr, index, namePtr, 256);
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
        return SpoutNative.SpoutSender.GetAutoShare(_nativePtr);
    }

    public bool GetBufferMode()
    {
        return SpoutNative.SpoutSender.GetBufferMode(_nativePtr);
    }

    public int GetBuffers()
    {
        return SpoutNative.SpoutSender.GetBuffers(_nativePtr);
    }

    public bool GetCPU()
    {
        return SpoutNative.SpoutSender.GetCPU(_nativePtr);
    }

    public bool GetCPUmode()
    {
        return SpoutNative.SpoutSender.GetCPUmode(_nativePtr);
    }

    public bool GetCPUshare()
    {
        return SpoutNative.SpoutSender.GetCPUshare(_nativePtr);
    }

    public DXGI_FORMAT GetDX11format()
    {
        return SpoutNative.SpoutSender.GetDX11format(_nativePtr);
    }

    public bool GetDX9()
    {
        return SpoutNative.SpoutSender.GetDX9(_nativePtr);
    }

    public double GetFps()
    {
        return SpoutNative.SpoutSender.GetFps(_nativePtr);
    }

    public int GetFrame()
    {
        return SpoutNative.SpoutSender.GetFrame(_nativePtr);
    }

    public bool GetGLDX()
    {
        return SpoutNative.SpoutSender.GetGLDX(_nativePtr);
    }

    public IntPtr GetHandle()
    {
        return SpoutNative.SpoutSender.GetHandle(_nativePtr);
    }

    public uint GetHeight()
    {
        return SpoutNative.SpoutSender.GetHeight(_nativePtr);
    }

    public bool GetHostPath(string senderName, out string path)
    {
        IntPtr senderNamePtr = Marshal.StringToHGlobalAnsi(senderName);
        IntPtr pathPtr = Marshal.AllocHGlobal(256);
        try
        {
            bool success = SpoutNative.SpoutSender.GetHostPath(_nativePtr, senderNamePtr, pathPtr, 256);
            path = success ? Marshal.PtrToStringAnsi(pathPtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(senderNamePtr);
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public int GetMemoryBufferSize(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.GetMemoryBufferSize(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool GetMemoryShareMode()
    {
        return SpoutNative.SpoutSender.GetMemoryShareMode(_nativePtr);
    }

    public string GetName()
    {
        IntPtr ptr = SpoutNative.SpoutSender.GetName(_nativePtr);
        return Marshal.PtrToStringAnsi(ptr);
    }

    public int GetNumAdapters()
    {
        return SpoutNative.SpoutSender.GetNumAdapters(_nativePtr);
    }

    public int GetPerformancePreference(string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutSender.GetPerformancePreference(_nativePtr, pathPtr);
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
            bool success = SpoutNative.SpoutSender.GetPreferredAdapterName(_nativePtr, index, namePtr, 256);
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
            bool success = SpoutNative.SpoutSender.GetSender(_nativePtr, index, namePtr, 256);
            name = success ? Marshal.PtrToStringAnsi(namePtr) : string.Empty;
            return success;
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public int GetSenderCount()
    {
        return SpoutNative.SpoutSender.GetSenderCount(_nativePtr);
    }

    public uint GetSenderFormat()
    {
        return SpoutNative.SpoutSender.GetSenderFormat(_nativePtr);
    }

    public double GetSenderFps()
    {
        return SpoutNative.SpoutSender.GetSenderFps(_nativePtr);
    }

    public int GetSenderFrame()
    {
        return SpoutNative.SpoutSender.GetSenderFrame(_nativePtr);
    }

    public bool GetSenderGLDX()
    {
        return SpoutNative.SpoutSender.GetSenderGLDX(_nativePtr);
    }

    public IntPtr GetSenderHandle()
    {
        return SpoutNative.SpoutSender.GetSenderHandle(_nativePtr);
    }

    public uint GetSenderHeight()
    {
        return SpoutNative.SpoutSender.GetSenderHeight(_nativePtr);
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
            return SpoutNative.SpoutSender.GetSenderInfo(_nativePtr, namePtr, ref width, ref height, ref handle, ref format);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public string GetSenderName()
    {
        IntPtr ptr = SpoutNative.SpoutSender.GetSenderName(_nativePtr);
        return Marshal.PtrToStringAnsi(ptr);
    }

    public uint GetSharedTextureID()
    {
        return SpoutNative.SpoutSender.GetSharedTextureID(_nativePtr);
    }

    public int GetSpoutVersion()
    {
        return SpoutNative.SpoutSender.GetSpoutVersion(_nativePtr);
    }

    public int GetVerticalSync()
    {
        return SpoutNative.SpoutSender.GetVerticalSync(_nativePtr);
    }

    public void HoldFps(int fps)
    {
        SpoutNative.SpoutSender.HoldFps(_nativePtr, fps);
    }

    public bool IsApplicationPath(string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutSender.IsApplicationPath(_nativePtr, pathPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public bool IsFrameCountEnabled()
    {
        return SpoutNative.SpoutSender.IsFrameCountEnabled(_nativePtr);
    }

    public bool IsFrameSyncEnabled()
    {
        return SpoutNative.SpoutSender.IsFrameSyncEnabled(_nativePtr);
    }

    public bool IsGLDXready()
    {
        return SpoutNative.SpoutSender.IsGLDXready(_nativePtr);
    }

    public bool IsPreferenceAvailable()
    {
        return SpoutNative.SpoutSender.IsPreferenceAvailable(_nativePtr);
    }

    public bool ReadTextureData(uint textureID, uint width, IntPtr pixels, uint stride, uint internalFormat, uint externalFormat, uint format, bool invert, uint hostFBO)
    {
        return SpoutNative.SpoutSender.ReadTextureData(_nativePtr, textureID, width, pixels, stride, internalFormat, externalFormat, format, invert, hostFBO);
    }

    public void ReleaseSender()
    {
        SpoutNative.SpoutSender.ReleaseSender(_nativePtr);
    }

    public bool SendFbo(uint fboID, uint width, uint height, bool invert)
    {
        return SpoutNative.SpoutSender.SendFbo(_nativePtr, fboID, width, height, invert);
    }

    public bool SendImage(IntPtr pixels, uint width, uint height, bool invert, uint glFormat)
    {
        return SpoutNative.SpoutSender.SendImage(_nativePtr, pixels, width, height, invert, glFormat);
    }

    public bool SendTexture(uint textureID, uint width, uint height, bool invert, uint hostFBO)
    {
        return SpoutNative.SpoutSender.SendTexture(_nativePtr, textureID, width, height, invert, hostFBO);
    }

    public bool SetActiveSender(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.SetActiveSender(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void SetAutoShare(bool autoShare)
    {
        SpoutNative.SpoutSender.SetAutoShare(_nativePtr, autoShare);
    }

    public void SetBufferMode(bool bufferMode)
    {
        SpoutNative.SpoutSender.SetBufferMode(_nativePtr, bufferMode);
    }

    public void SetBuffers(int buffers)
    {
        SpoutNative.SpoutSender.SetBuffers(_nativePtr, buffers);
    }

    public bool SetCPUmode(bool cpu)
    {
        return SpoutNative.SpoutSender.SetCPUmode(_nativePtr, cpu);
    }

    public void SetCPUshare(bool cpu)
    {
        SpoutNative.SpoutSender.SetCPUshare(_nativePtr, cpu);
    }

    public void SetDX11format(DXGI_FORMAT format)
    {
        SpoutNative.SpoutSender.SetDX11format(_nativePtr, format);
    }

    public bool SetDX9(bool dx9)
    {
        return SpoutNative.SpoutSender.SetDX9(_nativePtr, dx9);
    }

    public void SetFrameCount(bool enable)
    {
        SpoutNative.SpoutSender.SetFrameCount(_nativePtr, enable);
    }

    public void SetFrameSync(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            SpoutNative.SpoutSender.SetFrameSync(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public void SetMaxSenders(int max)
    {
        SpoutNative.SpoutSender.SetMaxSenders(_nativePtr, max);
    }

    public bool SetMemoryShareMode(bool memory)
    {
        return SpoutNative.SpoutSender.SetMemoryShareMode(_nativePtr, memory);
    }

    public bool SetPerformancePreference(int preference, string path)
    {
        IntPtr pathPtr = Marshal.StringToHGlobalAnsi(path);
        try
        {
            return SpoutNative.SpoutSender.SetPerformancePreference(_nativePtr, preference, pathPtr);
        }
        finally
        {
            Marshal.FreeHGlobal(pathPtr);
        }
    }

    public bool SetPreferredAdapter(int index)
    {
        return SpoutNative.SpoutSender.SetPreferredAdapter(_nativePtr, index);
    }

    public void SetShareMode(int mode)
    {
        SpoutNative.SpoutSender.SetShareMode(_nativePtr, mode);
    }

    public void SetSenderFormat(uint format)
    {
        SpoutNative.SpoutSender.SetSenderFormat(_nativePtr, format);
    }

    public void SetSenderName(string name)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            SpoutNative.SpoutSender.SetSenderName(_nativePtr, namePtr);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool SetVerticalSync(int swapInterval)
    {
        return SpoutNative.SpoutSender.SetVerticalSync(_nativePtr, swapInterval);
    }

    public bool UnBindSharedTexture()
    {
        return SpoutNative.SpoutSender.UnBindSharedTexture(_nativePtr);
    }

    public bool UpdateSender(string name, uint width, uint height)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.UpdateSender(_nativePtr, namePtr, width, height);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool WaitFrameSync(string name, uint timeout)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.WaitFrameSync(_nativePtr, namePtr, timeout);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }

    public bool WriteMemoryBuffer(string name, IntPtr data, int length)
    {
        IntPtr namePtr = Marshal.StringToHGlobalAnsi(name);
        try
        {
            return SpoutNative.SpoutSender.WriteMemoryBuffer(_nativePtr, namePtr, data, length);
        }
        finally
        {
            Marshal.FreeHGlobal(namePtr);
        }
    }
}


