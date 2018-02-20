using System.Collections.Generic;
using BusinessLogicLayer.LoggerInterfaces;

namespace BusinessLogicLayer.Loggers
{
    public class LoggerManager : ILoggerManager

    {
        private readonly List<IErrorLogger> _errorLoggers = new List<IErrorLogger>();
        private readonly List<ILogger> _loggers;
        private readonly List<IRecordLogger> _recordLoggers = new List<IRecordLogger>();
        private readonly List<IWarningLogger> _warningLoggers = new List<IWarningLogger>();

        public LoggerManager(List<ILogger> loggers)
        {
            _loggers = loggers;
            foreach (var logger in loggers)
            {
                if (logger is IRecordLogger recordLogger)
                    _recordLoggers.Add(recordLogger);

                if (logger is IWarningLogger warningLogger)
                    _warningLoggers.Add(warningLogger);

                if (logger is IErrorLogger errorLogger)
                    _errorLoggers.Add(errorLogger);
            }
        }

        public void LogRecord<T>(T record)
        {
            _recordLoggers.ForEach(x => x.LogRecord(record));
        }

        public void LogWarning<T>(T record)
        {
            _warningLoggers.ForEach(x => x.LogWarning(record));
        }

        public IEnumerable<ILogger> GetLoggers()
        {
            return _loggers;
        }

        public void LogError<T>(T record)
        {
            _errorLoggers.ForEach(x => x.LogError(record));
        }
    }
}