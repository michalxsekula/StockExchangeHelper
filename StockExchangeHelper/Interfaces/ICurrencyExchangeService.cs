using System;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.Interfaces
{
    public interface ICurrencyExchangeService
    {
        ExchangeRate GetExchangeRate(DateTime startDate, DateTime endDate, string code);
    }
}