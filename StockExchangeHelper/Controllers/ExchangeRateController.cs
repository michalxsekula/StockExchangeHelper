using System;
using System.Web.Mvc;
using StockExchangeHelper.Interfaces;
using StockExchangeHelper.Models;
using StockExchangeHelper.ViewModels;

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
            var exchangeRateViewModel = new ExchangeRateViewModel
            {
                ExchangeRate = new ExchangeRate()
            };
            return View("ExchangeRateForm", exchangeRateViewModel);
        }

        public ActionResult SendRequest(ExchangeRateViewModel viewModel)
        {
            viewModel.ExceptionMessage = null;
            if (!ModelState.IsValid)
                return View("ExchangeRateForm", viewModel);
            try
            {
                viewModel.ExchangeRate = _currencyExchangeService.GetExchangeRate(
                    viewModel.ExchangeRate.StartDate,
                    viewModel.ExchangeRate.EndDate,
                    viewModel.ExchangeRate.Code);
            }
            catch (Exception e)
            {
                viewModel.ExceptionMessage = e.Message;
                return View("ExchangeRateForm", viewModel);
            }

            return View("Index", viewModel.ExchangeRate);
        }
    }
}