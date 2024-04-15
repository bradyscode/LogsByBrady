using LogsByBrady.Enums;

namespace LogsByBrady.Interfaces
{
    public interface ILogger
    {
        string GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider);
        Task Log(string message, string path);
    }
}