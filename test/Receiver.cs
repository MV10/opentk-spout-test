
using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using SpoutInterop;

namespace test;

// This version of the receiver attempts to use the shared texture directly

// Lynn says there is a bug in the non-allocating ReceiveTexture()
// https://github.com/leadedge/Spout2/issues/128#issuecomment-3368664300

public class Receiver : OpenTKWindow, IDisposable
{
    private SpoutReceiver receiver;
    private string name;
    private int textureID = -1;


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
        SpoutUtils.EnableSpoutLogFile("test.log", false);
        SpoutUtils.SetSpoutLogLevel(SpoutLogLevel.Verbose);

        SpoutUtils.SpoutLogNotice("-------- receiver ctor");
        receiver = new();
        if (!string.IsNullOrWhiteSpace(name)) receiver.SetActiveSender(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        SpoutUtils.SpoutLogNotice("-------- ReceiveTexture");
        if (receiver.ReceiveTexture()) // should auto-connect
        {
            SpoutUtils.SpoutLogNotice("-------- IsUpdated");
            if (receiver.IsUpdated())
            {
                SpoutUtils.SpoutLogNotice("-------- SharedTextureID");
                textureID = (int)receiver.GetSharedTextureID();
            }
            else
            {
                textureID = -1;
            }
            SpoutUtils.SpoutLogNotice($"-------- textureID {textureID}");
        }

        base.OnRenderFrame(e);
    }

    // called from OpenGLUtils.SetUniforms
    internal void SetTextureUniformCallback()
    {
        if (textureID == -1) return;
        SpoutUtils.SpoutLogNotice("-------- BindSharedTexture");
        receiver.BindSharedTexture();
        Shader.SetTexture("receivedTexture", textureID, TextureUnit.Texture0);
        SpoutUtils.SpoutLogNotice("-------- UnBindSharedTexture");
        _ = receiver.UnBindSharedTexture();
    }

    public new void Dispose()
    {
        base.Dispose();
        receiver.Dispose();
    }
}
