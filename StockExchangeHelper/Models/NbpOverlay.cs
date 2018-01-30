using System;
using System.IO;
using System.Net;
using AutoMapper;
using StockExchangeHelper.ExtensionMethods;
using StockExchangeHelper.Interfaces;

namespace StockExchangeHelper.Models
{
    public class NbpOverlay : ICurrencyExchangeService
    {
        private const string BasicNbpUri = "http://api.nbp.pl/api/exchangerates/rates/a/";
        private const string DemandedOutputFormat = "application/xml";
        private const string DateFormat = "yyyy-MM-dd";
        private IMapper _mapper;

        public NbpOverlay()
        {
            InitializeMapper();
        }

        public ExchangeRate GetExchangeRate(DateTime startDate, DateTime endDate, string code)
        {
            var nbpUri = $"{BasicNbpUri}{code}/{startDate.ToString(DateFormat)}/{endDate.ToString(DateFormat)}";
            var exchangeRateNbp = GetNativeExchangeRate(nbpUri);

            return _mapper.Map<ExchangeRateNbp, ExchangeRate>(exchangeRateNbp);
        }

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

        private void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ExchangeRateNbp, ExchangeRate>(); });
            _mapper = config.CreateMapper();
        }
    }
}