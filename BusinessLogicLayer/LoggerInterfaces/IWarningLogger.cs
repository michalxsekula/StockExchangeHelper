namespace BusinessLogicLayer.LoggerInterfaces
{
    public interface IWarningLogger : ILogger
    {
        void LogWarning<T>(T warning);
    }
}