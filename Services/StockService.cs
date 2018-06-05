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

            // var test = _context.Stocks.Find(id);

            // var test = test.ToArrayAsync();

            // test.Find(id);

            // var test = id.ToString();
            var test = id;
            

            // return await _context.Stocks
            // .Where(x=> x.PortId == test).ToArrayAsync();

              return await _context.Stocks
            .Where(x=> x.Portfolio.Id == test).ToArrayAsync();

            // return await _context.Stocks.ToArrayAsync();

            // return await test;
            
        }
    }
}

