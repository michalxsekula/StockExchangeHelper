using System;
using System.Linq;
using System.Xml.Serialization;
using StockExchangeHelper.ExtensionMethods;

namespace StockExchangeHelper.Models
{
    [XmlRoot("ExchangeRatesSeries")]
    public class ExchangeRateNbp
    {
        [XmlElement("Table")] public string Table { get; set; }

        [XmlElement("Currency")] public string Currency { get; set; }

        [XmlElement("Code")] public string Code { get; set; }

        [XmlElement("Rates")] public Rates Rates { get; set; }

        public DateTime StartDate => Rates.RateList.First().EffectiveDate;

        public DateTime EndDate => Rates.RateList.Last().EffectiveDate;

        public double AverageRate
        {
            get { return Rates.RateList.Select(x => x.Mid).ToList().Average(); }
        }

        public double StandardDeviation
        {
            get { return Rates.RateList.Select(x => x.Mid).ToList().CalculateStandardDeviation(); }
        }
    }
}