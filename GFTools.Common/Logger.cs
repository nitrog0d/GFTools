using Pastel;
using System.Drawing;

namespace GFTools.Common;

public class Logger {
    private static readonly Color VERBOSE = Color.Gray;
    private static readonly Color INFO = Color.LightBlue;
    private static readonly Color WARNING = Color.Yellow;
    private static readonly Color ERROR = Color.IndianRed;

    private static bool LastNoNewline;

    public string Source;

    public Logger(string source) => Source = source;

    public void VerboseLine(string message) => Verbose($"{message}{Environment.NewLine}");
    public void InfoLine(string message) => Info($"{message}{Environment.NewLine}");
    public void WarnLine(string message) => Warn($"{message}{Environment.NewLine}");
    public void ErrorLine(string message) => Error($"{message}{Environment.NewLine}");

    public void Verbose(string message) => Write(Source, message, VERBOSE);
    public void Info(string message) => Write(Source, message, INFO);
    public void Warn(string message) => Write(Source, message, WARNING);
    public void Error(string message) => Write(Source, message, ERROR);

    public static void VerboseLine(string message, string source = "GFTools") => Verbose($"{message}{Environment.NewLine}", source);
    public static void InfoLine(string message, string source = "GFTools") => Info($"{message}{Environment.NewLine}", source);
    public static void WarnLine(string message, string source = "GFTools") => Warn($"{message}{Environment.NewLine}", source);
    public static void ErrorLine(string message, string source = "GFTools") => Error($"{message}{Environment.NewLine}", source);

    public static void Verbose(string message, string source = "GFTools") => Write(source, message, VERBOSE);
    public static void Info(string message, string source = "GFTools") => Write(source, message, INFO);
    public static void Warn(string message, string source = "GFTools") => Write(source, message, WARNING);
    public static void Error(string message, string source = "GFTools") => Write(source, message, ERROR);

    public static void Write(string source, string message, Color color) {
        if (!LastNoNewline)
            WritePrelude(source, color);

        LastNoNewline = !message.EndsWith(Environment.NewLine);

        message = message.Pastel(color);

        Console.Write(message);
    }

    private static void WritePrelude(string source, Color color) {
        var message = $"[{source}] ";
        message = message.Pastel(color);

        Console.Write(message);
    }
}
