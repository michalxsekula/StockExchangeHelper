using System;

namespace StockExchangeHelper.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public double AverageRate { get; set; }
        public double StandardDeviation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}