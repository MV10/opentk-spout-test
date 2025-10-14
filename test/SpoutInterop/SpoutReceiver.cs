
using System.Runtime.InteropServices;
using System.Text;

namespace SpoutInterop;

public class SpoutReceiver : IDisposable
{
    private IntPtr _nativePtr;
    private bool _disposed;

    public SpoutReceiver()
    {
        _nativePtr = Marshal.AllocHGlobal(SpoutNative.SpoutReceiver_Size);
        SpoutNative.SpoutReceiver_ctor(_nativePtr);
    }

    public SpoutReceiver(SpoutReceiver other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        _nativePtr = Marshal.AllocHGlobal(SpoutNative.SpoutReceiver_Size);
        SpoutNative.SpoutReceiver_copy_ctor(_nativePtr, other._nativePtr);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            SpoutNative.SpoutReceiver_dtor(_nativePtr);
            Marshal.FreeHGlobal(_nativePtr);
            _nativePtr = IntPtr.Zero;
            _disposed = true;
        }
    }

    ~SpoutReceiver()
    {
        Dispose();
    }

    public void SetReceiverName(string sendername = null)
    {
        SpoutNative.SpoutReceiver_SetReceiverName(_nativePtr, sendername);
    }

    public bool GetReceiverName(StringBuilder sendername, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetReceiverName(_nativePtr, sendername, maxchars);
    }

    public void ReleaseReceiver()
    {
        SpoutNative.SpoutReceiver_ReleaseReceiver(_nativePtr);
    }

    public bool ReceiveTexture()
    {
        return SpoutNative.SpoutReceiver_ReceiveTexture(_nativePtr);
    }

    public bool ReceiveTexture(StringBuilder name, ref uint width, ref uint height, uint TextureID = 0, uint TextureTarget = 0, bool bInvert = false, uint HostFbo = 0)
    {
        return SpoutNative.SpoutReceiver_ReceiveTexture(_nativePtr, name, ref width, ref height, TextureID, TextureTarget, bInvert, HostFbo);
    }

    public bool ReceiveTexture(uint TextureID, uint TextureTarget, bool bInvert = false, uint HostFbo = 0)
    {
        return SpoutNative.SpoutReceiver_ReceiveTexture(_nativePtr, TextureID, TextureTarget, bInvert, HostFbo);
    }

    public bool ReceiveImage(IntPtr pixels, uint glFormat = 0, bool bInvert = false, uint HostFbo = 0)
    {
        return SpoutNative.SpoutReceiver_ReceiveImage(_nativePtr, pixels, glFormat, bInvert, HostFbo);
    }

    public bool ReceiveImage(StringBuilder name, ref uint width, ref uint height, IntPtr pixels, uint glFormat = 0, bool bInvert = false, uint HostFbo = 0)
    {
        return SpoutNative.SpoutReceiver_ReceiveImage(_nativePtr, name, ref width, ref height, pixels, glFormat, bInvert, HostFbo);
    }

    public bool IsUpdated()
    {
        return SpoutNative.SpoutReceiver_IsUpdated(_nativePtr);
    }

    public bool IsConnected()
    {
        return SpoutNative.SpoutReceiver_IsConnected(_nativePtr);
    }

    public bool IsFrameNew()
    {
        return SpoutNative.SpoutReceiver_IsFrameNew(_nativePtr);
    }

    public string GetSenderName()
    {
        IntPtr namePtr = SpoutNative.SpoutReceiver_GetSenderName(_nativePtr);
        return Marshal.PtrToStringAnsi(namePtr);
    }

    public uint GetSenderWidth()
    {
        return SpoutNative.SpoutReceiver_GetSenderWidth(_nativePtr);
    }

    public uint GetSenderHeight()
    {
        return SpoutNative.SpoutReceiver_GetSenderHeight(_nativePtr);
    }

    public uint GetSenderFormat()
    {
        return SpoutNative.SpoutReceiver_GetSenderFormat(_nativePtr);
    }

    public double GetSenderFps()
    {
        return SpoutNative.SpoutReceiver_GetSenderFps(_nativePtr);
    }

    public long GetSenderFrame()
    {
        return SpoutNative.SpoutReceiver_GetSenderFrame(_nativePtr);
    }

    public IntPtr GetSenderHandle()
    {
        return SpoutNative.SpoutReceiver_GetSenderHandle(_nativePtr);
    }

    public IntPtr GetSenderTexture()
    {
        return SpoutNative.SpoutReceiver_GetSenderTexture(_nativePtr);
    }

    public bool GetSenderCPU()
    {
        return SpoutNative.SpoutReceiver_GetSenderCPU(_nativePtr);
    }

    public bool GetSenderGLDX()
    {
        return SpoutNative.SpoutReceiver_GetSenderGLDX(_nativePtr);
    }

    public StdVector GetSenderList()
    {
        return SpoutNative.SpoutReceiver_GetSenderList(_nativePtr);
    }

    public int GetSenderIndex(string sendername)
    {
        return SpoutNative.SpoutReceiver_GetSenderIndex(_nativePtr, sendername);
    }

    public bool SelectSender(IntPtr hwnd = default)
    {
        return SpoutNative.SpoutReceiver_SelectSender(_nativePtr, hwnd);
    }

    public void SetFrameCount(bool bEnable)
    {
        SpoutNative.SpoutReceiver_SetFrameCount(_nativePtr, bEnable);
    }

    public void DisableFrameCount()
    {
        SpoutNative.SpoutReceiver_DisableFrameCount(_nativePtr);
    }

    public bool IsFrameCountEnabled()
    {
        return SpoutNative.SpoutReceiver_IsFrameCountEnabled(_nativePtr);
    }

    public void HoldFps(int fps)
    {
        SpoutNative.SpoutReceiver_HoldFps(_nativePtr, fps);
    }

    public void SetFrameSync(string name = null)
    {
        SpoutNative.SpoutReceiver_SetFrameSync(_nativePtr, name);
    }

    public bool WaitFrameSync(string SenderName, uint dwTimeout = 0)
    {
        return SpoutNative.SpoutReceiver_WaitFrameSync(_nativePtr, SenderName, dwTimeout);
    }

    public void EnableFrameSync(bool bSync = true)
    {
        SpoutNative.SpoutReceiver_EnableFrameSync(_nativePtr, bSync);
    }

    public void CloseFrameSync()
    {
        SpoutNative.SpoutReceiver_CloseFrameSync(_nativePtr);
    }

    public bool IsFrameSyncEnabled()
    {
        return SpoutNative.SpoutReceiver_IsFrameSyncEnabled(_nativePtr);
    }

    public int GetSenderCount()
    {
        return SpoutNative.SpoutReceiver_GetSenderCount(_nativePtr);
    }

    public bool GetSender(int index, StringBuilder sendername, int MaxSize = 256)
    {
        return SpoutNative.SpoutReceiver_GetSender(_nativePtr, index, sendername, MaxSize);
    }

    public bool GetSenderInfo(string sendername, ref uint width, ref uint height, ref IntPtr dxShareHandle, ref uint dwFormat)
    {
        return SpoutNative.SpoutReceiver_GetSenderInfo(_nativePtr, sendername, ref width, ref height, ref dxShareHandle, ref dwFormat);
    }

    public bool GetActiveSender(StringBuilder sendername)
    {
        return SpoutNative.SpoutReceiver_GetActiveSender(_nativePtr, sendername);
    }

    public bool SetActiveSender(string sendername)
    {
        return SpoutNative.SpoutReceiver_SetActiveSender(_nativePtr, sendername);
    }

    public int GetNumAdapters()
    {
        return SpoutNative.SpoutReceiver_GetNumAdapters(_nativePtr);
    }

    public bool GetAdapterName(int index, StringBuilder adaptername, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetAdapterName(_nativePtr, index, adaptername, maxchars);
    }

    public string AdapterName()
    {
        IntPtr namePtr = SpoutNative.SpoutReceiver_AdapterName(_nativePtr);
        return Marshal.PtrToStringAnsi(namePtr);
    }

    public int GetAdapter()
    {
        return SpoutNative.SpoutReceiver_GetAdapter(_nativePtr);
    }

    public int GetSenderAdapter(string sendername, StringBuilder adaptername = null, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetSenderAdapter(_nativePtr, sendername, adaptername, maxchars);
    }

    public bool GetAdapterInfo(StringBuilder description, StringBuilder output, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetAdapterInfo(_nativePtr, description, output, maxchars);
    }

    public bool GetAdapterInfo(int index, StringBuilder description, StringBuilder output, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetAdapterInfo(_nativePtr, index, description, output, maxchars);
    }

    public int GetPerformancePreference(string path = null)
    {
        return SpoutNative.SpoutReceiver_GetPerformancePreference(_nativePtr, path);
    }

    public bool SetPerformancePreference(int preference, string path = null)
    {
        return SpoutNative.SpoutReceiver_SetPerformancePreference(_nativePtr, preference, path);
    }

    public bool GetPreferredAdapterName(int preference, StringBuilder adaptername, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_GetPreferredAdapterName(_nativePtr, preference, adaptername, maxchars);
    }

    public bool SetPreferredAdapter(int preference)
    {
        return SpoutNative.SpoutReceiver_SetPreferredAdapter(_nativePtr, preference);
    }

    public bool IsPreferenceAvailable()
    {
        return SpoutNative.SpoutReceiver_IsPreferenceAvailable(_nativePtr);
    }

    public bool IsApplicationPath(string path)
    {
        return SpoutNative.SpoutReceiver_IsApplicationPath(_nativePtr, path);
    }

    public bool FindNVIDIA(ref int nAdapter)
    {
        return SpoutNative.SpoutReceiver_FindNVIDIA(_nativePtr, ref nAdapter);
    }

    public bool GetAdapterInfo(StringBuilder renderadapter, StringBuilder renderdescription, StringBuilder renderversion, StringBuilder displaydescription, StringBuilder displayversion, int maxsize = 256)
    {
        return SpoutNative.SpoutReceiver_GetAdapterInfo(_nativePtr, renderadapter, renderdescription, renderversion, displaydescription, displayversion, maxsize);
    }

    public bool SelectSenderPanel(string message = null)
    {
        return SpoutNative.SpoutReceiver_SelectSenderPanel(_nativePtr, message);
    }

    public bool CheckSpoutPanel(StringBuilder sendername, int maxchars = 256)
    {
        return SpoutNative.SpoutReceiver_CheckSpoutPanel(_nativePtr, sendername, maxchars);
    }

    public bool CreateReceiver(StringBuilder Sendername, ref uint width, ref uint height)
    {
        return SpoutNative.SpoutReceiver_CreateReceiver(_nativePtr, Sendername, ref width, ref height);
    }

    public bool CheckReceiver(StringBuilder name, ref uint width, ref uint height, ref bool bConnected)
    {
        return SpoutNative.SpoutReceiver_CheckReceiver(_nativePtr, name, ref width, ref height, ref bConnected);
    }

    public bool BindSharedTexture()
    {
        return SpoutNative.SpoutReceiver_BindSharedTexture(_nativePtr);
    }

    public bool UnBindSharedTexture()
    {
        return SpoutNative.SpoutReceiver_UnBindSharedTexture(_nativePtr);
    }

    public bool CopyTexture(uint srcTextureID, uint srcTextureTarget, uint dstTextureID, uint dstTextureTarget, uint width, uint height, bool bInvert = false, uint HostFBO = 0)
    {
        return SpoutNative.SpoutReceiver_CopyTexture(_nativePtr, srcTextureID, srcTextureTarget, dstTextureID, dstTextureTarget, width, height, bInvert, HostFBO);
    }

    public bool CreateOpenGL()
    {
        return SpoutNative.SpoutReceiver_CreateOpenGL(_nativePtr);
    }

    public bool CloseOpenGL()
    {
        return SpoutNative.SpoutReceiver_CloseOpenGL(_nativePtr);
    }

    public uint DX11format(int format)
    {
        return SpoutNative.SpoutReceiver_DX11format(_nativePtr, format);
    }

    public uint GetSharedTextureID()
    {
        return SpoutNative.SpoutReceiver_GetSharedTextureID(_nativePtr);
    }
}