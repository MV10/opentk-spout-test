using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using Spout.Interop;
using Spout.Interop.Spoututils;

namespace test;

// This version of the receiver attempts to use the shared texture directly
// https://github.com/leadedge/Spout2/issues/128

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

        receiver = new();

        //receiver.SetFrameCount(true);

        // writes to %AppData%\Spout (paste that into File Explorer)
        //SpoutUtils.EnableSpoutLogFile("test.log", false);
        
        // Unnecessary
        // https://github.com/leadedge/Spout2/issues/119#issuecomment-2574113975
        //receiver.SetReceiverName(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        textureID = -1;
        if(receiver.ReceiveTexture()) // should auto-connect
        {
            // necessary to call before using the texture or size info etc
            if (receiver.IsUpdated) textureID = (int)receiver.SharedTextureID;
            //Console.WriteLine(receiver.SenderFrame);
        }

        base.OnRenderFrame(e);
    }

    // called from OpenGLUtils.SetUniforms
    internal void SetTextureUniformCallback()
    {
        if (textureID == -1) return;
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
