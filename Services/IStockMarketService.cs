using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceScraper3.Models;

namespace FinanceScraper3.Services
{
    public interface IStockMarketService
    {
        Task<StockMarket> GetStockMarket();
    }
}
