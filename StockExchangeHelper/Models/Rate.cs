using System;
using System.Xml.Serialization;

namespace StockExchangeHelper.Models
{
    public class Rate
    {
        [XmlElement("No")] public string No { get; set; }

        [XmlElement("EffectiveDate")] public DateTime EffectiveDate { get; set; }

        [XmlElement("Mid")] public double Mid { get; set; }
    }
}