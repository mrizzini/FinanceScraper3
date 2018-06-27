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
        
        public SnapshotsController(IPortfolioService portfolioService, UserManager<ApplicationUser> userManager)
        {
            _portfolioService = portfolioService;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index(string sortOrder)
        {

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