using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using StockExchangeHelper.Interfaces;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.Controllers.Api
{
    public class ExchangeRatesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public ExchangeRatesController()
        {
            _context = new ApplicationDbContext();
            _currencyExchangeService = new NbpOverlay();
        }

        [HttpGet]
        public IHttpActionResult GetExchangeRates()
        {
            return Ok(_context.ExchangeRates.ToList());
        }

        [HttpGet]
        public IHttpActionResult GetExchangeRate(int id)
        {
            var exchangeRate = _context.ExchangeRates.SingleOrDefault(x => x.Id == id);
            if (exchangeRate == null)
                return NotFound();

            return Ok(exchangeRate);
        }

        [HttpPost]
        public IHttpActionResult SendExchangeRate(ExchangeRate exchangeRate)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            exchangeRate = _currencyExchangeService.GetExchangeRate(
                exchangeRate.StartDate,
                exchangeRate.EndDate,
                exchangeRate.Code);

            exchangeRate.SaveDate = DateTime.Now;

            _context.ExchangeRates.Add(exchangeRate);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + exchangeRate.Id), exchangeRate);
        }

        [HttpDelete]
        public void DeleteExchangeRate(int id)
        {
            var exchangeRateInDb = _context.ExchangeRates.SingleOrDefault(x => x.Id == id);
            if (exchangeRateInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.ExchangeRates.Remove(exchangeRateInDb);
            _context.SaveChanges();
        }
    }
}