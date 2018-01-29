using System;
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

        public ActionResult GetRate()
        {
            var exchangeRate = new ExchangeRate();
            return View("ExchangeRateForm", exchangeRate);
        }

        public ActionResult SendRequest(ExchangeRate exchangeRate)
        {
            if (!ModelState.IsValid)
                return View("ExchangeRateForm", exchangeRate);
            try
            {
                exchangeRate = _currencyExchangeService.GetExchangeRate(
                    exchangeRate.StartDate,
                    exchangeRate.EndDate,
                    exchangeRate.Code);
            }
            catch (Exception e)
            {
                //todo redirect to 'GetRate' page and show error message
                return Content(e.Message);
            }

            return View("Index", exchangeRate);
        }
    }
}