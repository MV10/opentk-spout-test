
using System.Runtime.InteropServices;
using System.Text;

namespace SpoutInterop;

public class SpoutSender : IDisposable
{
    private IntPtr _nativePtr;
    private bool _disposed;

    public SpoutSender()
    {
        _nativePtr = Marshal.AllocHGlobal(SpoutNative.SpoutSender_Size);
        SpoutNative.SpoutSender_ctor(_nativePtr);
    }

    public SpoutSender(SpoutSender other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        _nativePtr = Marshal.AllocHGlobal(SpoutNative.SpoutSender_Size);
        SpoutNative.SpoutSender_copy_ctor(_nativePtr, other._nativePtr);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            SpoutNative.SpoutSender_dtor(_nativePtr);
            Marshal.FreeHGlobal(_nativePtr);
            _nativePtr = IntPtr.Zero;
            _disposed = true;
        }
    }

    ~SpoutSender()
    {
        Dispose();
    }

    public void SetSenderName(string sendername = null)
    {
        SpoutNative.SpoutSender_SetSenderName(_nativePtr, sendername);
    }

    public void SetSenderFormat(uint dwFormat)
    {
        SpoutNative.SpoutSender_SetSenderFormat(_nativePtr, dwFormat);
    }

    public void ReleaseSender()
    {
        SpoutNative.SpoutSender_ReleaseSender(_nativePtr);
    }

    public bool SendFbo(uint FboID, uint fbowidth, uint fboheight, bool bInvert = true)
    {
        return SpoutNative.SpoutSender_SendFbo(_nativePtr, FboID, fbowidth, fboheight, bInvert);
    }

    public bool SendTexture(uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert = true, uint HostFBO = 0)
    {
        return SpoutNative.SpoutSender_SendTexture(_nativePtr, TextureID, TextureTarget, width, height, bInvert, HostFBO);
    }

    public bool SendImage(IntPtr pixels, uint width, uint height, uint glFormat = 0, bool bInvert = false, uint HostFBO = 0)
    {
        return SpoutNative.SpoutSender_SendImage(_nativePtr, pixels, width, height, glFormat, bInvert, HostFBO);
    }

    public bool IsInitialized()
    {
        return SpoutNative.SpoutSender_IsInitialized(_nativePtr);
    }

    public string GetName()
    {
        IntPtr namePtr = SpoutNative.SpoutSender_GetName(_nativePtr);
        return Marshal.PtrToStringAnsi(namePtr);
    }

    public uint GetWidth()
    {
        return SpoutNative.SpoutSender_GetWidth(_nativePtr);
    }

    public uint GetHeight()
    {
        return SpoutNative.SpoutSender_GetHeight(_nativePtr);
    }

    public double GetFps()
    {
        return SpoutNative.SpoutSender_GetFps(_nativePtr);
    }

    public long GetFrame()
    {
        return SpoutNative.SpoutSender_GetFrame(_nativePtr);
    }

    public IntPtr GetHandle()
    {
        return SpoutNative.SpoutSender_GetHandle(_nativePtr);
    }

    public bool GetCPU()
    {
        return SpoutNative.SpoutSender_GetCPU(_nativePtr);
    }

    public bool GetGLDX()
    {
        return SpoutNative.SpoutSender_GetGLDX(_nativePtr);
    }

    public void SetFrameCount(bool bEnable)
    {
        SpoutNative.SpoutSender_SetFrameCount(_nativePtr, bEnable);
    }

    public void DisableFrameCount()
    {
        SpoutNative.SpoutSender_DisableFrameCount(_nativePtr);
    }

    public bool IsFrameCountEnabled()
    {
        return SpoutNative.SpoutSender_IsFrameCountEnabled(_nativePtr);
    }

    public void HoldFps(int fps)
    {
        SpoutNative.SpoutSender_HoldFps(_nativePtr, fps);
    }

    public void SetFrameSync(string name = null)
    {
        SpoutNative.SpoutSender_SetFrameSync(_nativePtr, name);
    }

    public bool WaitFrameSync(string SenderName, uint dwTimeout = 0)
    {
        return SpoutNative.SpoutSender_WaitFrameSync(_nativePtr, SenderName, dwTimeout);
    }

    public void EnableFrameSync(bool bSync = true)
    {
        SpoutNative.SpoutSender_EnableFrameSync(_nativePtr, bSync);
    }

    public void CloseFrameSync()
    {
        SpoutNative.SpoutSender_CloseFrameSync(_nativePtr);
    }

    public bool IsFrameSyncEnabled()
    {
        return SpoutNative.SpoutSender_IsFrameSyncEnabled(_nativePtr);
    }

    public int GetNumAdapters()
    {
        return SpoutNative.SpoutSender_GetNumAdapters(_nativePtr);
    }

    public bool GetAdapterName(int index, StringBuilder adaptername, int maxchars = 256)
    {
        return SpoutNative.SpoutSender_GetAdapterName(_nativePtr, index, adaptername, maxchars);
    }

    public string AdapterName()
    {
        IntPtr namePtr = SpoutNative.SpoutSender_AdapterName(_nativePtr);
        return Marshal.PtrToStringAnsi(namePtr);
    }

    public int GetAdapter()
    {
        return SpoutNative.SpoutSender_GetAdapter(_nativePtr);
    }

    public int GetSenderAdapter(string sendername, StringBuilder adaptername = null, int maxchars = 256)
    {
        return SpoutNative.SpoutSender_GetSenderAdapter(_nativePtr, sendername, adaptername, maxchars);
    }

    public bool GetAdapterInfo(StringBuilder description, StringBuilder output, int maxchars = 256)
    {
        return SpoutNative.SpoutSender_GetAdapterInfo(_nativePtr, description, output, maxchars);
    }

    public bool GetAdapterInfo(int index, StringBuilder description, StringBuilder output, int maxchars = 256)
    {
        return SpoutNative.SpoutSender_GetAdapterInfo(_nativePtr, index, description, output, maxchars);
    }

    public int GetPerformancePreference(string path = null)
    {
        return SpoutNative.SpoutSender_GetPerformancePreference(_nativePtr, path);
    }

    public bool SetPerformancePreference(int preference, string path = null)
    {
        return SpoutNative.SpoutSender_SetPerformancePreference(_nativePtr, preference, path);
    }

    public bool GetPreferredAdapterName(int preference, StringBuilder adaptername, int maxchars = 256)
    {
        return SpoutNative.SpoutSender_GetPreferredAdapterName(_nativePtr, preference, adaptername, maxchars);
    }

    public bool SetPreferredAdapter(int preference)
    {
        return SpoutNative.SpoutSender_SetPreferredAdapter(_nativePtr, preference);
    }

    public bool IsPreferenceAvailable()
    {
        return SpoutNative.SpoutSender_IsPreferenceAvailable(_nativePtr);
    }

    public bool IsApplicationPath(string path)
    {
        return SpoutNative.SpoutSender_IsApplicationPath(_nativePtr, path);
    }

    public bool FindNVIDIA(ref int nAdapter)
    {
        return SpoutNative.SpoutSender_FindNVIDIA(_nativePtr, ref nAdapter);
    }

    public bool GetAdapterInfo(StringBuilder renderadapter, StringBuilder renderdescription, StringBuilder renderversion, StringBuilder displaydescription, StringBuilder displayversion, int maxsize = 256)
    {
        return SpoutNative.SpoutSender_GetAdapterInfo(_nativePtr, renderadapter, renderdescription, renderversion, displaydescription, displayversion, maxsize);
    }

    public bool CreateSender(string Sendername, uint width = 0, uint height = 0, uint dwFormat = 0)
    {
        return SpoutNative.SpoutSender_CreateSender(_nativePtr, Sendername, width, height, dwFormat);
    }

    public bool UpdateSender(string Sendername, uint width, uint height)
    {
        return SpoutNative.SpoutSender_UpdateSender(_nativePtr, Sendername, width, height);
    }

    public bool DrawSharedTexture(float max_x = 1.0f, float max_y = 1.0f, float aspect = 1.0f, bool bInvert = true, uint HostFBO = 0)
    {
        return SpoutNative.SpoutSender_DrawSharedTexture(_nativePtr, max_x, max_y, aspect, bInvert, HostFBO);
    }

    public bool DrawToSharedTexture(uint TextureID, uint TextureTarget, uint width, uint height, float max_x = 1.0f, float max_y = 1.0f, float aspect = 1.0f, bool bInvert = false, uint HostFBO = 0)
    {
        return SpoutNative.SpoutSender_DrawToSharedTexture(_nativePtr, TextureID, TextureTarget, width, height, max_x, max_y, aspect, bInvert, HostFBO);
    }

    public bool BindSharedTexture()
    {
        return SpoutNative.SpoutSender_BindSharedTexture(_nativePtr);
    }

    public bool CopyTexture(uint srcTextureID, uint srcTextureTarget, uint dstTextureID, uint dstTextureTarget, uint width, uint height, bool bInvert = false, uint HostFBO = 0)
    {
        return SpoutNative.SpoutSender_CopyTexture(_nativePtr, srcTextureID, srcTextureTarget, dstTextureID, dstTextureTarget, width, height, bInvert, HostFBO);
    }

    public bool CreateMemoryBuffer(string name, int size)
    {
        return SpoutNative.SpoutSender_CreateMemoryBuffer(_nativePtr, name, size);
    }

    public uint DX11format(int format)
    {
        return SpoutNative.SpoutSender_DX11format(_nativePtr, format);
    }

    public bool UnBindSharedTexture()
    {
        return SpoutNative.SpoutSender_UnBindSharedTexture(_nativePtr);
    }

    public bool WriteMemoryBuffer(string name, IntPtr buffer, int size)
    {
        return SpoutNative.SpoutSender_WriteMemoryBuffer(_nativePtr, name, buffer, size);
    }
}