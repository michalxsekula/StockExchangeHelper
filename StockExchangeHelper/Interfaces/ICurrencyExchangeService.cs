using StockExchangeHelper.Models;

namespace StockExchangeHelper.Interfaces
{
    internal interface ICurrencyExchangeService
    {
        ExchangeRate GetExchangeRate();
    }
}