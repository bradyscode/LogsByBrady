namespace logs_by_brady
{
    public interface ILogger
    {
        public string Message { get; set; }
        string Log(string logLevel, string message);
    }
}