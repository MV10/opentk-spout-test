using eyecandy;
using OpenTK.Windowing.Common;
using Spout.Interop;

namespace test;

public class Sender : OpenTKWindow, IDisposable
{
    private SpoutSender sender;
    private string name;

    public Sender(EyeCandyWindowConfig windowConfig, string spoutName)
        : base(windowConfig, "passthrough.vert", "sender.frag")
    {
        name = spoutName;
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        sender = new();
        sender.SetSenderName(name);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        // all zeros means use default framebuffer and auto-detect size
        sender?.SendFbo(0, 0, 0, true);
    }

    public new void Dispose()
    {
        base.Dispose();
        sender.Dispose();
    }
}
