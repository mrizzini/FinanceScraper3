using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FinanceScraper3.Models;
using FinanceScraper3.Data;
using FinanceScraper3.Services;

namespace FinanceScraper3.Controllers
{
    public class HomeController : Controller
    {

        private readonly IStockMarketService _stockMarketService;

        public HomeController(IStockMarketService stockMarketService)
        {
            _stockMarketService = stockMarketService;
        }     

        public async Task<IActionResult> Index()
        {
            var stockMarketInfo = await _stockMarketService.GetStockMarket();

            var model = new StockMarketViewModel()
            {
                StockMarketInfo = stockMarketInfo
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "About Scrapecity";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "My contact info:";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
