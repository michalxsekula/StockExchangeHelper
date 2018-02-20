using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLogicLayer.LoggerInterfaces;
using StockExchangeHelper.Interfaces;
using StockExchangeHelper.Models;
using StockExchangeHelper.ViewModels;

namespace StockExchangeHelper.Controllers
{
    public class ExchangeRateController : Controller
    {
        private static ILoggerManager _loggerManager;
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public ExchangeRateController(ICurrencyExchangeService currencyExchangeService, ILoggerManager loggerManager)
        {
            _currencyExchangeService = currencyExchangeService;
            _context = new ApplicationDbContext();
            _loggerManager = loggerManager;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult GetHistoryResults()
        {
            var rates = new List<ExchangeRate>();
            if (_context.ExchangeRates != null) rates = _context.ExchangeRates.ToList();

            return View(rates);
        }

        public ActionResult GetRate()
        {
            var exchangeRateViewModel = new ExchangeRateViewModel
            {
                ExchangeRate = new ExchangeRate()
            };
            return View("ExchangeRateForm", exchangeRateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendRequest(ExchangeRateViewModel viewModel)
        {
            viewModel.ExceptionMessage = null;
            if (!ModelState.IsValid)
            {
                _loggerManager.LogWarning("Trial of sending incorrect request.");
                return View("ExchangeRateForm", viewModel);
            }

            try
            {
                viewModel.ExchangeRate = _currencyExchangeService.GetExchangeRate(
                    viewModel.ExchangeRate.StartDate,
                    viewModel.ExchangeRate.EndDate,
                    viewModel.ExchangeRate.Code);

                viewModel.ExchangeRate.SaveDate = DateTime.Now;
                _loggerManager.LogRecord(viewModel.ExchangeRate);
            }
            catch (Exception e)
            {
                _loggerManager.LogError(e.Message);
                viewModel.ExceptionMessage = e.Message;
                return View("ExchangeRateForm", viewModel);
            }

            return View("Result", viewModel.ExchangeRate);
        }
    }
}