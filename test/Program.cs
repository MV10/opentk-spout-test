using eyecandy;

namespace test;

internal class Program
{
    internal static BaseWindow Window;

    static void Main(string[] args)
    {
        Console.WriteLine("\nOpenTK Spout Test\n\n");

        if(args.Length < 1 || args.Length > 2 
          || (!args[0].Equals("sender", StringComparison.InvariantCultureIgnoreCase)
          && !args[0].Equals("receiver", StringComparison.InvariantCultureIgnoreCase)))
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

        if(args[0].Equals("sender", StringComparison.InvariantCultureIgnoreCase))
            Window = new Sender(windowConfig, spoutName);
        else
            Window = new Receiver(windowConfig, spoutName);

        Window.Focus();
        Window.Run();
        Window.Dispose();
    }

    static void Help()
    {
        Console.WriteLine("Usage: opentk-spout-test [sender|receiver] \"[name]\"");
        Console.WriteLine("  sender   - transmit shader output frames");
        Console.WriteLine("  receiver - apply shader to input frames");
        Console.WriteLine("  name     - optional spout name (use quotes if name has spaces)");
    }
}
