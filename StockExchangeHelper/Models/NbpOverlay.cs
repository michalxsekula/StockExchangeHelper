using System;
using AutoMapper;
using BusinessLogicLayer.RequestProvider;
using BusinessLogicLayer.XmlMethods;
using StockExchangeHelper.Interfaces;

namespace StockExchangeHelper.Models
{
    public class NbpOverlay : ICurrencyExchangeService
    {
        private const string BasicNbpUri = "http://api.nbp.pl/api/exchangerates/rates/a/";
        private const string DemandedContentType = "application/xml";
        private const string DateFormat = "yyyy-MM-dd";
        private readonly HttpRequestProvider _httpRequestProvider;
        private IMapper _mapper;

        public NbpOverlay()
        {
            InitializeMapper();
            _httpRequestProvider = new HttpRequestProvider();
        }

        public ExchangeRate GetExchangeRate(DateTime startDate, DateTime endDate, string code)
        {
            var nbpUri = $"{BasicNbpUri}{code}/{startDate.ToString(DateFormat)}/{endDate.ToString(DateFormat)}";
            var exchangeRateNbp = GetNativeExchangeRate(nbpUri);

            return _mapper.Map<ExchangeRateNbp, ExchangeRate>(exchangeRateNbp);
        }

        private ExchangeRateNbp GetNativeExchangeRate(string nbpUri)
        {
            var responseFromServer = _httpRequestProvider.Get(nbpUri, DemandedContentType);

            return responseFromServer.ParseXmlToObject<ExchangeRateNbp>();
        }

        private void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ExchangeRateNbp, ExchangeRate>(); });
            _mapper = config.CreateMapper();
        }
    }
}