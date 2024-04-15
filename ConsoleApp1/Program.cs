using LogsByBrady.FlatFile;
using LogsByBrady.Interfaces;
public class Program
{
    static void Main(string[] args)
    {
        IBradysLogger fileLogger = new FileLogging();
        Console.WriteLine(fileLogger.Critical("Test Exception"));
        Console.WriteLine(fileLogger.Debug("Test Debug"));
        Console.WriteLine(fileLogger.Warning("Test Warning"));
        Console.WriteLine(fileLogger.Exception(new Exception().ToString()));
        Console.WriteLine(fileLogger.Info("Test Info"));
        Console.WriteLine(fileLogger.Trace("Test Trace"));
        Console.WriteLine(fileLogger.Success("Test Success"));
        Console.WriteLine(fileLogger.Notice("Test Notice {Test}"));
        Console.WriteLine(fileLogger);
    }
}
