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
    // /snapshots
    [Authorize]
    public class SnapshotsController : Controller
    {
        
        private ApplicationDbContext ctx; // = new ApplicationDbContext();

        public SnapshotsController(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewSnapshot()
        {

            var snapshot = Scrape.ScrapeData();

            ctx.Portfolio.Add(snapshot);
            ctx.SaveChanges();

            return Content("test");

        }
        

    }
}
