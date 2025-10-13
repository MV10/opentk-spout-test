
using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using SpoutInterop;

namespace test;

// This version of the receiver allocates its own texture instead of using the shared texture directly

public class AllocatingReceiver : OpenTKWindow, IDisposable
{
    private const bool INVERT = true;

    private SpoutReceiver receiver;
    private string name;

    public AllocatingReceiver(EyeCandyWindowConfig windowConfig, string spoutName)
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

        SpoutUtils.SpoutLogNotice("-------- receiver ctor (entering)");
        receiver = new();
        SpoutUtils.SpoutLogNotice("-------- receiver ctor (exited)");
        //if (!string.IsNullOrWhiteSpace(name)) Console.WriteLine(receiver.SetActiveSender(name));
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        SpoutUtils.SpoutLogNotice("-------- ReceiveTexture()");
        if (receiver.ReceiveTexture())
        {
            SpoutUtils.SpoutLogNotice("-------- IsUpdated");
            _ = receiver.IsUpdated();

            int width = (int)receiver.GetWidth();
            int height = (int)receiver.GetHeight();
            if(width != OpenGLUtils.Width || height != OpenGLUtils.Height)
            {
                OpenGLUtils.Width = width;
                OpenGLUtils.Height = height;
                OpenGLUtils.Allocate();
            }

            SpoutUtils.SpoutLogNotice("-------- ReceiveTexture(tex, targ, inv, fbo)");
            receiver.ReceiveTexture((uint)OpenGLUtils.TextureHandle, (uint)TextureTarget.Texture2D, INVERT, 0);
        }

        base.OnRenderFrame(e);
    }

    // called from OpenGLUtils.SetUniforms
    internal void SetTextureUniformCallback()
    {
        if (OpenGLUtils.TextureHandle == -1) return;
        Shader.SetTexture("receivedTexture", OpenGLUtils.TextureHandle, TextureUnit.Texture0);
    }

    public new void Dispose()
    {
        base.Dispose();
        //SpoutUtils.CloseSpoutConsole(true);
        receiver.Dispose();
    }
}
