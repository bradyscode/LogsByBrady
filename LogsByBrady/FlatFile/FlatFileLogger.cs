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

        public object GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider)
        {
            var logModel = new LogModel
            {
                Message = message,
                LogLevel = logLevel.ToUpper(),
            };
            if (bradysFormatProvider == BradysFormatProvider.Json)
            {

                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                var jsonString = JsonSerializer.Serialize(logModel, options);
                return jsonString + ",";
            }
            var returnMessage = $"[{logModel.LogLevel.ToUpper()}] {logModel.CallingProject}/{logModel.CallingClass} - [{DateTime.UtcNow}] Thread #: {logModel.ManagedThreadId} : {message}";
            return returnMessage;
        }
        public async Task Log(object message, string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append: true))
                {
                    await writer.WriteLineAsync(message.ToString());
                }
            }
            catch
            {
                Debug.WriteLine("Error writing to file.");
            }
        }
    }
}