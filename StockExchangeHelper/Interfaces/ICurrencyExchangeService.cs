using System;
using StockExchangeHelper.Models;

namespace StockExchangeHelper.Interfaces
{
    internal interface ICurrencyExchangeService
    {
        ExchangeRate GetExchangeRate(DateTime startDate, DateTime endDate, string code);
    }
}