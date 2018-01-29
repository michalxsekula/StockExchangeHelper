using System.Collections.Generic;
using System.Xml.Serialization;

namespace StockExchangeHelper.Models
{
    public class Rates
    {
        [XmlElement("Rate")] public List<Rate> RateList { get; set; }
    }
}