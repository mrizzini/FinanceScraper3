using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceScraper3.Data;
using FinanceScraper3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using FinanceScraper3.Controllers;
using System.Globalization;
using System.Threading;

namespace FinanceScraper3.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly ApplicationDbContext _context;
       
        public PortfolioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetPortfolioSnapshotsAsync(ApplicationUser user, string sortOrder)
        {

            IQueryable<Portfolio> snapshot = from s in _context.Portfolios
                                             select s;

            switch (sortOrder)
            {
                case "Date":
                    snapshot = snapshot.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    snapshot = snapshot.OrderByDescending(s => s.Date);
                    break;
                case "TotalValue":
                    snapshot = snapshot.OrderBy(s => s.TotalValue);
                    break;
                case "totalvalue_desc":
                    snapshot = snapshot.OrderByDescending(s => s.TotalValue);
                    break;
                case "DayGain":
                    snapshot = snapshot.OrderBy(s => s.DayGain);
                    break;
                case "daygain_desc":
                    snapshot = snapshot.OrderByDescending(s => s.DayGain);
                    break;
                case "DayGainPercent":
                    snapshot = snapshot.OrderBy(s => s.DayGainPercent);
                    break;
                case "daygainpercent_desc":
                    snapshot = snapshot.OrderByDescending(s => s.DayGainPercent);
                    break;
                case "TotalGain":
                    snapshot = snapshot.OrderBy(s => s.TotalGain);
                    break;
                case "totalgain_desc":
                    snapshot = snapshot.OrderByDescending(s => s.TotalGain);
                    break;
                case "TotalGainPercent":
                    snapshot = snapshot.OrderBy(s => s.TotalGainPercent);
                    break;
                case "totalgainpercent_desc":
                    snapshot = snapshot.OrderByDescending(s => s.TotalGainPercent);
                    break;
                default:
                    snapshot = snapshot.OrderBy(s => s.Date);
                    break;
            }

            return await snapshot
                        .Where(x=> x.UserId == user.Id)
                        .AsNoTracking().ToListAsync();
        }

        public async Task<bool> TriggerSnapshotAsync(Portfolio newSnapshot, ApplicationUser user)
        {           

            // create new driver class
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--headless");

            //Give the path of the geckodriver.exe    
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService("bin/Debug/netcoreapp2.0/");

            IWebDriver driver = new FirefoxDriver(service, options);

            driver.Navigate().GoToUrl("https://login.yahoo.com/?.src=finance&.intl=us&.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios&add=1");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            var userNameField = driver.FindElement(By.XPath("//*[@id='login-username']"));
            var loginUserButton = driver.FindElement(By.XPath("//*[@id='login-signin']"));            
            userNameField.SendKeys("testscraper"); 
            loginUserButton.Click();

            IWebElement passwordWait = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.XPath("//*[@id='login-passwd']"));
            }); 

            var userPasswordField = driver.FindElement(By.XPath("//*[@id='login-passwd']"));
            var loginPasswordButton = driver.FindElement(By.XPath("//*[@id='login-signin']")); 
            userPasswordField.SendKeys("Password1!");
            loginPasswordButton.Click();

            driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v2");
            
            try
            {
                IWebElement popup = wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]"));
                });

                driver.FindElement(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]")).Click();
                System.Console.WriteLine("Popup clicked");
            }
            catch(WebDriverException e)
            {
                System.Console.WriteLine("Popup not found {0}", e);
            }

            var totalValue = driver.FindElement(By.ClassName("_3wreg")).Text;
            var dayGain = driver.FindElement(By.ClassName("_2ETlv")).FindElement(By.TagName("span")).Text.Split(" ");
            var totalGain = driver.FindElement(By.ClassName("_2HvXW")).FindElement(By.TagName("span")).Text.Split(" ");

            newSnapshot.Date = DateTime.Now;
            newSnapshot.TotalValue = double.Parse(totalValue, NumberStyles.Currency);
            newSnapshot.TotalGain = double.Parse(totalGain[0]);     
            newSnapshot.TotalGainPercent = double.Parse(totalGain[1].Trim( new char[] { '%', ' ', '(', ')' } ) ) / 100;
            newSnapshot.DayGain = double.Parse(dayGain[0]);
            newSnapshot.DayGainPercent = double.Parse(dayGain[1].Trim( new char[] { '%', ' ','(', ')' } ) ) / 100;        

            var portfolioStockList = new List<Stock>();
            
            IWebElement tableWait = wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.ClassName("tJDbU"));
            });

            var stockListTable = driver.FindElement(By.ClassName("tJDbU"));
            var stockListTableRows = stockListTable.FindElements(By.ClassName("_14MJo"));
            var stockInfo = new List<string>();

            foreach (var row in stockListTableRows)
            {
                var stockListTableCells = row.FindElements(By.TagName("td"));
                if (stockListTableCells.Count > 0)
                {
                    foreach (var cell in stockListTableCells)
                    {
                        stockInfo.Add(cell.Text);
                    }

                    var stockSymbolAndPrice = stockInfo[0].ToString().Split("\n");
                    var changeByDollarAndPercent = stockInfo[1].ToString().Split("\n");
                    var dayGainByDollarAndPercent = stockInfo[5].ToString().Split("\n");
                    var totalGainByDollarAndPercent = stockInfo[6].ToString().Split("\n");
                    var lotSplit = stockInfo[7].Split(" ");
            
                    portfolioStockList.Add(new Stock()
                    {
                        StockSymbol = stockSymbolAndPrice[0].ToString(),          
                        CurrentPrice = double.Parse(stockSymbolAndPrice[1]),
                        ChangeByDollar = double.Parse(changeByDollarAndPercent[1]),
                        ChangeByPercent = (double.Parse(changeByDollarAndPercent[0].Trim( new char[] {'%' } )) / 100),
                        Shares = double.Parse(stockInfo[2]),  
                        CostBasis = double.Parse(stockInfo[3]),
                        MarketValue = double.Parse(stockInfo[4]),
                        DayGainByDollar = double.Parse(dayGainByDollarAndPercent[1]),
                        DayGainByPercent = (double.Parse(dayGainByDollarAndPercent[0].Trim( new char[] {'%' } )) / 100),
                        TotalGainByDollar = double.Parse(totalGainByDollarAndPercent[1]),
                        TotalGainByPercent = (double.Parse(totalGainByDollarAndPercent[0].Trim( new char[] {'%' } )) / 100),
                        Lots = double.Parse(lotSplit[0]),
                        Notes = stockInfo[8],
                    });
                }
                System.Console.WriteLine("Stock done");
                stockInfo.Clear();
            }

            System.Console.WriteLine("Driver quitting");
            driver.Quit();
            newSnapshot.Stocks = portfolioStockList;
            newSnapshot.UserId = user.Id;
            _context.Portfolios.Add(newSnapshot);

            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            // if operation was successful, we will return true, because saveResult should == how many objects were saved to DB. If it is less than 0, we know it failed and return false
        }
    }
}