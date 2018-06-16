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
using Microsoft.AspNetCore.Identity;

namespace FinanceScraper3.Controllers
{
    // /snapshots
    [Authorize]
    public class SnapshotsController : Controller
    {

        // Declares private var to hold a reference to the IPortfolio service
        // Lets us use the service rom the Index method later
        private readonly IPortfolioService _portfolioService;
        // inject usermanager. we can use this to get the current user in the index action
        private readonly UserManager<ApplicationUser> _userManager;
        
        // private ApplicationDbContext _ctx; // = new ApplicationDbContext();

        // Constructor. By adding IPortfolioSerivce we need to provide an object that matches the interface when we create this controller
        // Interfaces help decouple the logic of the app
        public SnapshotsController(IPortfolioService portfolioService, UserManager<ApplicationUser> userManager)
        {
            _portfolioService = portfolioService;
            _userManager = userManager;
        }
        


        // Index = /Snapshots
        public async Task<IActionResult> Index(string sortOrder)
        {
            // returns a Task<Portfolio[]>. We may not have the result right away so we use the await keyword so the code waits until the result is ready before continuing
            // await lets the code pause on an async operation and the pick up where it left off when the database request finishes, and the rest of our app isnt blocked
            // Now we must update the Index method signature to return Task<IActionResult> and mark as async
            // Now our Snapshots Controller depends on the IPortfolio Service interface
            // Now we must declare which concrete class to use for each interface, in Startup.cs


            // looks up current user from User property. If it is somehow null, we use Challenge() to force user to login again if info is missing
            // We now must update IPortfolioService interface since we are passing Application parameter to GetPortfolioSnapshotsAsync
            // Then change signature of method in PortfolioService
            // Then update database to add UserId
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["TotalValueSortParm"] = sortOrder == "TotalValue" ? "totalvalue_desc" : "TotalValue";
            ViewData["DayGainSortParm"] = sortOrder == "DayGain" ? "daygain_desc" : "DayGain";
            ViewData["DayGainPercentSortParm"] = sortOrder == "DayGainPercent" ? "daygainpercent_desc" : "DayGainPercent";
            ViewData["TotalGainSortParm"] = sortOrder == "TotalGain" ? "totalgain_desc" : "TotalGain";
            ViewData["TotalGainPercentSortParm"] = sortOrder == "TotalGainPercent" ? "totalgainpercent_desc" : "TotalGainPercent";

            var portfolioSnapshots = await _portfolioService.GetPortfolioSnapshotsAsync(currentUser, sortOrder);

            var model = new PortfolioViewModel()
            {
                PortfolioSnapshots = portfolioSnapshots
            };

            return View(model);
        }



        public async Task<IActionResult> TriggerSnapshot(Portfolio newPortfolio)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _portfolioService.TriggerSnapshotAsync(newPortfolio, currentUser);
            
            if (!successful)
            {
                return BadRequest(new { error = "Could not scrape data at this time" });
            }

            return RedirectToAction("Index");
        }

    }
}