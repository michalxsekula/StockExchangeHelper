using System.IO;
using BusinessLogicLayer.ExtensionMethods;
using BusinessLogicLayer.LoggerInterfaces;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.LoggerAdaptors
{
    public class XmlLoggerAdapator : IRecordLogger
    {
        private readonly XmlReportsCreator<ExchangeRate> _xmlReportsCreator = new XmlReportsCreator<ExchangeRate>();

        public void LogRecord<T>(T record)
        {
            const string reportsPath = @"C:\temp\";
            if (!Directory.Exists(reportsPath))
                Directory.CreateDirectory(reportsPath);

            _xmlReportsCreator.SaveToXmlReport(record as ExchangeRate, reportsPath);
        }
    }
}