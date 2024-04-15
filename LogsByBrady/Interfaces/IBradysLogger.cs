namespace LogsByBrady.Interfaces
{
    public interface IBradysLogger
    {
        public abstract ILogger Error(string message);
        public abstract ILogger Info(string message);
        public abstract ILogger Debug(string message);
        public abstract ILogger Warning(string message);
        public abstract ILogger Critical(string message);
        public abstract ILogger Trace(string message);
        public abstract ILogger Success(string message);
        public abstract ILogger Notice(string message);
        public abstract ILogger Exception(string message);
    }
}