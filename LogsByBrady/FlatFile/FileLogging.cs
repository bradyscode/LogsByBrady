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
        List<string> paths = new List<string>();

        public FileLogging()
        {
            if (format == BradysFormatProvider.All)
            {
                foreach(var format in Enum.GetValues<BradysFormatProvider>())
                {
                    if(format!= BradysFormatProvider.All)
                        paths.Add(Path.Combine(userPath, $"{Assembly.GetEntryAssembly()?.GetName().Name}-{DateTime.Now.ToString("yyyy-MM-dd")}.{format.ToEnumMember()}"));
                }
            }
            else
            {
                paths.Add(Path.Combine(userPath, $"{Assembly.GetEntryAssembly()?.GetName().Name}-{DateTime.Now.ToString("yyyy-MM-dd")}.{format.ToEnumMember()}"));
            }

            foreach(var path in paths)
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
        }
        public ILogger Critical(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("critical", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Debug(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("debug", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Error(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("error", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Exception(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("exception", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Info(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("info", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Notice(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("notice", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Success(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("success", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Trace(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("trace", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }

        public ILogger Warning(string message)
        {
            foreach (var path in paths)
            {
                var logMessage = logger.GenerateMessage("warning", message, EnumExtensions.ToEnum<BradysFormatProvider>(Path.GetExtension(path)));
                logger.Log(logMessage, path);
            }
            return logger;
        }
    }
}