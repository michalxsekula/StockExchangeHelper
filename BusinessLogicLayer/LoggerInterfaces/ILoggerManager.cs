using System.Collections.Generic;

namespace BusinessLogicLayer.LoggerInterfaces
{
    public interface ILoggerManager : IErrorLogger, IRecordLogger, IWarningLogger
    {
        IEnumerable<ILogger> GetLoggers();
    }
}