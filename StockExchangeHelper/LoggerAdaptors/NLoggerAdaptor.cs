using BusinessLogicLayer.LoggerInterfaces;
using NLog;

namespace StockExchangeHelper.LoggerAdaptors
{
    public class NLoggerAdaptor : IErrorLogger, IRecordLogger, IWarningLogger
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogError<T>(T error)
        {
            _logger.Error(error);
        }

        public void LogRecord<T>(T record)
        {
            _logger.Info(record);
        }

        public void LogWarning<T>(T warning)
        {
            _logger.Warn(warning);
        }
    }
}