using LogsByBrady.Enums;
using LogsByBrady.Interfaces;
using System.Reflection;

namespace LogsByBrady.FlatFile
{
    public class FileLogging : IBradysLogger
    {
        static string loggingDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        ILogger logger = new FlatFileLogger();
        static BradysFormatProvider format = DependencyInjection._bls.Format;
        static string userPath = DependencyInjection._bls.Path ?? loggingDir;
        string path = Path.Combine(userPath, $"{Assembly.GetEntryAssembly()?.GetName().Name}-{DateTime.Now.ToString("yyyy-MM-dd")}.{format.ToEnumMember()}");

        public FileLogging()
        {
            if (!File.Exists(path))
            {
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                File.Create(path).Close();
            }
        }
        public ILogger Critical(string message)
        {
            var logMessage = logger.GenerateMessage("critical", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Debug(string message)
        {
            var logMessage = logger.GenerateMessage("debug", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Error(string message)
        {
            var logMessage = logger.GenerateMessage("Error", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Exception(string message)
        {
            var logMessage = logger.GenerateMessage("Exception", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Info(string message)
        {
            var logMessage = logger.GenerateMessage("info", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Notice(string message)
        {
            var logMessage = logger.GenerateMessage("notice", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Success(string message)
        {
            var logMessage = logger.GenerateMessage("success", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Trace(string message)
        {
            var logMessage = logger.GenerateMessage("trace", message, format);
            logger.Log(logMessage, path);
            return logger;
        }

        public ILogger Warning(string message)
        {
            var logMessage = logger.GenerateMessage("warning", message, format);
            logger.Log(logMessage, path);
            return logger;
        }
    }
}