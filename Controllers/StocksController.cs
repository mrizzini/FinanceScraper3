using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FinanceScraper3.Models;
using FinanceScraper3.Data;
using FinanceScraper3.Controllers;
using Microsoft.AspNetCore.Authorization;
using FinanceScraper3.Services;

namespace FinanceScraper3.Controllers
{
    // /snapshots
    [Authorize]
    public class StocksController : Controller
    {

        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }
 
        public async Task<IActionResult> Index(int id)
        {
            // if (id == null)
            // {
            //     return BadRequest(new { error = "No Scrape ID found" });
            // }

            // Movie movie = _stockService.Stocks.Find(id);
            // if (movie == null)
            // {
            //     return HttpNotFound();
            // }
            // return View(movie);

            System.Console.WriteLine("ID IS {0}", id);

            var stocks = await _stockService.GetStocksAsync(id);

            // stocks = stocks.Find(id)

            var model = new StockViewModel()
            {
                Stocks = stocks,
                PortfolioId = id
            };

            return View(model);

        }
 
    }
}