using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using Spout.Interop;

namespace test;

public class Receiver : OpenTKWindow, IDisposable
{
    private SpoutReceiver receiver;
    private string name;
    private int textureUnit = -1;

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
        //Spout.Interop.Spoututils.SpoutUtils.EnableLogs();

        // Unnecessary?
        // https://github.com/leadedge/Spout2/issues/119#issuecomment-2574113975
        //receiver.SetReceiverName(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        textureUnit = -1;
        if(receiver.ReceiveTexture())
        {
            textureUnit = (int)receiver.SharedTextureID;

            if(receiver.IsUpdated)
            {
                // ummm...
            }
        }

        base.OnRenderFrame(e);
    }

    internal void SetTextureUniformCallback()
    {
        if (textureUnit == -1) return;
        _ = receiver.BindSharedTexture();
        GL.Uniform1(Shader.Uniforms["receivedTexture"].Location, textureUnit);
        _ = receiver.UnBindSharedTexture;
    }

    public new void Dispose()
    {
        base.Dispose();
    }
}
