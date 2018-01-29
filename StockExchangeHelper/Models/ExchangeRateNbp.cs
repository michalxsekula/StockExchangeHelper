using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StockExchangeHelper.Models
{
    [XmlRoot("ExchangeRatesSeries")]
    public class ExchangeRateNbp
    {
        [XmlElement("Table")] public string Table { get; set; }

        [XmlElement("Currency")] public string Currency { get; set; }

        [XmlElement("Code")] public string Code { get; set; }

        [XmlElement("Rates")] public Rates Rates { get; set; }
    }
}