using LogsByBrady;

namespace logs_by_brady
{
    public interface ILogger
    {
        string GenerateMessage(string logLevel, string message, BradysFormatProvider bradysFormatProvider);
        Task Log(string message, string path);
    }
}