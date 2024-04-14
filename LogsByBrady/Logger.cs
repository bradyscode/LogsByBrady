namespace logs_by_brady
{
    internal class Logger : ILogger
    {
        public string Message { get; set; }

        public string Log(string logLevel, string message)
        {
            var returnMessage = $"[{logLevel.ToUpper()}] - [{DateTime.UtcNow}] : {message}";
            Message = returnMessage;
            return returnMessage;
        }
    }
}