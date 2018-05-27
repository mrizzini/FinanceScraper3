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

namespace FinanceScraper3.Controllers
{
    public class SnapshotsController : Controller
    {
        
        // /snapshots
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewSnapshot()
        {

            Scrape.ScrapeData();

            return Content("test");


        }
        

    }
}
