using System.Collections.Generic;
using System.IO;
using System.Linq;
using BusinessLogicLayer.XmlMethods;

namespace BusinessLogicLayer.ExtensionMethods
{
    public class XmlReportsCreator<T>
    {
        public const string FileName = @"xmlReport.xml";
        public static string Path;
        private static List<T> _records;

        public void SaveToXmlReport(T record, string directoryPath)
        {
            if (directoryPath.Last() != '\\')
                directoryPath = string.Concat(directoryPath, '\\');

            Path = $"{directoryPath}{FileName}";
            if (!File.Exists(Path))
                _records = new List<T> {record};
            else
                AppendInformationToExistingReport(record);

            SaveDataToFile();
        }

        private static void SaveDataToFile()
        {
            File.Create(Path).Dispose();
            using (TextWriter tw = new StreamWriter(Path))
            {
                tw.Write(_records.SerializeObjectToXml());
                tw.Close();
            }
        }

        private static void AppendInformationToExistingReport(T record)
        {
            var xmlReport = LoadXmlFile();

            _records = xmlReport.ParseXmlToObject<List<T>>();
            _records.Add(record);
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