
using System.Runtime.InteropServices;
using System.Text;

namespace SpoutInterop;

public enum SpoutLogLevel
{
    SPOUT_LOG_SILENT = 0,
    SPOUT_LOG_VERBOSE,
    SPOUT_LOG_NOTICE,
    SPOUT_LOG_WARNING,
    SPOUT_LOG_ERROR,
    SPOUT_LOG_FATAL,
    SPOUT_LOG_NONE
}

public enum DXGI_FORMAT : uint
{
    UNKNOWN = 0,
    R8G8B8A8_UNORM = 28,
    B8G8R8A8_UNORM = 87,
    B8G8R8X8_UNORM = 88,
    R10G10B10A2_UNORM = 24,
    R16G16B16A16_FLOAT = 10,
    R32G32B32A32_FLOAT = 2,
}

[StructLayout(LayoutKind.Sequential)]
public struct SharedTextureInfo
{
    public uint sharehandle;
    public uint width;
    public uint height;
    public uint format;
    public uint usage;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string description;
    public uint partnerId;
}

internal static class Std
{
    [DllImport("msvcp140.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void string_dtor(IntPtr self);

    [DllImport("msvcp140.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void vector_string_dtor(IntPtr self);

    internal static string GetString(IntPtr mem)
    {
        IntPtr data = Marshal.ReadIntPtr(mem);
        int size = Marshal.ReadInt32(mem, 8);
        if (size == 0) return string.Empty;
        byte[] bytes = new byte[size];
        Marshal.Copy(data, bytes, 0, size);
        return Encoding.ASCII.GetString(bytes);
    }

    internal static List<string> GetVectorString(IntPtr vec)
    {
        IntPtr begin = Marshal.ReadIntPtr(vec);
        IntPtr end = Marshal.ReadIntPtr(vec, 8);
        int count = (int)((end.ToInt64() - begin.ToInt64()) / Marshal.SizeOf<IntPtr>());
        List<string> list = new List<string>(count);
        for (int i = 0; i < count; i++)
        {
            IntPtr strPtr = Marshal.ReadIntPtr(begin, i * Marshal.SizeOf<IntPtr>());
            list.Add(GetString(strPtr));
        }
        return list;
    }
}

internal static class SpoutNative
{
    const string DllName = "Spout.dll";
    const CallingConvention CallConv = CallingConvention.ThisCall;
    const CharSet ChSet = CharSet.Ansi;

    internal static class Spout
    {
        [DllImport(DllName, EntryPoint = "??0Spout@@QEAA@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr ctor_copy(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "??0Spout@@QEAA@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr ctor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??1Spout@@UEAA@XZ", CallingConvention = CallConv)]
        internal static extern void dtor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??4Spout@@QEAAAEAV0@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr assign(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "?AdapterName@Spout@@QEAAPEADXZ", CallingConvention = CallConv)]
        internal static extern IntPtr AdapterName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CheckReceiver@Spout@@QEAA_NPEADAEAI1AEA_N@Z", CallingConvention = CallConv)]
        internal static extern bool CheckReceiver(IntPtr self, IntPtr name, ref uint width, ref uint height, ref bool connected);

        [DllImport(DllName, EntryPoint = "?CheckSender@Spout@@IEAA_NII@Z", CallingConvention = CallConv)]
        internal static extern bool CheckSender(IntPtr self, uint width, uint height);

        [DllImport(DllName, EntryPoint = "?CheckSpoutPanel@Spout@@QEAA_NPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool CheckSpoutPanel(IntPtr self, IntPtr name, ref int adapter);

        [DllImport(DllName, EntryPoint = "?CloseFrameSync@Spout@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void CloseFrameSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CreateReceiver@Spout@@QEAA_NPEADAEAI1@Z", CallingConvention = CallConv)]
        internal static extern bool CreateReceiver(IntPtr self, IntPtr name, ref uint width, ref uint height);

        [DllImport(DllName, EntryPoint = "?CreateSender@Spout@@QEAA_NPEBDIIK@Z", CallingConvention = CallConv)]
        internal static extern bool CreateSender(IntPtr self, IntPtr name, uint width, uint height, uint format);

        [DllImport(DllName, EntryPoint = "?DisableFrameCount@Spout@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void DisableFrameCount(IntPtr self);

        [DllImport(DllName, EntryPoint = "?EnableFrameSync@Spout@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void EnableFrameSync(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?FindNVIDIA@Spout@@QEAA_NAEAH@Z", CallingConvention = CallConv)]
        internal static extern bool FindNVIDIA(IntPtr self, ref int adapter);

        [DllImport(DllName, EntryPoint = "?GetActiveSender@Spout@@QEAA_NPEAD@Z", CallingConvention = CallConv)]
        internal static extern bool GetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetAdapter@Spout@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetAdapter(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NHPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfo(IntPtr self, int index, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfoOverload2(IntPtr self, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetCPU@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPU(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetFps@Spout@@QEAANXZ", CallingConvention = CallConv)]
        internal static extern double GetFps(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetFrame@Spout@@QEAAJXZ", CallingConvention = CallConv)]
        internal static extern int GetFrame(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetGLDX@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetGLDX(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHandle@Spout@@QEAAPEAXXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetHandle(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHeight@Spout@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetHeight(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetNumAdapters@Spout@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetNumAdapters(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetPerformancePreference@Spout@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetPerformancePreference(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetPreferredAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetReceiverName@Spout@@QEAA_NPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetReceiverName(IntPtr self, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSender@Spout@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetSender(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSenderAdapter@Spout@@QEAAHPEBDPEADH@Z", CallingConvention = CallConv)]
        internal static extern int GetSenderAdapter(IntPtr self, IntPtr name, IntPtr adapterName, int size);

        [DllImport(DllName, EntryPoint = "?GetSenderCPU@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetSenderCPU(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderCount@Spout@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderCount(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFormat@Spout@@QEAAKXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderFormat(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFps@Spout@@QEAANXZ", CallingConvention = CallConv)]
        internal static extern double GetSenderFps(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFrame@Spout@@QEAAJXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderFrame(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderGLDX@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetSenderGLDX(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHandle@Spout@@QEAAPEAXXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderHandle(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHeight@Spout@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderHeight(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderIndex@Spout@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetSenderIndex(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetSenderInfo@Spout@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z", CallingConvention = CallConv)]
        internal static extern bool GetSenderInfo(IntPtr self, IntPtr name, ref uint width, ref uint height, ref IntPtr handle, ref uint format);

        [DllImport(DllName, EntryPoint = "?GetSenderList@Spout@@QEAA?AV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderList(IntPtr self, IntPtr vec);

        [DllImport(DllName, EntryPoint = "?GetSenderName@Spout@@QEAAPEBDXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderTexture@Spout@@QEAAPEAUID3D11Texture2D@@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderTexture(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderWidth@Spout@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderWidth(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetWidth@Spout@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetWidth(IntPtr self);

        [DllImport(DllName, EntryPoint = "?HoldFps@Spout@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void HoldFps(IntPtr self, int fps);

        [DllImport(DllName, EntryPoint = "?InitReceiver@Spout@@IEAAXPEBDIIK@Z", CallingConvention = CallConv)]
        internal static extern void InitReceiver(IntPtr self, IntPtr name, uint width, uint height, uint format);

        [DllImport(DllName, EntryPoint = "?IsApplicationPath@Spout@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool IsApplicationPath(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?IsConnected@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsConnected(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameCountEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameNew@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameNew(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameSyncEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsInitialized@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsInitialized(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsPreferenceAvailable(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsUpdated@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsUpdated(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEADAEAI1PEAEI_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveImage(IntPtr self, IntPtr name, ref uint width, ref uint height, IntPtr pixels, uint stride, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEAEI_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveImageOverload2(IntPtr self, IntPtr pixels, uint stride, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveSenderData@Spout@@IEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool ReceiveSenderData(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveTextureOverload1(IntPtr self, uint textureID, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NPEADAEAI1II_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveTextureOverload2(IntPtr self, IntPtr name, ref uint width, ref uint height, uint textureID, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool ReceiveTextureOverload3(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReleaseReceiver@Spout@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void ReleaseReceiver(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReleaseSender@Spout@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void ReleaseSender(IntPtr self);

        [DllImport(DllName, EntryPoint = "?SelectSender@Spout@@QEAA_NPEAUHWND__@@@Z", CallingConvention = CallConv)]
        internal static extern bool SelectSender(IntPtr self, IntPtr hwnd);

        [DllImport(DllName, EntryPoint = "?SelectSenderPanel@Spout@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SelectSenderPanel(IntPtr self, IntPtr message);

        [DllImport(DllName, EntryPoint = "?SendFbo@Spout@@QEAA_NIII_N@Z", CallingConvention = CallConv)]
        internal static extern bool SendFbo(IntPtr self, uint fboID, uint width, uint height, bool invert);

        [DllImport(DllName, EntryPoint = "?SendImage@Spout@@QEAA_NPEBEIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool SendImage(IntPtr self, IntPtr pixels, uint width, uint height, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?SendTexture@Spout@@QEAA_NIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool SendTexture(IntPtr self, uint textureID, uint width, uint height, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?SetActiveSender@Spout@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetFrameCount@Spout@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameCount(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?SetFrameSync@Spout@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameSync(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetPerformancePreference@Spout@@QEAA_NHPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetPerformancePreference(IntPtr self, int preference, IntPtr path);

        [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@Spout@@QEAA_NH@Z", CallingConvention = CallConv)]
        internal static extern bool SetPreferredAdapter(IntPtr self, int index);

        [DllImport(DllName, EntryPoint = "?SetReceiverName@Spout@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetReceiverName(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetSenderFormat@Spout@@QEAAXK@Z", CallingConvention = CallConv)]
        internal static extern void SetSenderFormat(IntPtr self, uint format);

        [DllImport(DllName, EntryPoint = "?SetSenderName@Spout@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetSenderName(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?UpdateSender@Spout@@QEAA_NPEBDII@Z", CallingConvention = CallConv)]
        internal static extern bool UpdateSender(IntPtr self, IntPtr name, uint width, uint height);

        [DllImport(DllName, EntryPoint = "?WaitFrameSync@Spout@@QEAA_NPEBDK@Z", CallingConvention = CallConv)]
        internal static extern bool WaitFrameSync(IntPtr self, IntPtr name, uint timeout);
    }

    internal static class SpoutSender
    {
        [DllImport(DllName, EntryPoint = "??0SpoutSender@@QEAA@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr ctor_copy(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "??0SpoutSender@@QEAA@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr ctor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??1SpoutSender@@QEAA@XZ", CallingConvention = CallConv)]
        internal static extern void dtor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??4SpoutSender@@QEAAAEAV0@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr assign(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "?AdapterName@SpoutSender@@QEAAPEADXZ", CallingConvention = CallConv)]
        internal static extern IntPtr AdapterName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?BindSharedTexture@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool BindSharedTexture(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CloseFrameSync@SpoutSender@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void CloseFrameSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CopyTexture@SpoutSender@@QEAA_NIIIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool CopyTexture(IntPtr self, uint srcID, uint dstID, uint srcTarget, uint dstTarget, uint width, uint height, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?CreateMemoryBuffer@SpoutSender@@QEAA_NPEBDH@Z", CallingConvention = CallConv)]
        internal static extern bool CreateMemoryBuffer(IntPtr self, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?CreateOpenGL@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool CreateOpenGL(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CreateSender@SpoutSender@@QEAA_NPEBDIIK@Z", CallingConvention = CallConv)]
        internal static extern bool CreateSender(IntPtr self, IntPtr name, uint width, uint height, uint format);

        [DllImport(DllName, EntryPoint = "?DX11format@SpoutSender@@QEAA?AW4DXGI_FORMAT@@H@Z", CallingConvention = CallConv)]
        internal static extern DXGI_FORMAT DX11format(IntPtr self, int format);

        [DllImport(DllName, EntryPoint = "?DeleteMemoryBuffer@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool DeleteMemoryBuffer(IntPtr self);

        [DllImport(DllName, EntryPoint = "?DisableFrameCount@SpoutSender@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void DisableFrameCount(IntPtr self);

        [DllImport(DllName, EntryPoint = "?EnableFrameSync@SpoutSender@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void EnableFrameSync(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?GLDXformat@SpoutSender@@QEAAHW4DXGI_FORMAT@@Z", CallingConvention = CallConv)]
        internal static extern int GLDXformat(IntPtr self, DXGI_FORMAT format);

        [DllImport(DllName, EntryPoint = "?GLformat@SpoutSender@@QEAAHII@Z", CallingConvention = CallConv)]
        internal static extern int GLformat(IntPtr self, uint @internal, uint external);

        [DllImport(DllName, EntryPoint = "?GLformatName@SpoutSender@@QEAA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@H@Z", CallingConvention = CallConv)]
        internal static extern IntPtr GLformatName(IntPtr self, IntPtr str, int format);

        [DllImport(DllName, EntryPoint = "?GetActiveSender@SpoutSender@@QEAA_NPEAD@Z", CallingConvention = CallConv)]
        internal static extern bool GetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetAdapter@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetAdapter(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutSender@@QEAA_NHPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfo1(IntPtr self, int index, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutSender@@QEAA_NPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfo2(IntPtr self, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterName@SpoutSender@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetAutoShare@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetAutoShare(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetBufferMode@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetBufferMode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetBuffers@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetBuffers(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetCPU@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPU(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetCPUmode@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPUmode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetCPUshare@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPUshare(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetDX11format@SpoutSender@@QEAA?AW4DXGI_FORMAT@@XZ", CallingConvention = CallConv)]
        internal static extern DXGI_FORMAT GetDX11format(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetDX9@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetDX9(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetFps@SpoutSender@@QEAANXZ", CallingConvention = CallConv)]
        internal static extern double GetFps(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetFrame@SpoutSender@@QEAAJXZ", CallingConvention = CallConv)]
        internal static extern int GetFrame(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetGLDX@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetGLDX(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHandle@SpoutSender@@QEAAPEAXXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetHandle(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHeight@SpoutSender@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetHeight(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHostPath@SpoutSender@@QEAA_NPEBDPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetHostPath(IntPtr self, IntPtr senderName, IntPtr path, int maxchars);

        [DllImport(DllName, EntryPoint = "?GetMemoryBufferSize@SpoutSender@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetMemoryBufferSize(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetMemoryShareMode@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetMemoryShareMode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetName@SpoutSender@@QEAAPEBDXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetNumAdapters@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetNumAdapters(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetPerformancePreference@SpoutSender@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetPerformancePreference(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@SpoutSender@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetPreferredAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSender@SpoutSender@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetSender(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSenderCount@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderCount(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFormat@SpoutSender@@QEAAKXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderFormat(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFps@SpoutSender@@QEAANXZ", CallingConvention = CallConv)]
        internal static extern double GetSenderFps(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFrame@SpoutSender@@QEAAJXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderFrame(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderGLDX@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetSenderGLDX(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHandle@SpoutSender@@QEAAPEAXXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderHandle(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHeight@SpoutSender@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderHeight(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderInfo@SpoutSender@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z", CallingConvention = CallConv)]
        internal static extern bool GetSenderInfo(IntPtr self, IntPtr name, ref uint width, ref uint height, ref IntPtr handle, ref uint format);

        [DllImport(DllName, EntryPoint = "?GetSenderName@SpoutSender@@QEAAPEBDXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSharedTextureID@SpoutSender@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSharedTextureID(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSpoutVersion@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetSpoutVersion(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetVerticalSync@SpoutSender@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetVerticalSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?HoldFps@SpoutSender@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void HoldFps(IntPtr self, int fps);

        [DllImport(DllName, EntryPoint = "?IsApplicationPath@SpoutSender@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool IsApplicationPath(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameCountEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameSyncEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsGLDXready@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsGLDXready(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsPreferenceAvailable(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReadTextureData@SpoutSender@@QEAA_NIIPEAXIIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReadTextureData(IntPtr self, uint textureID, uint width, IntPtr pixels, uint stride, uint @internal, uint external, uint format, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?ReleaseSender@SpoutSender@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void ReleaseSender(IntPtr self);

        [DllImport(DllName, EntryPoint = "?SendFbo@SpoutSender@@QEAA_NIII_N@Z", CallingConvention = CallConv)]
        internal static extern bool SendFbo(IntPtr self, uint fboID, uint width, uint height, bool invert);

        [DllImport(DllName, EntryPoint = "?SendImage@SpoutSender@@QEAA_NPEBEIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool SendImage(IntPtr self, IntPtr pixels, uint width, uint height, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?SendTexture@SpoutSender@@QEAA_NIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool SendTexture(IntPtr self, uint textureID, uint width, uint height, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?SetActiveSender@SpoutSender@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetAutoShare@SpoutSender@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetAutoShare(IntPtr self, bool autoShare);

        [DllImport(DllName, EntryPoint = "?SetBufferMode@SpoutSender@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetBufferMode(IntPtr self, bool bufferMode);

        [DllImport(DllName, EntryPoint = "?SetBuffers@SpoutSender@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetBuffers(IntPtr self, int buffers);

        [DllImport(DllName, EntryPoint = "?SetCPUmode@SpoutSender@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetCPUmode(IntPtr self, bool cpu);

        [DllImport(DllName, EntryPoint = "?SetCPUshare@SpoutSender@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetCPUshare(IntPtr self, bool cpu);

        [DllImport(DllName, EntryPoint = "?SetDX11format@SpoutSender@@QEAAXW4DXGI_FORMAT@@@Z", CallingConvention = CallConv)]
        internal static extern void SetDX11format(IntPtr self, DXGI_FORMAT format);

        [DllImport(DllName, EntryPoint = "?SetDX9@SpoutSender@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetDX9(IntPtr self, bool dx9);

        [DllImport(DllName, EntryPoint = "?SetFrameCount@SpoutSender@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameCount(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?SetFrameSync@SpoutSender@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameSync(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetMaxSenders@SpoutSender@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetMaxSenders(IntPtr self, int max);

        [DllImport(DllName, EntryPoint = "?SetMemoryShareMode@SpoutSender@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetMemoryShareMode(IntPtr self, bool memory);

        [DllImport(DllName, EntryPoint = "?SetPerformancePreference@SpoutSender@@QEAA_NHPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetPerformancePreference(IntPtr self, int preference, IntPtr path);

        [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@SpoutSender@@QEAA_NH@Z", CallingConvention = CallConv)]
        internal static extern bool SetPreferredAdapter(IntPtr self, int index);

        [DllImport(DllName, EntryPoint = "?SetShareMode@SpoutSender@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetShareMode(IntPtr self, int mode);

        [DllImport(DllName, EntryPoint = "?SetSenderFormat@SpoutSender@@QEAAXK@Z", CallingConvention = CallConv)]
        internal static extern void SetSenderFormat(IntPtr self, uint format);

        [DllImport(DllName, EntryPoint = "?SetSenderName@SpoutSender@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetSenderName(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetVerticalSync@SpoutSender@@QEAA_NH@Z", CallingConvention = CallConv)]
        internal static extern bool SetVerticalSync(IntPtr self, int swapInterval);

        [DllImport(DllName, EntryPoint = "?UnBindSharedTexture@SpoutSender@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool UnBindSharedTexture(IntPtr self);

        [DllImport(DllName, EntryPoint = "?UpdateSender@SpoutSender@@QEAA_NPEBDII@Z", CallingConvention = CallConv)]
        internal static extern bool UpdateSender(IntPtr self, IntPtr name, uint width, uint height);

        [DllImport(DllName, EntryPoint = "?WaitFrameSync@SpoutSender@@QEAA_NPEBDK@Z", CallingConvention = CallConv)]
        internal static extern bool WaitFrameSync(IntPtr self, IntPtr name, uint timeout);

        [DllImport(DllName, EntryPoint = "?WriteMemoryBuffer@SpoutSender@@QEAA_NPEBD0H@Z", CallingConvention = CallConv)]
        internal static extern bool WriteMemoryBuffer(IntPtr self, IntPtr name, IntPtr data, int length);
    }

    internal static class SpoutReceiver
    {
        [DllImport(DllName, EntryPoint = "??0SpoutReceiver@@QEAA@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr ctor_copy(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "??0SpoutReceiver@@QEAA@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr ctor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??1SpoutReceiver@@QEAA@XZ", CallingConvention = CallConv)]
        internal static extern void dtor(IntPtr self);

        [DllImport(DllName, EntryPoint = "??4SpoutReceiver@@QEAAAEAV0@AEBV0@@Z", CallingConvention = CallConv)]
        internal static extern IntPtr assign(IntPtr self, IntPtr other);

        [DllImport(DllName, EntryPoint = "?AdapterName@SpoutReceiver@@QEAAPEADXZ", CallingConvention = CallConv)]
        internal static extern IntPtr AdapterName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?BindSharedTexture@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool BindSharedTexture(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CheckReceiver@SpoutReceiver@@QEAA_NPEADAEAI1AEA_N@Z", CallingConvention = CallConv)]
        internal static extern bool CheckReceiver(IntPtr self, IntPtr name, ref uint width, ref uint height, ref bool connected);

        [DllImport(DllName, EntryPoint = "?CheckSenderPanel@SpoutReceiver@@QEAA_NPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool CheckSenderPanel(IntPtr self, IntPtr name, int maxchars);

        [DllImport(DllName, EntryPoint = "?CloseFrameSync@SpoutReceiver@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void CloseFrameSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CopyTexture@SpoutReceiver@@QEAA_NIIIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool CopyTexture(IntPtr self, uint srcID, uint dstID, uint srcTarget, uint dstTarget, uint width, uint height, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?CreateOpenGL@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool CreateOpenGL(IntPtr self);

        [DllImport(DllName, EntryPoint = "?CreateReceiver@SpoutReceiver@@QEAA_NPEADAEAI1@Z", CallingConvention = CallConv)]
        internal static extern bool CreateReceiver(IntPtr self, IntPtr name, ref uint width, ref uint height);

        [DllImport(DllName, EntryPoint = "?DX11format@SpoutReceiver@@QEAA?AW4DXGI_FORMAT@@H@Z", CallingConvention = CallConv)]
        internal static extern DXGI_FORMAT DX11format(IntPtr self, int format);

        [DllImport(DllName, EntryPoint = "?DisableFrameCount@SpoutReceiver@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void DisableFrameSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?EnableFrameSync@SpoutReceiver@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void EnableFrameSync(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?GLDXformat@SpoutReceiver@@QEAAHW4DXGI_FORMAT@@Z", CallingConvention = CallConv)]
        internal static extern int GLDXformat(IntPtr self, DXGI_FORMAT format);

        [DllImport(DllName, EntryPoint = "?GLformat@SpoutReceiver@@QEAAHII@Z", CallingConvention = CallConv)]
        internal static extern int GLformat(IntPtr self, uint @internal, uint external);

        [DllImport(DllName, EntryPoint = "?GLformatName@SpoutReceiver@@QEAA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@H@Z", CallingConvention = CallConv)]
        internal static extern IntPtr GLformatName(IntPtr self, IntPtr str, int format);

        [DllImport(DllName, EntryPoint = "?GetActiveSender@SpoutReceiver@@QEAA_NPEAD@Z", CallingConvention = CallConv)]
        internal static extern bool GetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetAdapter@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetAdapter(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutReceiver@@QEAA_NHPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfo1(IntPtr self, int index, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutReceiver@@QEAA_NPEAD0H@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterInfo2(IntPtr self, IntPtr name, IntPtr desc, int size);

        [DllImport(DllName, EntryPoint = "?GetAdapterName@SpoutReceiver@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetAutoShare@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetAutoShare(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetBufferMode@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetBufferMode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetBuffers@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetBuffers(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetCPUmode@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPUmode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetCPUshare@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetCPUshare(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetDX11format@SpoutReceiver@@QEAA?AW4DXGI_FORMAT@@XZ", CallingConvention = CallConv)]
        internal static extern DXGI_FORMAT GetDX11format(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetDX9@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetDX9(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetHostPath@SpoutReceiver@@QEAA_NPEBDPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetHostPath(IntPtr self, IntPtr senderName, IntPtr path, int maxchars);

        [DllImport(DllName, EntryPoint = "?GetMaxSenders@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetMaxSenders(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetMemoryBufferSize@SpoutReceiver@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetMemoryBufferSize(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?GetMemoryShareMode@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetMemoryShareMode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetNumAdapters@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetNumAdapters(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetPerformancePreference@SpoutReceiver@@QEAAHPEBD@Z", CallingConvention = CallConv)]
        internal static extern int GetPerformancePreference(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@SpoutReceiver@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetPreferredAdapterName(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetReceiverName@SpoutReceiver@@QEAA_NPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetReceiverName(IntPtr self, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSender@SpoutReceiver@@QEAA_NHPEADH@Z", CallingConvention = CallConv)]
        internal static extern bool GetSender(IntPtr self, int index, IntPtr name, int size);

        [DllImport(DllName, EntryPoint = "?GetSenderCPU@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetSenderCPU(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderCount@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderCount(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFormat@SpoutReceiver@@QEAAKXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderFormat(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFps@SpoutReceiver@@QEAANXZ", CallingConvention = CallConv)]
        internal static extern double GetSenderFps(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderFrame@SpoutReceiver@@QEAAJXZ", CallingConvention = CallConv)]
        internal static extern int GetSenderFrame(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderGLDX@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool GetSenderGLDX(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHandle@SpoutReceiver@@QEAAPEAXXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderHandle(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderHeight@SpoutReceiver@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderHeight(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderInfo@SpoutReceiver@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z", CallingConvention = CallConv)]
        internal static extern bool GetSenderInfo(IntPtr self, IntPtr name, ref uint width, ref uint height, ref IntPtr handle, ref uint format);

        [DllImport(DllName, EntryPoint = "?GetSenderList@SpoutReceiver@@QEAA?AV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@XZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderList(IntPtr self, IntPtr vec);

        [DllImport(DllName, EntryPoint = "?GetSenderName@SpoutReceiver@@QEAAPEBDXZ", CallingConvention = CallConv)]
        internal static extern IntPtr GetSenderName(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSenderWidth@SpoutReceiver@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSenderWidth(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetShareMode@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetShareMode(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSharedTextureID@SpoutReceiver@@QEAAIXZ", CallingConvention = CallConv)]
        internal static extern uint GetSharedTextureID(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetSpoutVersion@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetSpoutVersion(IntPtr self);

        [DllImport(DllName, EntryPoint = "?GetVerticalSync@SpoutReceiver@@QEAAHXZ", CallingConvention = CallConv)]
        internal static extern int GetVerticalSync(IntPtr self);

        [DllImport(DllName, EntryPoint = "?HoldFps@SpoutReceiver@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void HoldFps(IntPtr self, int fps);

        [DllImport(DllName, EntryPoint = "?IsApplicationPath@SpoutReceiver@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool IsApplicationPath(IntPtr self, IntPtr path);

        [DllImport(DllName, EntryPoint = "?IsConnected@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsConnected(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameCountEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameNew@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameNew(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsFrameSyncEnabled(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsGLDXready@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsGLDXready(IntPtr self);

        [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool IsPreferenceAvailable(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReadMemoryBuffer@SpoutReceiver@@QEAAHPEBDPEADH@Z", CallingConvention = CallConv)]
        internal static extern int ReadMemoryBuffer(IntPtr self, IntPtr name, IntPtr data, int length);

        [DllImport(DllName, EntryPoint = "?ReadTextureData@SpoutReceiver@@QEAA_NIIPEAXIIIII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReadTextureData(IntPtr self, uint textureID, uint width, IntPtr pixels, uint stride, uint @internal, uint external, uint format, bool invert, uint hostFBO);

        [DllImport(DllName, EntryPoint = "?ReceiveImage@SpoutReceiver@@QEAA_NPEADAEAI1PEAEI_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveImage(IntPtr self, IntPtr name, ref uint width, ref uint height, IntPtr pixels, uint stride, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveImage@SpoutReceiver@@QEAA_NPEAEI_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveImageOverload2(IntPtr self, IntPtr pixels, uint stride, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@SpoutReceiver@@QEAA_NII_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveTexture1(IntPtr self, uint textureID, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@SpoutReceiver@@QEAA_NPEADAEAI1II_NI@Z", CallingConvention = CallConv)]
        internal static extern bool ReceiveTexture2(IntPtr self, IntPtr name, ref uint width, ref uint height, uint textureID, bool invert, uint glFormat);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool ReceiveTexture3(IntPtr self);

        [DllImport(DllName, EntryPoint = "?ReleaseReceiver@SpoutReceiver@@QEAAXXZ", CallingConvention = CallConv)]
        internal static extern void ReleaseReceiver(IntPtr self);

        [DllImport(DllName, EntryPoint = "?SelectSender@SpoutReceiver@@QEAA_NPEAUHWND__@@@Z", CallingConvention = CallConv)]
        internal static extern bool SelectSender(IntPtr self, IntPtr hwnd);

        [DllImport(DllName, EntryPoint = "?SelectSenderPanel@SpoutReceiver@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SelectSenderPanel(IntPtr self, IntPtr message);

        [DllImport(DllName, EntryPoint = "?SetActiveSender@SpoutReceiver@@QEAA_NPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetActiveSender(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetAutoShare@SpoutReceiver@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetAutoShare(IntPtr self, bool autoShare);

        [DllImport(DllName, EntryPoint = "?SetBufferMode@SpoutReceiver@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetBufferMode(IntPtr self, bool bufferMode);

        [DllImport(DllName, EntryPoint = "?SetBuffers@SpoutReceiver@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetBuffers(IntPtr self, int buffers);

        [DllImport(DllName, EntryPoint = "?SetCPUmode@SpoutReceiver@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetCPUmode(IntPtr self, bool cpu);

        [DllImport(DllName, EntryPoint = "?SetCPUshare@SpoutReceiver@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetCPUshare(IntPtr self, bool cpu);

        [DllImport(DllName, EntryPoint = "?SetDX11format@SpoutReceiver@@QEAAXW4DXGI_FORMAT@@@Z", CallingConvention = CallConv)]
        internal static extern void SetDX11format(IntPtr self, DXGI_FORMAT format);

        [DllImport(DllName, EntryPoint = "?SetDX9@SpoutReceiver@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetDX9(IntPtr self, bool dx9);

        [DllImport(DllName, EntryPoint = "?SetFrameCount@SpoutReceiver@@QEAAX_N@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameCount(IntPtr self, bool enable);

        [DllImport(DllName, EntryPoint = "?SetFrameSync@SpoutReceiver@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetFrameSync(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetMaxSenders@SpoutReceiver@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetMaxSenders(IntPtr self, int max);

        [DllImport(DllName, EntryPoint = "?SetMemoryShareMode@SpoutReceiver@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetMemoryShareMode(IntPtr self, bool memory);

        [DllImport(DllName, EntryPoint = "?SetPerformancePreference@SpoutReceiver@@QEAA_NHPEBD@Z", CallingConvention = CallConv)]
        internal static extern bool SetPerformancePreference(IntPtr self, int preference, IntPtr path);

        [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@SpoutReceiver@@QEAA_NH@Z", CallingConvention = CallConv)]
        internal static extern bool SetPreferredAdapter(IntPtr self, int index);

        [DllImport(DllName, EntryPoint = "?SetReceiverName@SpoutReceiver@@QEAAXPEBD@Z", CallingConvention = CallConv)]
        internal static extern void SetReceiverName(IntPtr self, IntPtr name);

        [DllImport(DllName, EntryPoint = "?SetShareMode@SpoutReceiver@@QEAAXH@Z", CallingConvention = CallConv)]
        internal static extern void SetShareMode(IntPtr self, int mode);

        [DllImport(DllName, EntryPoint = "?SetVerticalSync@SpoutReceiver@@QEAA_N_N@Z", CallingConvention = CallConv)]
        internal static extern bool SetVerticalSync(IntPtr self, bool on);

        [DllImport(DllName, EntryPoint = "?UnBindSharedTexture@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallConv)]
        internal static extern bool UnBindSharedTexture(IntPtr self);

        [DllImport(DllName, EntryPoint = "?WaitFrameSync@SpoutReceiver@@QEAA_NPEBDK@Z", CallingConvention = CallConv)]
        internal static extern bool WaitFrameSync(IntPtr self, IntPtr name, uint timeout);

        [DllImport(DllName, EntryPoint = "?IsUpdated@SpoutReceiver@@QEAA_NXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsUpdated(IntPtr thisPtr);

        [DllImport(DllName, EntryPoint = "?GetWidth@SpoutReceiver@@QEAAIXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetWidth(IntPtr thisPtr);

        [DllImport(DllName, EntryPoint = "?GetHeight@SpoutReceiver@@QEAAIXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint GetHeight(IntPtr thisPtr);

        [DllImport(DllName, EntryPoint = "?ReceiveTexture@SpoutReceiver@@QEAA_NI_NI@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ReceiveTexture(IntPtr thisPtr, uint textureID, uint textureTarget, bool bInvert, uint hostFbo);
    }

    internal static class SpoutUtils
    {
        [DllImport("Spout.dll", EntryPoint = "?CloseSpoutConsole@spoututils@@YAX_N@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CloseSpoutConsole(bool bWait);

        [DllImport("Spout.dll", EntryPoint = "?CopyToClipBoard@spoututils@@YA_NPEAUHWND__@@PEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CopyToClipBoard(IntPtr hwnd, IntPtr text);

        [DllImport("Spout.dll", EntryPoint = "?DisableLogs@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DisableLogs();

        [DllImport("Spout.dll", EntryPoint = "?DisableSpoutLog@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DisableSpoutLog();

        [DllImport("Spout.dll", EntryPoint = "?DisableSpoutLogFile@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DisableSpoutLogFile();

        [DllImport("Spout.dll", EntryPoint = "?ElapsedMicroseconds@spoututils@@YANXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ElapsedMicroseconds();

        [DllImport("Spout.dll", EntryPoint = "?EnableLogs@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EnableLogs();

        [DllImport("Spout.dll", EntryPoint = "?EnableSpoutLog@spoututils@@YAXPEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EnableSpoutLog(IntPtr file);

        [DllImport("Spout.dll", EntryPoint = "?EnableSpoutLogFile@spoututils@@YAXPEBD_N@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EnableSpoutLogFile(IntPtr file, bool append);

        [DllImport("Spout.dll", EntryPoint = "?EndTiming@spoututils@@YAN_N0@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double EndTiming(bool b, bool c);

        [DllImport("Spout.dll", EntryPoint = "?FindSubKey@spoututils@@YA_NPEAUHKEY__@@PEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool FindSubKey(IntPtr hkey, IntPtr subkey);

        [DllImport("Spout.dll", EntryPoint = "?GetCounter@spoututils@@YANXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double GetCounter();

        [DllImport("Spout.dll", EntryPoint = "?GetCurrentModule@spoututils@@YAPEAUHINSTANCE__@@XZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetCurrentModule();

        [DllImport("Spout.dll", EntryPoint = "?GetExeName@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetExeName(IntPtr str);

        [DllImport("Spout.dll", EntryPoint = "?GetExePath@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@_N@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetExePath(IntPtr str, bool b);

        [DllImport("Spout.dll", EntryPoint = "?GetExeVersion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@PEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetExeVersion(IntPtr str, IntPtr path);

        [DllImport("Spout.dll", EntryPoint = "?GetRefreshRate@spoututils@@YANXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double GetRefreshRate();

        [DllImport("Spout.dll", EntryPoint = "?GetSDKversion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@PEAH@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetSDKversion(IntPtr str, ref int version);

        [DllImport("Spout.dll", EntryPoint = "?GetSpoutLog@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@PEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetSpoutLog(IntPtr str, IntPtr file);

        [DllImport("Spout.dll", EntryPoint = "?GetSpoutLogPath@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetSpoutLogPath(IntPtr str);

        [DllImport("Spout.dll", EntryPoint = "?GetSpoutVersion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@PEAH@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetSpoutVersion(IntPtr str, ref int version);

        [DllImport("Spout.dll", EntryPoint = "?IsLaptop@spoututils@@YA_NXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool IsLaptop();

        [DllImport("Spout.dll", EntryPoint = "?LogFileEnabled@spoututils@@YA_NXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool LogFileEnabled();

        [DllImport("Spout.dll", EntryPoint = "?LogsEnabled@spoututils@@YA_NXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool LogsEnabled();

        [DllImport("Spout.dll", EntryPoint = "?OpenSpoutConsole@spoututils@@YAXPEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OpenSpoutConsole(IntPtr title);

        [DllImport("Spout.dll", EntryPoint = "?OpenSpoutLogs@spoututils@@YA_NXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OpenSpoutLogs();

        [DllImport("Spout.dll", EntryPoint = "?ReadDwordFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1PEAK@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ReadDwordFromRegistry(IntPtr hkey, IntPtr subkey, IntPtr value, ref uint dword);

        [DllImport("Spout.dll", EntryPoint = "?ReadPathFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1PEADK@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ReadPathFromRegistry(IntPtr hkey, IntPtr subkey, IntPtr value, IntPtr path, uint size);

        [DllImport("Spout.dll", EntryPoint = "?RemovePathFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool RemovePathFromRegistry(IntPtr hkey, IntPtr subkey, IntPtr value);

        [DllImport("Spout.dll", EntryPoint = "?RemoveSpoutLogFile@spoututils@@YAXPEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RemoveSpoutLogFile(IntPtr file);

        [DllImport("Spout.dll", EntryPoint = "?RemoveSubKey@spoututils@@YA_NPEAUHKEY__@@PEBD@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool RemoveSubKey(IntPtr hkey, IntPtr subkey);

        [DllImport("Spout.dll", EntryPoint = "?SetSpoutLogLevel@spoututils@@YAXW4SpoutLogLevel@1@@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SetSpoutLogLevel(SpoutLogLevel level);

        [DllImport("Spout.dll", EntryPoint = "?ShowSpoutLogs@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ShowSpoutLogs();

        [DllImport("Spout.dll", EntryPoint = "?SpoutLog@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLog(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutLogError@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLogError(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutLogFatal@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLogFatal(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutLogNotice@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLogNotice(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutLogVerbose@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLogVerbose(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutLogWarning@spoututils@@YAXPEBDZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SpoutLogWarning(IntPtr format, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1I1K@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox1(IntPtr hwnd, IntPtr message, IntPtr caption, uint type, uint idHelp, uint language);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IAEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox2(IntPtr hwnd, IntPtr message, IntPtr caption, uint type, ref string help);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IK@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox3(IntPtr hwnd, IntPtr message, IntPtr caption, uint type, uint language);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@AEAH@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox4(IntPtr hwnd, IntPtr message, IntPtr caption, uint type, IntPtr vector, ref int result);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEBD0ZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox5(IntPtr message, IntPtr caption, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEBDI0ZZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SpoutMessageBox6(IntPtr message, uint type, IntPtr caption, params IntPtr[] args);

        [DllImport("Spout.dll", EntryPoint = "?StartTiming@spoututils@@YAXXZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void StartTiming();

        [DllImport("Spout.dll", EntryPoint = "?WriteDwordToRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1K@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool WriteDwordToRegistry(IntPtr hkey, IntPtr subkey, IntPtr value, uint dword);

        [DllImport("Spout.dll", EntryPoint = "?WritePathToRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1K0@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool WritePathToRegistry(IntPtr hkey, IntPtr subkey, IntPtr value, uint size, IntPtr path);
    }
}
