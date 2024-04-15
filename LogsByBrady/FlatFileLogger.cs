using LogsByBrady;

using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace logs_by_brady
{
    internal class FlatFileLogger : ILogger
    {

        public string GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider)
        {
            if(bradysFormatProvider == BradysFormatProvider.Json)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Serialize(new { DateTime.UtcNow, logLevel, message }, options);
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