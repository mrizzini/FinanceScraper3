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
 
        public async Task<IActionResult> Index(int id, string sortOrder, string searchString)
        {

            ViewData["StockSymbolSortParm"] = sortOrder == "StockSymbol" ? "stocksymbol_desc" : "StockSymbol";
            ViewData["CurrentPriceSortParm"] = sortOrder == "CurrentPrice" ? "currentprice_desc" : "CurrentPrice";
            ViewData["ChangeByDollarSortParm"] = sortOrder == "ChangeByDollar" ? "changebydollar_desc" : "ChangeByDollar";
            ViewData["ChangeByPercentSortParm"] = sortOrder == "ChangeByPercent" ? "changebypercent_desc" : "ChangeByPercent";
            ViewData["SharesSortParm"] = sortOrder == "Shares" ? "shares_desc" : "Shares";
            ViewData["CostBasisSortParm"] = sortOrder == "CostBasis" ? "costbasis_desc" : "CostBasis";
            ViewData["MarketValueSortParm"] = sortOrder == "MarketValue" ? "marketvalue_desc" : "MarketValue";
            ViewData["DayGainByDollarSortParm"] = sortOrder == "DayGainByDollar" ? "daygainbydollar_desc" : "DayGainByDollar";
            ViewData["DayGainByPercentSortParm"] = sortOrder == "DayGainByPercent" ? "daygainbypercent_desc" : "DayGainByPercent";
            ViewData["TotalGainByDollarSortParm"] = sortOrder == "TotalGainByDollar" ? "totalgainbydollar_desc" : "TotalGainByDollar";
            ViewData["TotalGainByPercentSortParm"] = sortOrder == "TotalGainByPercent" ? "totalgainbypercent_desc" : "TotalGainByPercent";
            ViewData["LotsSortParm"] = sortOrder == "Lots" ? "lots_desc" : "Lots";
            ViewData["NotesSortParm"] = sortOrder == "Notes" ? "notes_desc" : "Notes";

            ViewData["CurrentFilter"] = searchString;

            var stocks = await _stockService.GetStocksAsync(id, sortOrder, searchString);

            var model = new StockViewModel()
            {
                Stocks = stocks,
                PortfolioId = id
            };

            return View(model);

        }
 
    }
}