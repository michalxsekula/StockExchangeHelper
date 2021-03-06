﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.ExtensionMethods
{
    public static class XmlReportsCreator
    {
        public static string Path;
        public const string FileName = @"xmlReport.xml";
        private static List<ExchangeRate> _exchangeRates;
        
        public static void SaveToXmlReport(this ExchangeRate exchangeRate, string directoryPath)
        {
            if (directoryPath.Last() != '\\')
                directoryPath = string.Concat(directoryPath,'\\');
            
            Path = $"{directoryPath}{FileName}";
            if (!File.Exists(Path))
            {
                _exchangeRates = new List<ExchangeRate> {exchangeRate};
            }
            else
            {
                AppendInformationToExistingReport(exchangeRate);
            }

            SaveDataToFile();
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