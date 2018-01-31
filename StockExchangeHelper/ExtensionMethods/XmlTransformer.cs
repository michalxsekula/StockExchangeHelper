using System.IO;
using System.Xml.Serialization;

namespace StockExchangeHelper.ExtensionMethods
{
    public static class XmlTransformer
    {
        public static T ParseXmlToObject<T>(this string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(xmlString))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static string SerializeObjectToXml(this object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}