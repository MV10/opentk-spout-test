using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using Spout.Interop;
using Spout.Interop.Spoututils;

namespace test;

// This version of the receiver allocates its own texture instead of using the shared texture directly

// Work in progress -- do not use yet

public class AllocatingReceiver : OpenTKWindow, IDisposable
{
    private SpoutReceiver receiver;
    private string name;
    private int textureID = -1;

    public AllocatingReceiver(EyeCandyWindowConfig windowConfig, string spoutName)
        : base(windowConfig, "passthrough.vert", "receiver.frag")
    {
        name = spoutName;
        OpenGLUtils.SetTextureUniformCallback = SetTextureUniformCallback;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        receiver = new();

        // Doesn't seem to work...
        //SpoutUtils.OpenSpoutConsole();
        //SpoutUtils.EnableLogs();

        // Unnecessary?
        // https://github.com/leadedge/Spout2/issues/119#issuecomment-2574113975
        //receiver.SetReceiverName(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        textureID = -1;
        if (receiver.ReceiveTexture()) // should auto-connect
        {
            // necessary to call before using the texture, would
            // indicate size or sender changed, but if we use the
            // shared texture directly, then we don't store that
            _ = receiver.IsUpdated;

            textureID = (int)receiver.SharedTextureID;
        }

        base.OnRenderFrame(e);
    }

    // called from OpenGLUtils.SetUniforms
    internal void SetTextureUniformCallback()
    {
        if (textureID == -1) return;
        Shader.SetTexture("receivedTexture", textureID, TextureUnit.Texture0);
    }

    public new void Dispose()
    {
        base.Dispose();
        //SpoutUtils.CloseSpoutConsole(true);
        receiver.Dispose();
    }
}
