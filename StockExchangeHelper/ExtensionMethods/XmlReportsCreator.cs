using System.Collections.Generic;
using System.IO;
using System.Linq;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.ExtensionMethods
{
    public static class XmlReportsCreator
    {
        public const string Path = @"C:\temp\xmlReport.xml";
        private static List<ExchangeRate> _exchangeRates;

        public static void SaveToXmlReport(this ExchangeRate exchangeRate)
        {
            if (!File.Exists(Path))
            {
                _exchangeRates = new List<ExchangeRate> {exchangeRate};
                SaveDataToFile();
                return;
            }

            AppendInformationToExistingReport(exchangeRate);
        }

        private static void SaveDataToFile()
        {
            File.Create(Path).Dispose();
            using (TextWriter tw = new StreamWriter(Path))
            {
                tw.Write(_exchangeRates.SerializeObjectToXml());
                tw.Close();
            }
        }

        private static void AppendInformationToExistingReport(ExchangeRate exchangeRate)
        {
            var xmlReport = LoadXmlFile();

            _exchangeRates = xmlReport.ParseXmlToObject<List<ExchangeRate>>();
            exchangeRate.Id = _exchangeRates.Count();
            _exchangeRates.Add(exchangeRate);

            SaveDataToFile();
        }

        private static string LoadXmlFile()
        {
            using (var sr = new StreamReader(Path))
            {
                return sr.ReadToEnd();
            }
        }
    }
}