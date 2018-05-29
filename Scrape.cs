using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using FinanceScraper3.Controllers;
using FinanceScraper3.Models;
using System.Globalization;

namespace FinanceScraper3
{
    public class Scrape
    {
        public static Portfolio ScrapeData()
        {
            var snapshot = new Portfolio();

            // enables invisible scrape
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");

            // create new driver class
            var driver = new ChromeDriver("/Users/matthewrizzini/Desktop/Visual Studio Projects/FinanceScraper3/bin/Debug/netcoreapp2.0", option);            

            driver.Navigate().GoToUrl("https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");


            // navigating to username input box and clicking to sign in
            var userNameField = driver.FindElement(By.XPath("//*[@id='login-username']"));
            var loginUserButton = driver.FindElement(By.XPath("//*[@id='login-signin']"));            
            userNameField.SendKeys("testscraper");
            loginUserButton.Click();

            // waiting 5 seconds for page to load then to go onto enter password. need to throw an exception here
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 

            // sending password to input box and signing in 
            var userPasswordField = driver.FindElement(By.XPath("//*[@id='login-passwd']"));
            var loginPasswordButton = driver.FindElement(By.XPath("//*[@id='login-signin']")); 
            userPasswordField.SendKeys("scrapePass");
            loginPasswordButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15); 


            var portfolioButton = driver.FindElement(By.XPath("//*[@id='Nav-0-DesktopNav']/div/div[3]/div/div[1]/ul/li[2]"));

            portfolioButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);  

            var popups = driver.FindElements(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]"));

            // var popupButton = driver.FindElement(By.XPath("//*[@id='fin-tradeit']/div[2]/div/div/div[2]/button[2]"));

            if (popups.Count > 0)
            {
                System.Console.WriteLine("popup is {0}", popups[0]);
                popups[0].Click();
            }
            else
            {
                System.Console.WriteLine("Popup not found");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);  
            
            var myScraperButton = driver.FindElement(By.XPath("//*[@id='main']/section/section/div[2]/table/tbody/tr[2]/td[1]/a"));

            myScraperButton.Click();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);  
            
            var stockList = driver.FindElements(By.XPath("//*[@id='main']/section/section[2]/div[2]/table/tbody/tr"));

            System.Console.WriteLine("stocklist count is {0}", stockList.Count);

            foreach (var stock in stockList)
            {
                System.Console.WriteLine("Stock is " + stock.FindElement(By.ClassName("_1_2Qy")).Text);
            }

            var totalValue = driver.FindElement(By.XPath("//*[@id='main']/section/header/div/div[1]/div/div[2]/p[1]")).Text;
            
            var dayGain = driver.FindElement(By.XPath("//*[@id='main']/section/header/div/div[1]/div/div[2]/p[2]/span")).Text.Split(" ");
            
            var totalGain = driver.FindElement(By.XPath("//*[@id='main']/section/header/div/div[1]/div/div[2]/p[3]/span")).Text.Split(" ");

            // System.Console.WriteLine("totalValue is {0} and type is {1}", totalValue, totalValue.GetType());
            // System.Console.WriteLine("dayGain [0] is {0} and type is {1}", dayGain[0], dayGain[0].GetType());
            // System.Console.WriteLine("dayGain [1] is {0} and type is {1}", dayGain[1], dayGain[1].GetType());
            // System.Console.WriteLine("totalGain [0] is {0} and type is {1}", totalGain[0], totalGain[0].GetType());
            // System.Console.WriteLine("totalGain [1] is {0} and type is {1}", totalGain[1], totalGain[1].GetType());
            
            snapshot.Date = DateTime.Now;
            
            snapshot.TotalValue = Double.Parse(totalValue, NumberStyles.Currency);
            
            snapshot.DayGain = Double.Parse(dayGain[0]);
            
            snapshot.DayGainPercent = Double.Parse(dayGain[1].TrimStart(new char[] {'(', '+', '-', ' ' }).TrimEnd( new char[] { '%', ' ', ')' } ) ) / 100;
            // snapshot.DayGainPercent = Double.Parse(dayGain[1].Replace("%", "").Replace("(", "").Replace(")", "").Replace("+", "")) / 100;
            
                        
            snapshot.TotalGain = Double.Parse(totalGain[0]);
            
            snapshot.TotalGainPercent = Double.Parse(totalGain[1].TrimStart(new char[] {'(', ' ', '+', '-' }).TrimEnd( new char[] { '%', ' ', ')' } ) ) / 100;
            // snapshot.TotalGainPercent = Double.Parse(totalGain[1].Replace("(", "").Replace(")", "").Replace("%", "")) / 100;
            
            System.Console.WriteLine("totalValue is {0} and type is {1}", snapshot.TotalValue, snapshot.TotalValue.GetType());
            System.Console.WriteLine("dayGain is {0} and type is {1}", snapshot.DayGain, snapshot.DayGain.GetType());
            System.Console.WriteLine("dayGain percent is {0} and type is {1}", snapshot.DayGainPercent, snapshot.DayGainPercent.GetType());
            System.Console.WriteLine("totalGain is {0} and type is {1}", snapshot.TotalGain, snapshot.TotalGain.GetType());
            System.Console.WriteLine("totalGain percent is {0} and type is {1}", snapshot.TotalGainPercent, snapshot.TotalGainPercent.GetType());

            return snapshot;


        // public DateTime Date { get; set; }
        // public double TotalValue { get; set; }
        // public double DayGain { get; set; }
        // public double DayGainPercent { get; set; }
        // public double TotalGain { get; set; }
        // public double TotalGainPercent { get; set; }
        // public virtual List<Stock> Stocks { get; set; }

        }
 
 
    }
}