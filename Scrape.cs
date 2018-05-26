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


namespace FinanceScraper3
{
    public class Scrape
    {
 
        public static void ScrapeData()
        {
                       // create new driver class
             IWebDriver driver = new ChromeDriver("/Users/matthewrizzini/Desktop/Visual Studio Projects/FinanceScraper/bin/Debug/netcoreapp2.0/");

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
        }
 
 
    }
}