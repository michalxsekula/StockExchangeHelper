namespace BusinessLogicLayer.LoggerInterfaces
{
    public interface IErrorLogger : ILogger
    {
        void LogError<T>(T error);
    }
}