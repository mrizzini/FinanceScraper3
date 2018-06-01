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
    public class SnapshotsController : Controller
    {

        // Declares private var to hold a reference to the IPortfolio service
        // Lets us use the service rom the Index method later
        private readonly IPortfolioService _portfolioService;
        
        private ApplicationDbContext _ctx; // = new ApplicationDbContext();

        // Constructor. By adding IPortfolioSerivce we need to provide an object that matches the interface when we create this controller
        // Interfaces help decouple the logic of the app
        public SnapshotsController(ApplicationDbContext ctx, IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
            _ctx = ctx;
        }


        public async Task<IActionResult> Index()
        {
            // returns a Task<Portfolio[]>. We may not have the result right away so we use the await keyword so the code waits until the result is ready before continuing
            // await lets the code pause on an async operation and the pick up where it left off when the database request finishes, and the rest of our app isnt blocked
            // Now we must update the Index method signature to return Task<IActionResult> and mark as async
            // Now our Snapshots Controller depends on the IPortfolio Service interface
            // Now we must declare which concrete class to use for each interface, in Startup.cs
            var portfolioSnapshots = await _portfolioService.GetPortfolioSnapshotsAsync();

            var model = new PortfolioViewModel()
            {
                PortfolioSnapshots = portfolioSnapshots
            };


            return View(model);
        }

        public IActionResult NewSnapshot()
        {

            var snapshot = Scrape.ScrapeData();

            _ctx.Portfolios.Add(snapshot);
            _ctx.SaveChanges();

            return Content("test");

        }
        

    }
}
