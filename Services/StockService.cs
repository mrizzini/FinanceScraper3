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

        public async Task<Stock[]> GetStocksAsync(int id, string sortOrder)
        {
            var snapId = id;

            IQueryable<Stock> stock = from s in _context.Stocks
                                             select s;

            switch (sortOrder)
            {
                case "StockSymbol":
                    stock = stock.OrderBy(s => s.StockSymbol);
                    break;
                case "stocksymbol_desc":
                    stock = stock.OrderByDescending(s => s.StockSymbol);
                    break;
                case "CurrentPrice":
                    stock = stock.OrderBy(s => s.CurrentPrice);
                    break;
                case "currentprice_desc":
                    stock = stock.OrderByDescending(s => s.CurrentPrice);
                    break;
                case "ChangeByDollar":
                    stock = stock.OrderBy(s => s.ChangeByDollar);
                    break;
                case "changebydollar_desc":
                    stock = stock.OrderByDescending(s => s.ChangeByDollar);
                    break;
                case "ChangeByPercent":
                    stock = stock.OrderBy(s => s.ChangeByPercent);
                    break;
                case "changebypercent_desc":
                    stock = stock.OrderByDescending(s => s.ChangeByPercent);
                    break;
                case "Shares":
                    stock = stock.OrderBy(s => s.Shares);
                    break;
                case "shares_desc":
                    stock = stock.OrderByDescending(s => s.Shares);
                    break;
                case "CostBasis":
                    stock = stock.OrderBy(s => s.CostBasis);
                    break;
                case "costbasis_desc":
                    stock = stock.OrderByDescending(s => s.CostBasis);
                    break;
                case "MarketValue":
                    stock = stock.OrderBy(s => s.MarketValue);
                    break;
                case "marketvalue_desc":
                    stock = stock.OrderByDescending(s => s.MarketValue);
                    break;
                case "DayGainByDollar":
                    stock = stock.OrderBy(s => s.DayGainByDollar);
                    break;
                case "daygainbydollar_desc":
                    stock = stock.OrderByDescending(s => s.DayGainByDollar);
                    break;
                case "DayGainByPercent":
                    stock = stock.OrderBy(s => s.DayGainByPercent);
                    break;
                case "daygainbypercent_desc":
                    stock = stock.OrderByDescending(s => s.DayGainByPercent);
                    break;
                case "TotalGainByDollar":
                    stock = stock.OrderBy(s => s.TotalGainByDollar);
                    break;
                case "totalgainbydollar_desc":
                    stock = stock.OrderByDescending(s => s.TotalGainByDollar);
                    break;
                case "TotalGainByPercent":
                    stock = stock.OrderBy(s => s.TotalGainByPercent);
                    break;
                case "totalgainbypercent_desc":
                    stock = stock.OrderByDescending(s => s.TotalGainByPercent);
                    break;
                case "Lots":
                    stock = stock.OrderBy(s => s.Lots);
                    break;
                case "lots_desc":
                    stock = stock.OrderByDescending(s => s.Lots);
                    break;
                case "Notes":
                    stock = stock.OrderBy(s => s.Notes);
                    break;
                case "notes_desc":
                    stock = stock.OrderByDescending(s => s.Notes);
                    break;

                    

                default:
                    stock = stock.OrderBy(s => s.StockSymbol);
                    break;
            }



    
            return await stock
            .Where(x=> x.Portfolio.Id == snapId)
            .AsNoTracking()
            .ToArrayAsync();


        }
    }
}

