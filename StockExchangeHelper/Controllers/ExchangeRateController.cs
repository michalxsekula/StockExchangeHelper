﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using NLog;
using StockExchangeHelper.ExtensionMethods;
using StockExchangeHelper.Interfaces;
using StockExchangeHelper.Models;
using StockExchangeHelper.ViewModels;

namespace StockExchangeHelper.Controllers
{
    public class ExchangeRateController : Controller
    {
        private static Logger _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public ExchangeRateController(ICurrencyExchangeService currencyExchangeService)
        {
            _currencyExchangeService = currencyExchangeService;
            _context = new ApplicationDbContext();
            _logger = LogManager.GetCurrentClassLogger();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult GetHistoryResults()
        {
            var rates = new List<ExchangeRate>();
            if (_context.ExchangeRates != null)
            {
                 rates = _context.ExchangeRates.ToList();
            }
            
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
                _logger.Warn("Trial of sending incorrect request.");
                return View("ExchangeRateForm", viewModel);
            }

            try
            {
                viewModel.ExchangeRate = _currencyExchangeService.GetExchangeRate(
                    viewModel.ExchangeRate.StartDate,
                    viewModel.ExchangeRate.EndDate,
                    viewModel.ExchangeRate.Code);

                viewModel.ExchangeRate.SaveDate = DateTime.Now;
                ReportRecord(viewModel.ExchangeRate);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                viewModel.ExceptionMessage = e.Message;
                return View("ExchangeRateForm", viewModel);
            }

            return View("Result", viewModel.ExchangeRate);
        }

        private void ReportRecord(ExchangeRate exchangeRate)
        {
            const string reportsPath = @"C:\temp\";
            if (!Directory.Exists(reportsPath))
                Directory.CreateDirectory(reportsPath);

            exchangeRate.SaveToXmlReport(reportsPath);
            _context.ExchangeRates.Add(exchangeRate);
            _context.SaveChanges();
            _logger.Info($"Record was correctly saved. {exchangeRate}");
        }
    }
}