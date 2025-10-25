using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using Spout.Interop;
using Spout.Interop.Spoututils;

namespace test;

// This version of the receiver attempts to use the shared texture directly

// Lynn says there is a bug in the non-allocating ReceiveTexture()
// https://github.com/leadedge/Spout2/issues/128#issuecomment-3368664300

public class Receiver : OpenTKWindow, IDisposable
{
    private SpoutReceiver receiver;
    private string name;
    private int textureID = -1;

    // Current assumption, we may need to use context sharing and coordinate texture
    // lifecycle. https://github.com/leadedge/Spout2/issues/128#issuecomment-3446737212

    public Receiver(EyeCandyWindowConfig windowConfig, string spoutName)
        : base(windowConfig, "passthrough.vert", "receiver.frag")
    {
        name = spoutName;
        OpenGLUtils.SetTextureUniformCallback = SetTextureUniformCallback;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        // writes to %AppData%\Spout (paste that into File Explorer)
        //SpoutUtils.EnableSpoutLogFile("test.log", false);
        //SpoutUtils.SetSpoutLogLevel(SpoutLogLevel.SPOUT_LOG_VERBOSE);

        receiver = new();
        if (!string.IsNullOrWhiteSpace(name)) receiver.SetActiveSender(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        if (receiver.ReceiveTexture()) // should auto-connect
        {
            if (receiver.IsUpdated)
            {
                textureID = (int)receiver.SharedTextureID;
                //SpoutUtils.SpoutLogNotice($"-------- SharedTextureID {textureID}");
            }
        }

        // this calls OpenGLU SetUniforms and Draw
        base.OnRenderFrame(e);
    }

    // called from OpenGLUtils.SetUniforms via base class OnRenderFrame
    internal void SetTextureUniformCallback()
    {
        if (textureID == -1) return;
        //GL.ActiveTexture(TextureUnit.Texture0);
        receiver.BindSharedTexture();
        Shader.SetTexture("receivedTexture", textureID, TextureUnit.Texture0);
        _ = receiver.UnBindSharedTexture;
    }

    public new void Dispose()
    {
        base.Dispose();
        receiver.Dispose();
    }
}
