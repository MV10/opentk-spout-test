using eyecandy;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace test;

public class OpenTKWindow : BaseWindow, IDisposable
{
    public OpenTKWindow(EyeCandyWindowConfig windowConfig, string vertexShaderPathname, string fragmentShaderPathname)
        : base(windowConfig, createShaderFromConfig: false)
    {
        Shader = new(vertexShaderPathname, fragmentShaderPathname);
        if (!Shader.IsValid) Environment.Exit(-1);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        OpenGLUtils.Initialize(Shader);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        OpenGLUtils.SetUniforms(Shader);
        OpenGLUtils.Draw();
        SwapBuffers();
        CalculateFPS();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        var input = KeyboardState;

        if (input.IsKeyReleased(Keys.Escape))
        {
            Close();
            Console.WriteLine($"\n\n{FramesPerSecond} FPS\n{AverageFramesPerSecond} average FPS, last {AverageFPSTimeframeSeconds} seconds");
            return;
        }

        if (input.IsKeyReleased(Keys.Space))
        {
            WindowState = (WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
        }
    }

    public new void Dispose()
    {
        base.Dispose();
    }
}
