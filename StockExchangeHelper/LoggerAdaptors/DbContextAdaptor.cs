using BusinessLogicLayer.LoggerInterfaces;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.LoggerAdaptors
{
    public class DbContextAdaptor : IRecordLogger
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void LogRecord<T>(T record)
        {
            _context.ExchangeRates.Add(record as ExchangeRate);
            _context.SaveChanges();
        }
    }
}