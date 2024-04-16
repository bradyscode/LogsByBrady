using LogsByBrady.Enums;

namespace LogsByBrady.Interfaces
{
    public interface ILogger
    {
        object GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider);
        Task Log(object message, string path);
    }
}