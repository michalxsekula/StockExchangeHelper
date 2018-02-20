namespace BusinessLogicLayer.LoggerInterfaces
{
    public interface IRecordLogger : ILogger
    {
        void LogRecord<T>(T record);
    }
}
