using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using FinanceScraper3.Models;
using FinanceScraper3.Data;


namespace FinanceScraper3.Controllers
{
    public class HomeController : Controller
    {

        // private readonly ApplicationDbContext ctx;

        // public HomeController(ApplicationDbContext ctx)
        // {
        //     this.ctx = ctx;
        // }


        
        //  = new ApplicationDbContext();
        
        public IActionResult Index()
        {
            return View();
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
