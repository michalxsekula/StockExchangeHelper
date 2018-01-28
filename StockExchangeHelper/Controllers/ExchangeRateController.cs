using System.Web.Mvc;
using StockExchangeHelper.Interfaces;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.Controllers
{
    public class ExchangeRateController : Controller
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public ExchangeRateController()
        {
            _currencyExchangeService = new NbpOverlay();
        }

        public ActionResult Index()
        {
            var exchangeRate = _currencyExchangeService.GetExchangeRate();

            return View(exchangeRate);
        }
    }
}