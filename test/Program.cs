using eyecandy;

namespace test;

internal class Program
{
    internal static BaseWindow Window;

    static void Main(string[] args)
    {
        Console.WriteLine("\nOpenTK Spout Test\n\n");

        if(args.Length < 1 || args.Length > 2)
        {
            Help();
            return;
        }

        var spoutName = args.Length == 2 ? args[1] : "test";

        var windowConfig = new EyeCandyWindowConfig();
        windowConfig.OpenTKNativeWindowSettings.Title = "opentk-spout-test";
        windowConfig.OpenTKNativeWindowSettings.ClientSize = (960, 540);
        windowConfig.StartFullScreen = false;
        windowConfig.OpenTKNativeWindowSettings.APIVersion = new Version(4, 5);

        args[0] = args[0].Trim().ToLowerInvariant();
        if (args[0] == "sender") Window = new Sender(windowConfig, spoutName);
        if (args[0] == "receiver") Window = new Receiver(windowConfig, spoutName);
        if (args[0] == "alloc") Window = new AllocatingReceiver(windowConfig, spoutName);

        if(Window is null)
        {
            Help();
            return;
        }

        Window.Focus();
        Window.Run();
        Window.Dispose();
    }

    static void Help()
    {
        Console.WriteLine("Usage: opentk-spout-test [sender|receiver|alloc] \"[name]\"");
        Console.WriteLine("  sender   - transmit shader output frames");
        Console.WriteLine("  receiver - apply shader to input frames (shared texture)");
        Console.WriteLine("  alloc    - apply shader to input frames (internal texture)");
        Console.WriteLine("  name     - optional spout name (use quotes if name has spaces)");
    }
}
