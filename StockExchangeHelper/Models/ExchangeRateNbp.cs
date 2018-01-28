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

    public class Rates
    {
        [XmlElement("Rate")] public List<Rate> RateList { get; set; }
    }

    public class Rate
    {
        [XmlElement("No")] public string No { get; set; }

        [XmlElement("EffectiveDate")] public DateTime EffectiveDate { get; set; }

        [XmlElement("Mid")] public double Mid { get; set; }
    }
}