using System.IO;
using System.Linq;
using System.Net;
using StockExchangeHelper.ExtensionMethods;
using StockExchangeHelper.Interfaces;

namespace StockExchangeHelper.Models
{
    public class NbpOverlay : ICurrencyExchangeService
    {
        private const string NbpUri = "http://api.nbp.pl/api/exchangerates/rates/a/eur/2017-11-01/2017-11-24/";
        private const string DemandedOutputFormat = "application/xml";

        public ExchangeRate GetExchangeRate()
        {
            var exchangeRateNbp = GetNativeExchangeRate();
            var rateValues = exchangeRateNbp.Rates.RateList.Select(x => x.Mid).ToList();

            //todo add automapper
            return new ExchangeRate
            {
                Code = exchangeRateNbp.Code,
                Currency = exchangeRateNbp.Currency,
                AverageRate = rateValues.Average(),
                StandardDeviation = rateValues.CalculateStandardDeviation(),
                StartDate = exchangeRateNbp.Rates.RateList.First().EffectiveDate,
                EndDate = exchangeRateNbp.Rates.RateList.Last().EffectiveDate
            };
        }

        private ExchangeRateNbp GetNativeExchangeRate()
        {
            var request = WebRequest.Create(NbpUri);
            request.ContentType = DemandedOutputFormat;
            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer.ParseXmlToObject<ExchangeRateNbp>();
        }
    }
}