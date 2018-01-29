using System;
using System.IO;
using System.Linq;
using System.Net;
using StockExchangeHelper.ExtensionMethods;
using StockExchangeHelper.Interfaces;

namespace StockExchangeHelper.Models
{
    public class NbpOverlay : ICurrencyExchangeService
    {
        private const string BasicNbpUri = "http://api.nbp.pl/api/exchangerates/rates/a/";
        private const string DemandedOutputFormat = "application/xml";
        private const string DateFormat = "yyyy-MM-dd";


        private ExchangeRateNbp GetNativeExchangeRate(string nbpUri)
        {
            var request = WebRequest.Create(nbpUri);
            request.ContentType = DemandedOutputFormat;
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer.ParseXmlToObject<ExchangeRateNbp>();
        }

        public ExchangeRate GetExchangeRate(DateTime startDate, DateTime endDate, string code)
        {
            var nbpUri = $"{BasicNbpUri}{code}/{startDate.ToString(DateFormat)}/{endDate.ToString(DateFormat)}";
            var exchangeRateNbp = GetNativeExchangeRate(nbpUri);
            var midRateValues = exchangeRateNbp.Rates.RateList.Select(x => x.Mid).ToList();

            //todo add automapper
            return new ExchangeRate
            {
                Code = exchangeRateNbp.Code,
                Currency = exchangeRateNbp.Currency,
                AverageRate = midRateValues.Average(),
                StandardDeviation = midRateValues.CalculateStandardDeviation(),
                StartDate = exchangeRateNbp.Rates.RateList.First().EffectiveDate,
                EndDate = exchangeRateNbp.Rates.RateList.Last().EffectiveDate
            };
        }
    }
}