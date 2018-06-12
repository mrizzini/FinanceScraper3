using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceScraper3.Data;
using FinanceScraper3.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceScraper3.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock[]> GetStocksAsync(int id)
        {
            var snapId = id;
    
            return await _context.Stocks
            .Where(x=> x.Portfolio.Id == snapId)
            .ToArrayAsync();
        }
    }
}

