using LogsByBrady;
using LogsByBrady.Enums;
using LogsByBrady.Interfaces;
using LogsByBrady.Models;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LogsByBrady.FlatFile
{
    internal class FlatFileLogger : ILogger
    {

        public string GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider)
        {
            if (bradysFormatProvider == BradysFormatProvider.Json)
            {
                var logModel = new LogModel
                {
                    Message = message,
                    LogLevel = logLevel.ToUpper()
                };
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(logModel, options);
                return jsonString + ",";
            }
            var returnMessage = $"[{logLevel.ToUpper()}] - [{DateTime.UtcNow}] : {message}";
            return returnMessage;
        }
        public async Task Log(string message, string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: true))
                {
                    await writer.WriteLineAsync(message);
                }
            }
            catch
            {
                Debug.WriteLine("Error writing to file.");
            }
        }
    }
}