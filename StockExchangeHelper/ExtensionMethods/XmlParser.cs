using System.IO;
using System.Xml.Serialization;

namespace StockExchangeHelper.ExtensionMethods
{
    public static class XmlParser
    {
        public static T ParseXmlToObject<T>(this string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                return (T) serializer.Deserialize(reader);
            }
        }
    }
}