using eyecandy;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using Spout.Interop;
using Spout.Interop.Spoututils;

namespace test;

// This version of the receiver allocates its own texture instead of using the shared texture directly

public class AllocatingReceiver : OpenTKWindow, IDisposable
{

    // set to 1.0 to match the sender dimensions; use other values
    // to test Spout blitting to the allocated FBO resolution
    private const float SCALE = 0.10f;

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
        //SpoutUtils.EnableSpoutLogFile("test.log", false);
        //SpoutUtils.SetSpoutLogLevel(SpoutLogLevel.SPOUT_LOG_VERBOSE);

        receiver = new();
        if (!string.IsNullOrWhiteSpace(name)) Console.WriteLine(receiver.SetActiveSender(name));
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        if (receiver.ReceiveTexture())
        {
            _ = receiver.IsUpdated;

            int width = (int)((float)receiver.SenderWidth * SCALE);
            int height = (int)((float)receiver.SenderHeight * SCALE);
            if(width != OpenGLUtils.Width || height != OpenGLUtils.Height)
            {
                OpenGLUtils.Width = width;
                OpenGLUtils.Height = height;
                OpenGLUtils.Allocate();
            }

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
