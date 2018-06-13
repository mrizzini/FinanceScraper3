using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceScraper3.Models;

namespace FinanceScraper3.Services
{
    public interface IStockService
    {
        Task<Stock[]> GetStocksAsync(int id, string sortOrder);
        // a task that contains an array of stocks
    }
}




