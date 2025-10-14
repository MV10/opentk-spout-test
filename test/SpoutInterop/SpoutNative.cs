
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SpoutInterop;

// Enums from documentation
public enum SpoutLogLevel : int
{
    SPOUT_LOG_SILENT,
    SPOUT_LOG_VERBOSE,
    SPOUT_LOG_NOTICE,
    SPOUT_LOG_WARNING,
    SPOUT_LOG_ERROR,
    SPOUT_LOG_FATAL,
    SPOUT_LOG_NONE
}

// Struct to represent std::string layout in MSVC (approximate, with SSO)
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct StdString
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] Buf;
    public ulong Size;
    public ulong Capacity;
}

// Struct to represent std::vector<std::string> layout (pointer to begin, end, capacity end)
[StructLayout(LayoutKind.Sequential)]
public struct StdVector
{
    public IntPtr Begin;
    public IntPtr End;
    public IntPtr CapacityEnd;
}

public static class SpoutNative
{
    // Estimated sizes for object allocation (with fudge factor)
    public const int Spout_Size = 4096;
    public const int SpoutSender_Size = 4096;
    public const int SpoutReceiver_Size = 4096;

    private const string DllName = "Spout.dll";
    private const CallingConvention ThisCall = CallingConvention.ThisCall;
    private const CallingConvention CDecl = CallingConvention.Cdecl;

    // Spout class constructors and destructor
    [DllImport(DllName, EntryPoint = "??0Spout@@QEAA@XZ", CallingConvention = ThisCall)]
    public static extern void Spout_ctor(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "??0Spout@@QEAA@AEBV0@@Z", CallingConvention = ThisCall)]
    public static extern void Spout_copy_ctor(IntPtr thisptr, IntPtr otherptr);

    [DllImport(DllName, EntryPoint = "??1Spout@@UEAA@XZ", CallingConvention = ThisCall)]
    public static extern void Spout_dtor(IntPtr thisptr);

    // Spout class methods from documentation
    [DllImport(DllName, EntryPoint = "?SetSenderName@Spout@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void Spout_SetSenderName(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?SetSenderFormat@Spout@@QEAAXK@Z", CallingConvention = ThisCall)]
    public static extern void Spout_SetSenderFormat(IntPtr thisptr, uint dwFormat);

    [DllImport(DllName, EntryPoint = "?ReleaseSender@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void Spout_ReleaseSender(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?SendFbo@Spout@@QEAA_NIII_N@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SendFbo(IntPtr thisptr, uint FboID, uint width, uint height, bool bInvert);

    [DllImport(DllName, EntryPoint = "?SendTexture@Spout@@QEAA_NIIII_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SendTexture(IntPtr thisptr, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?SendImage@Spout@@QEAA_NPEBEIIH_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SendImage(IntPtr thisptr, IntPtr pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?IsInitialized@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsInitialized(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetName@Spout@@QEAAPEBDXZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_GetName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetWidth@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint Spout_GetWidth(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetHeight@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint Spout_GetHeight(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetFps@Spout@@QEAANXZ", CallingConvention = ThisCall)]
    public static extern double Spout_GetFps(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetFrame@Spout@@QEAAJXZ", CallingConvention = ThisCall)]
    public static extern long Spout_GetFrame(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetHandle@Spout@@QEAAPEAXXZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_GetHandle(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetCPU@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_GetCPU(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetGLDX@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_GetGLDX(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?SetReceiverName@Spout@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void Spout_SetReceiverName(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?GetReceiverName@Spout@@QEAA_NPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetReceiverName(IntPtr thisptr, StringBuilder sendername, int maxchars);

    [DllImport(DllName, EntryPoint = "?ReleaseReceiver@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void Spout_ReleaseReceiver(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NPEADAEAI1II_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_ReceiveTexture(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEADAEAI1PEAEH_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_ReceiveImage(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, IntPtr pixels, uint glFormat, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?IsUpdated@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsUpdated(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsConnected@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsConnected(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameNew@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsFrameNew(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderName@Spout@@QEAAPEBDXZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_GetSenderName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderWidth@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint Spout_GetSenderWidth(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderHeight@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint Spout_GetSenderHeight(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFormat@Spout@@QEAAKXZ", CallingConvention = ThisCall)]
    public static extern uint Spout_GetSenderFormat(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFps@Spout@@QEAANXZ", CallingConvention = ThisCall)]
    public static extern double Spout_GetSenderFps(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFrame@Spout@@QEAAJXZ", CallingConvention = ThisCall)]
    public static extern long Spout_GetSenderFrame(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderHandle@Spout@@QEAAPEAXXZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_GetSenderHandle(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderTexture@Spout@@QEAAPEAPEAUID3D11Texture2D@@XZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_GetSenderTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderCPU@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_GetSenderCPU(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderGLDX@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_GetSenderGLDX(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderList@Spout@@QEAA?AV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@XZ", CallingConvention = ThisCall)]
    public static extern StdVector Spout_GetSenderList(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderIndex@Spout@@QEAAHPEBD@Z", CallingConvention = ThisCall)]
    public static extern int Spout_GetSenderIndex(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?SelectSender@Spout@@QEAA_NPEAUHWND__@@XZ", CallingConvention = ThisCall)]
    public static extern bool Spout_SelectSender(IntPtr thisptr, IntPtr hwnd);

    [DllImport(DllName, EntryPoint = "?SetFrameCount@Spout@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void Spout_SetFrameCount(IntPtr thisptr, bool bEnable);

    [DllImport(DllName, EntryPoint = "?DisableFrameCount@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void Spout_DisableFrameCount(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsFrameCountEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?HoldFps@Spout@@QEAAXH@Z", CallingConvention = ThisCall)]
    public static extern void Spout_HoldFps(IntPtr thisptr, int fps);

    [DllImport(DllName, EntryPoint = "?SetFrameSync@Spout@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void Spout_SetFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport(DllName, EntryPoint = "?WaitFrameSync@Spout@@QEAA_NPEBDK@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_WaitFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string SenderName, uint dwTimeout);

    [DllImport(DllName, EntryPoint = "?EnableFrameSync@Spout@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void Spout_EnableFrameSync(IntPtr thisptr, bool bSync);

    [DllImport(DllName, EntryPoint = "?CloseFrameSync@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void Spout_CloseFrameSync(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsFrameSyncEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderCount@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int Spout_GetSenderCount(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSender@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetSender(IntPtr thisptr, int index, StringBuilder sendername, int MaxSize);

    [DllImport(DllName, EntryPoint = "?GetSenderInfo@Spout@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetSenderInfo(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername, ref uint width, ref uint height, ref IntPtr dxShareHandle, ref uint dwFormat);

    [DllImport(DllName, EntryPoint = "?GetActiveSender@Spout@@QEAA_NPEAD@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetActiveSender(IntPtr thisptr, StringBuilder sendername);

    [DllImport(DllName, EntryPoint = "?SetActiveSender@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SetActiveSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?GetNumAdapters@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int Spout_GetNumAdapters(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetAdapterName(IntPtr thisptr, int index, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?AdapterName@Spout@@QEAAPEADXZ", CallingConvention = ThisCall)]
    public static extern IntPtr Spout_AdapterName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetAdapter@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int Spout_GetAdapter(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderAdapter@Spout@@QEAAHPEBDPEADH@Z", CallingConvention = ThisCall)]
    public static extern int Spout_GetSenderAdapter(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetAdapterInfo(IntPtr thisptr, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NHPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetAdapterInfo(IntPtr thisptr, int index, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetPerformancePreference@Spout@@QEAAHPEBD@Z", CallingConvention = ThisCall)]
    public static extern int Spout_GetPerformancePreference(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?SetPerformancePreference@Spout@@QEAA_NHPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SetPerformancePreference(IntPtr thisptr, int preference, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetPreferredAdapterName(IntPtr thisptr, int preference, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@Spout@@QEAA_NH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SetPreferredAdapter(IntPtr thisptr, int preference);

    [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_IsPreferenceAvailable(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsApplicationPath@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_IsApplicationPath(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?FindNVIDIA@Spout@@QEAA_NAEAH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_FindNVIDIA(IntPtr thisptr, ref int nAdapter);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NPEAD0000H@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_GetAdapterInfo(IntPtr thisptr, StringBuilder renderadapter, StringBuilder renderdescription, StringBuilder renderversion, StringBuilder displaydescription, StringBuilder displayversion, int maxsize);

    [DllImport(DllName, EntryPoint = "?CreateSender@Spout@@QEAA_NPEBDIIK@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_CreateSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string Sendername, uint width, uint height, uint dwFormat);

    [DllImport(DllName, EntryPoint = "?UpdateSender@Spout@@QEAA_NPEBDII@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_UpdateSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string Sendername, uint width, uint height);

    [DllImport(DllName, EntryPoint = "?CreateReceiver@Spout@@QEAA_NPEADAEAI1@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_CreateReceiver(IntPtr thisptr, StringBuilder Sendername, ref uint width, ref uint height);

    [DllImport(DllName, EntryPoint = "?CheckReceiver@Spout@@QEAA_NPEADAEAI1AEA_N@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_CheckReceiver(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, ref bool bConnected);

    [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool Spout_ReceiveTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEAEH_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_ReceiveImage(IntPtr thisptr, IntPtr pixels, uint glFormat, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?SelectSenderPanel@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_SelectSenderPanel(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string message);

    [DllImport(DllName, EntryPoint = "?CheckSpoutPanel@Spout@@QEAA_NPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_CheckSpoutPanel(IntPtr thisptr, StringBuilder sendername, int maxchars);

    [DllImport(DllName, EntryPoint = "?DrawSharedTexture@Spout@@QEAA_NMMM_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_DrawSharedTexture(IntPtr thisptr, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?DrawToSharedTexture@Spout@@QEAA_NIIIMMM_NI@Z", CallingConvention = ThisCall)]
    public static extern bool Spout_DrawToSharedTexture(IntPtr thisptr, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

    // SpoutSender class constructors and destructor
    [DllImport(DllName, EntryPoint = "??0SpoutSender@@QEAA@XZ", CallingConvention = ThisCall)]
    public static extern void SpoutSender_ctor(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "??0SpoutSender@@QEAA@AEBV0@@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_copy_ctor(IntPtr thisptr, IntPtr otherptr);

    [DllImport(DllName, EntryPoint = "??1SpoutSender@@QEAA@XZ", CallingConvention = ThisCall)]
    public static extern void SpoutSender_dtor(IntPtr thisptr);

    // SpoutSender class methods from documentation (sender-specific)
    [DllImport(DllName, EntryPoint = "?AdapterName@SpoutSender@@QEAAPEADXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutSender_AdapterName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?BindSharedTexture@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_BindSharedTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CloseFrameSync@SpoutSender@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutSender_CloseFrameSync(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CloseOpenGL@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_CloseOpenGL(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CopyTexture@SpoutSender@@QEAA_NIIIIII_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_CopyTexture(IntPtr thisptr, uint srcTextureID, uint srcTextureTarget, uint dstTextureID, uint dstTextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?CreateSender@SpoutSender@@QEAA_NPEBDIIK@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_CreateSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, uint width, uint height, uint format);

    [DllImport(DllName, EntryPoint = "?CreateMemoryBuffer@SpoutSender@@QEAA_NPEBDH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_CreateMemoryBuffer(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, int size);

    [DllImport(DllName, EntryPoint = "?DX11format@SpoutSender@@QEAA?AW4DXGI_FORMAT@@H@Z", CallingConvention = ThisCall)]
    public static extern uint SpoutSender_DX11format(IntPtr thisptr, int format);

    [DllImport(DllName, EntryPoint = "?UnBindSharedTexture@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_UnBindSharedTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?UpdateSender@SpoutSender@@QEAA_NPEBDII@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_UpdateSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, uint width, uint height);

    [DllImport(DllName, EntryPoint = "?WaitFrameSync@SpoutSender@@QEAA_NPEBDK@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_WaitFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, uint timeout);

    [DllImport(DllName, EntryPoint = "?WriteMemoryBuffer@SpoutSender@@QEAA_NPEBD0H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_WriteMemoryBuffer(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, IntPtr buffer, int size);

    [DllImport(DllName, EntryPoint = "?SetSenderName@SpoutSender@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_SetSenderName(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?SetSenderFormat@SpoutSender@@QEAAXK@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_SetSenderFormat(IntPtr thisptr, uint dwFormat);

    [DllImport(DllName, EntryPoint = "?ReleaseSender@SpoutSender@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutSender_ReleaseSender(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?SendFbo@SpoutSender@@QEAA_NIII_N@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_SendFbo(IntPtr thisptr, uint FboID, uint fbowidth, uint fboheight, bool bInvert);

    [DllImport(DllName, EntryPoint = "?SendTexture@SpoutSender@@QEAA_NIIII_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_SendTexture(IntPtr thisptr, uint TextureID, uint TextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?SendImage@SpoutSender@@QEAA_NPEBEIIH_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_SendImage(IntPtr thisptr, IntPtr pixels, uint width, uint height, uint glFormat, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?IsInitialized@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_IsInitialized(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetName@SpoutSender@@QEAAPEBDXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutSender_GetName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetWidth@SpoutSender@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint SpoutSender_GetWidth(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetHeight@SpoutSender@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint SpoutSender_GetHeight(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetFps@SpoutSender@@QEAANXZ", CallingConvention = ThisCall)]
    public static extern double SpoutSender_GetFps(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetFrame@SpoutSender@@QEAAJXZ", CallingConvention = ThisCall)]
    public static extern long SpoutSender_GetFrame(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetHandle@SpoutSender@@QEAAPEAXXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutSender_GetHandle(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetCPU@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetCPU(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetGLDX@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetGLDX(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?SetFrameCount@SpoutSender@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_SetFrameCount(IntPtr thisptr, bool bEnable);

    [DllImport(DllName, EntryPoint = "?DisableFrameCount@SpoutSender@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutSender_DisableFrameCount(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_IsFrameCountEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?HoldFps@SpoutSender@@QEAAXH@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_HoldFps(IntPtr thisptr, int fps);

    [DllImport(DllName, EntryPoint = "?SetFrameSync@SpoutSender@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_SetFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport(DllName, EntryPoint = "?EnableFrameSync@SpoutSender@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void SpoutSender_EnableFrameSync(IntPtr thisptr, bool bSync);

    [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_IsFrameSyncEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetNumAdapters@SpoutSender@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int SpoutSender_GetNumAdapters(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetAdapterName@SpoutSender@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetAdapterName(IntPtr thisptr, int index, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapter@SpoutSender@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int SpoutSender_GetAdapter(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderAdapter@SpoutSender@@QEAAHPEBDPEADH@Z", CallingConvention = ThisCall)]
    public static extern int SpoutSender_GetSenderAdapter(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutSender@@QEAA_NPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetAdapterInfo(IntPtr thisptr, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutSender@@QEAA_NHPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetAdapterInfo(IntPtr thisptr, int index, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetPerformancePreference@SpoutSender@@QEAAHPEBD@Z", CallingConvention = ThisCall)]
    public static extern int SpoutSender_GetPerformancePreference(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?SetPerformancePreference@SpoutSender@@QEAA_NHPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_SetPerformancePreference(IntPtr thisptr, int preference, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@SpoutSender@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetPreferredAdapterName(IntPtr thisptr, int preference, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@SpoutSender@@QEAA_NH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_SetPreferredAdapter(IntPtr thisptr, int preference);

    [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@SpoutSender@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_IsPreferenceAvailable(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsApplicationPath@SpoutSender@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_IsApplicationPath(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?FindNVIDIA@SpoutSender@@QEAA_NAEAH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_FindNVIDIA(IntPtr thisptr, ref int nAdapter);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@SpoutSender@@QEAA_NPEAD0000H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_GetAdapterInfo(IntPtr thisptr, StringBuilder renderadapter, StringBuilder renderdescription, StringBuilder renderversion, StringBuilder displaydescription, StringBuilder displayversion, int maxsize);

    [DllImport(DllName, EntryPoint = "?DrawSharedTexture@SpoutSender@@QEAA_NMMM_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_DrawSharedTexture(IntPtr thisptr, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?DrawToSharedTexture@SpoutSender@@QEAA_NIIIMMM_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutSender_DrawToSharedTexture(IntPtr thisptr, uint TextureID, uint TextureTarget, uint width, uint height, float max_x, float max_y, float aspect, bool bInvert, uint HostFBO);

    // SpoutReceiver class constructors and destructor
    [DllImport(DllName, EntryPoint = "??0SpoutReceiver@@QEAA@XZ", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_ctor(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "??0SpoutReceiver@@QEAA@AEBV0@@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_copy_ctor(IntPtr thisptr, IntPtr otherptr);

    [DllImport(DllName, EntryPoint = "??1SpoutReceiver@@QEAA@XZ", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_dtor(IntPtr thisptr);

    // SpoutReceiver class methods from documentation (receiver-specific)
    [DllImport(DllName, EntryPoint = "?AdapterName@SpoutReceiver@@QEAAPEADXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutReceiver_AdapterName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?BindSharedTexture@SpoutReceiver@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_BindSharedTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CheckReceiver@SpoutReceiver@@QEAA_NPEADAEAI1AEA_N@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CheckReceiver(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, ref bool bConnected);

    [DllImport(DllName, EntryPoint = "?CheckSenderPanel@SpoutReceiver@@QEAA_NPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CheckSenderPanel(IntPtr thisptr, StringBuilder sendername, int maxchars);

    [DllImport(DllName, EntryPoint = "?CloseFrameSync@SpoutReceiver@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_CloseFrameSync(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CloseOpenGL@SpoutReceiver@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CloseOpenGL(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?CopyTexture@SpoutReceiver@@QEAA_NIIIIII_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CopyTexture(IntPtr thisptr, uint srcTextureID, uint srcTextureTarget, uint dstTextureID, uint dstTextureTarget, uint width, uint height, bool bInvert, uint HostFBO);

    [DllImport(DllName, EntryPoint = "?CreateReceiver@SpoutReceiver@@QEAA_NPEADAEAI1@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CreateReceiver(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height);

    [DllImport(DllName, EntryPoint = "?DX11format@SpoutReceiver@@QEAA?AW4DXGI_FORMAT@@H@Z", CallingConvention = ThisCall)]
    public static extern uint SpoutReceiver_DX11format(IntPtr thisptr, int format);

    [DllImport(DllName, EntryPoint = "?UnBindSharedTexture@SpoutReceiver@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_UnBindSharedTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?WaitFrameSync@SpoutReceiver@@QEAA_NPEBDK@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_WaitFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name, uint timeout);

    [DllImport(DllName, EntryPoint = "?SetReceiverName@Spout@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_SetReceiverName(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?GetReceiverName@Spout@@QEAA_NPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetReceiverName(IntPtr thisptr, StringBuilder sendername, int maxchars);

    [DllImport(DllName, EntryPoint = "?ReleaseReceiver@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_ReleaseReceiver(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_ReceiveTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NII_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_ReceiveTexture(IntPtr thisptr, uint TextureID, uint TextureTarget, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?ReceiveTexture@Spout@@QEAA_NPEADAEAI1II_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_ReceiveTexture(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, uint TextureID, uint TextureTarget, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEADAEAI1PEAEH_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_ReceiveImage(IntPtr thisptr, StringBuilder name, ref uint width, ref uint height, IntPtr pixels, uint glFormat, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?ReceiveImage@Spout@@QEAA_NPEAEI_NI@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_ReceiveImage(IntPtr thisptr, IntPtr pixels, uint glFormat, bool bInvert, uint HostFbo);

    [DllImport(DllName, EntryPoint = "?IsUpdated@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsUpdated(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsConnected@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsConnected(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameNew@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsFrameNew(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderName@Spout@@QEAAPEBDXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutReceiver_GetSenderName(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderWidth@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint SpoutReceiver_GetSenderWidth(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderHeight@Spout@@QEAAIXZ", CallingConvention = ThisCall)]
    public static extern uint SpoutReceiver_GetSenderHeight(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFormat@Spout@@QEAAKXZ", CallingConvention = ThisCall)]
    public static extern uint SpoutReceiver_GetSenderFormat(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFps@Spout@@QEAANXZ", CallingConvention = ThisCall)]
    public static extern double SpoutReceiver_GetSenderFps(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderFrame@Spout@@QEAAJXZ", CallingConvention = ThisCall)]
    public static extern long SpoutReceiver_GetSenderFrame(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderHandle@Spout@@QEAAPEAXXZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutReceiver_GetSenderHandle(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderTexture@Spout@@QEAAPEAPEAUID3D11Texture2D@@XZ", CallingConvention = ThisCall)]
    public static extern IntPtr SpoutReceiver_GetSenderTexture(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderCPU@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetSenderCPU(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderGLDX@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetSenderGLDX(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderList@Spout@@QEAA?AV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@XZ", CallingConvention = ThisCall)]
    public static extern StdVector SpoutReceiver_GetSenderList(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderIndex@Spout@@QEAAHPEBD@Z", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetSenderIndex(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?SelectSender@Spout@@QEAA_NPEAUHWND__@@XZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_SelectSender(IntPtr thisptr, IntPtr hwnd);

    [DllImport(DllName, EntryPoint = "?SetFrameCount@Spout@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_SetFrameCount(IntPtr thisptr, bool bEnable);

    [DllImport(DllName, EntryPoint = "?DisableFrameCount@Spout@@QEAAXXZ", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_DisableFrameCount(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsFrameCountEnabled@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsFrameCountEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?HoldFps@Spout@@QEAAXH@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_HoldFps(IntPtr thisptr, int fps);

    [DllImport(DllName, EntryPoint = "?SetFrameSync@Spout@@QEAAXPEBD@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_SetFrameSync(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string name);

    [DllImport(DllName, EntryPoint = "?EnableFrameSync@Spout@@QEAAX_N@Z", CallingConvention = ThisCall)]
    public static extern void SpoutReceiver_EnableFrameSync(IntPtr thisptr, bool bSync);

    [DllImport(DllName, EntryPoint = "?IsFrameSyncEnabled@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsFrameSyncEnabled(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderCount@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetSenderCount(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSender@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetSender(IntPtr thisptr, int index, StringBuilder sendername, int MaxSize);

    [DllImport(DllName, EntryPoint = "?GetSenderInfo@Spout@@QEAA_NPEBDAEAI1AEAPEAXAEAK@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetSenderInfo(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername, ref uint width, ref uint height, ref IntPtr dxShareHandle, ref uint dwFormat);

    [DllImport(DllName, EntryPoint = "?GetActiveSender@Spout@@QEAA_NPEAD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetActiveSender(IntPtr thisptr, StringBuilder sendername);

    [DllImport(DllName, EntryPoint = "?SetActiveSender@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_SetActiveSender(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername);

    [DllImport(DllName, EntryPoint = "?GetNumAdapters@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetNumAdapters(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetAdapterName(IntPtr thisptr, int index, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapter@Spout@@QEAAHXZ", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetAdapter(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSenderAdapter@Spout@@QEAAHPEBDPEADH@Z", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetSenderAdapter(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string sendername, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetAdapterInfo(IntPtr thisptr, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NHPEAD0H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetAdapterInfo(IntPtr thisptr, int index, StringBuilder description, StringBuilder output, int maxchars);

    [DllImport(DllName, EntryPoint = "?GetPerformancePreference@Spout@@QEAAHPEBD@Z", CallingConvention = ThisCall)]
    public static extern int SpoutReceiver_GetPerformancePreference(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?SetPerformancePreference@Spout@@QEAA_NHPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_SetPerformancePreference(IntPtr thisptr, int preference, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?GetPreferredAdapterName@Spout@@QEAA_NHPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetPreferredAdapterName(IntPtr thisptr, int preference, StringBuilder adaptername, int maxchars);

    [DllImport(DllName, EntryPoint = "?SetPreferredAdapter@Spout@@QEAA_NH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_SetPreferredAdapter(IntPtr thisptr, int preference);

    [DllImport(DllName, EntryPoint = "?IsPreferenceAvailable@Spout@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsPreferenceAvailable(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?IsApplicationPath@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_IsApplicationPath(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?FindNVIDIA@Spout@@QEAA_NAEAH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_FindNVIDIA(IntPtr thisptr, ref int nAdapter);

    [DllImport(DllName, EntryPoint = "?GetAdapterInfo@Spout@@QEAA_NPEAD0000H@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_GetAdapterInfo(IntPtr thisptr, StringBuilder renderadapter, StringBuilder renderdescription, StringBuilder renderversion, StringBuilder displaydescription, StringBuilder displayversion, int maxsize);

    [DllImport(DllName, EntryPoint = "?SelectSenderPanel@Spout@@QEAA_NPEBD@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_SelectSenderPanel(IntPtr thisptr, [MarshalAs(UnmanagedType.LPStr)] string message);

    [DllImport(DllName, EntryPoint = "?CheckSpoutPanel@Spout@@QEAA_NPEADH@Z", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CheckSpoutPanel(IntPtr thisptr, StringBuilder sendername, int maxchars);

    [DllImport(DllName, EntryPoint = "?CreateOpenGL@SpoutReceiver@@QEAA_NXZ", CallingConvention = ThisCall)]
    public static extern bool SpoutReceiver_CreateOpenGL(IntPtr thisptr);

    [DllImport(DllName, EntryPoint = "?GetSharedTextureID@SpoutReceiver@@QEAAIXZ", CallingConvention = ThisCall)]
    internal static extern uint SpoutReceiver_GetSharedTextureID(IntPtr thisptr);

    // spoututils namespace functions from documentation
    [DllImport(DllName, EntryPoint = "?GetSDKversion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CDecl)]
    public static extern StdString spoututils_GetSDKversion();

    [DllImport(DllName, EntryPoint = "?GetSpoutVersion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CDecl)]
    public static extern StdString spoututils_GetSpoutVersion();

    [DllImport(DllName, EntryPoint = "?IsLaptop@spoututils@@YA_NXZ", CallingConvention = CDecl)]
    public static extern bool spoututils_IsLaptop();

    [DllImport(DllName, EntryPoint = "?GetCurrentModule@spoututils@@YAPEAUHMODULE__@@XZ", CallingConvention = CDecl)]
    public static extern IntPtr spoututils_GetCurrentModule();

    [DllImport(DllName, EntryPoint = "?GetExeVersion@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@PEBD@Z", CallingConvention = CDecl)]
    public static extern StdString spoututils_GetExeVersion([MarshalAs(UnmanagedType.LPStr)] string path);

    [DllImport(DllName, EntryPoint = "?GetExePath@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CDecl)]
    public static extern StdString spoututils_GetExePath();

    [DllImport(DllName, EntryPoint = "?GetExeName@spoututils@@YA?AV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@XZ", CallingConvention = CDecl)]
    public static extern StdString spoututils_GetExeName();

    [DllImport(DllName, EntryPoint = "?RemovePath@spoututils@@YAXAEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_RemovePath(ref StdString path);

    [DllImport(DllName, EntryPoint = "?RemoveName@spoututils@@YAXAEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_RemoveName(ref StdString path);

    [DllImport(DllName, EntryPoint = "?OpenSpoutConsole@spoututils@@YAXXZ", CallingConvention = CDecl)]
    public static extern void spoututils_OpenSpoutConsole();

    [DllImport(DllName, EntryPoint = "?CloseSpoutConsole@spoututils@@YAX_N@Z", CallingConvention = CDecl)]
    public static extern void spoututils_CloseSpoutConsole(bool bWarning);

    [DllImport(DllName, EntryPoint = "?EnableSpoutLog@spoututils@@YAXXZ", CallingConvention = CDecl)]
    public static extern void spoututils_EnableSpoutLog();

    [DllImport(DllName, EntryPoint = "?EnableSpoutLogFile@spoututils@@YAXPEBD_N@Z", CallingConvention = CDecl)]
    public static extern void spoututils_EnableSpoutLogFile([MarshalAs(UnmanagedType.LPStr)] string filename, bool bAppend);

    [DllImport(DllName, EntryPoint = "?DisableSpoutLogFile@spoututils@@YAXXZ", CallingConvention = CDecl)]
    public static extern void spoututils_DisableSpoutLogFile();

    [DllImport(DllName, EntryPoint = "?DisableSpoutLog@spoututils@@YAXXZ", CallingConvention = CDecl)]
    public static extern void spoututils_DisableSpoutLog();

    [DllImport(DllName, EntryPoint = "?SetSpoutLogLevel@spoututils@@YAXW4SpoutLogLevel@1@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_SetSpoutLogLevel(SpoutLogLevel level);

    [DllImport(DllName, EntryPoint = "?SpoutLog@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLog([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutLogVerbose@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLogVerbose([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutLogNotice@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLogNotice([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutLogWarning@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLogWarning([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutLogError@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLogError([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutLogFatal@spoututils@@YAXPEBDZZ", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutLogFatal([MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEBDK@Z", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox([MarshalAs(UnmanagedType.LPStr)] string message, uint dwMilliseconds);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEBD0ZZ", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox([MarshalAs(UnmanagedType.LPStr)] string caption, [MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEBD1I0ZZ", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox([MarshalAs(UnmanagedType.LPStr)] string caption, uint uType, [MarshalAs(UnmanagedType.LPStr)] string format, __arglist);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IK@Z", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string caption, uint uType, uint dwMilliseconds);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1I1K@Z", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string caption, uint uType, [MarshalAs(UnmanagedType.LPStr)] string instruction, uint dwMilliseconds);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IAEAV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string caption, uint uType, ref StdString text);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBox@spoututils@@YAHPEAUHWND__@@PEBD1IV?$vector@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@V?$allocator@V?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@2@@std@@AEAH@Z", CallingConvention = CDecl)]
    public static extern int spoututils_SpoutMessageBox(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string message, [MarshalAs(UnmanagedType.LPStr)] string caption, uint uType, StdVector items, ref int selected);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBoxIcon@spoututils@@YAXPEAUHICON__@@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutMessageBoxIcon(IntPtr hIcon);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBoxIcon@spoututils@@YA_NV?$basic_string@DU?$char_traits@D@std@@V?$allocator@D@2@@std@@@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_SpoutMessageBoxIcon(StdString iconfile);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBoxModeless@spoututils@@YA_N_N@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_SpoutMessageBoxModeless(bool bMode);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBoxWindow@spoututils@@YAXPEAUHWND__@@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutMessageBoxWindow(IntPtr hWnd);

    [DllImport(DllName, EntryPoint = "?SpoutMessageBoxPosition@spoututils@@YAXUtagPOINT@@@Z", CallingConvention = CDecl)]
    public static extern void spoututils_SpoutMessageBoxPosition(POINT pt);

    [DllImport(DllName, EntryPoint = "?CopyToClipBoard@spoututils@@YA_NPEAUHWND__@@PEBD@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_CopyToClipBoard(IntPtr hwnd, [MarshalAs(UnmanagedType.LPStr)] string text);

    [DllImport(DllName, EntryPoint = "?ReadDwordFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1PEAK@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_ReadDwordFromRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename, ref uint pValue);

    [DllImport(DllName, EntryPoint = "?WriteDwordToRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1K@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_WriteDwordToRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename, uint dwValue);

    [DllImport(DllName, EntryPoint = "?ReadPathFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1PEADK@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_ReadPathFromRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename, StringBuilder filepath, uint dwSize);

    [DllImport(DllName, EntryPoint = "?WritePathToRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD11@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_WritePathToRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename, [MarshalAs(UnmanagedType.LPStr)] string filepath);

    [DllImport(DllName, EntryPoint = "?WriteBinaryToRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1PEBEK@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_WriteBinaryToRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename, IntPtr hexdata, uint nChars);

    [DllImport(DllName, EntryPoint = "?RemovePathFromRegistry@spoututils@@YA_NPEAUHKEY__@@PEBD1@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_RemovePathFromRegistry(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey, [MarshalAs(UnmanagedType.LPStr)] string valuename);

    [DllImport(DllName, EntryPoint = "?RemoveSubKey@spoututils@@YA_NPEAUHKEY__@@PEBD@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_RemoveSubKey(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey);

    [DllImport(DllName, EntryPoint = "?FindSubKey@spoututils@@YA_NPEAUHKEY__@@PEBD@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_FindSubKey(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string subkey);

    [DllImport(DllName, EntryPoint = "?ElapsedMicroseconds@spoututils@@YANXZ", CallingConvention = CDecl)]
    public static extern double spoututils_ElapsedMicroseconds();

    [DllImport(DllName, EntryPoint = "?GetRefreshRate@spoututils@@YANXZ", CallingConvention = CDecl)]
    public static extern double spoututils_GetRefreshRate();

    [DllImport(DllName, EntryPoint = "?ExecuteProcess@spoututils@@YA_NPEBD0@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_ExecuteProcess([MarshalAs(UnmanagedType.LPStr)] string path, [MarshalAs(UnmanagedType.LPStr)] string commandline);

    [DllImport(DllName, EntryPoint = "?OpenSpoutPanel@spoututils@@YA_NPEBD@Z", CallingConvention = CDecl)]
    public static extern bool spoututils_OpenSpoutPanel([MarshalAs(UnmanagedType.LPStr)] string message);
}