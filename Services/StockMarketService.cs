using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinanceScraper3.Data;
using FinanceScraper3.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace FinanceScraper3.Services
{
    public class StockMarketService : IStockMarketService
    {
        public async Task<StockMarket> GetStockMarket()
        {

            var url = @"https://finance.yahoo.com/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var spCurrentPrice = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[1]/h3/span");
            var spPercentChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[1]/h3/div/span[2]/span");
            var spPriceChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[1]/h3/div/span[1]");
            var dowCurrentPrice = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[2]/h3/span");
            var dowPercentChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[2]/h3/div/span[2]/span");
            var dowPriceChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[2]/h3/div/span[1]");
            var nasdaqCurrentPrice = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[3]/h3/span");
            var nasdaqPercentChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[3]/h3/div/span[2]/span");
            var nasdaqPriceChange = doc.DocumentNode.SelectSingleNode("//*[@id='market-summary']/div/div[1]/div[1]/ul/li[3]/h3/div/span[1]");

            var StockMarketInfo = new StockMarket
            {
                Date = DateTime.Now,

                SPCurrentPrice = double.Parse(spCurrentPrice.InnerHtml, NumberStyles.Currency),

                SPPercentChange = double.Parse(spPercentChange.InnerHtml.Trim( new char[] { '%', ' ', '(', ')' } ) ) / 100,

                SPPriceChange = double.Parse(spPriceChange.InnerHtml, NumberStyles.Currency),

                DOWCurrentPrice = double.Parse(dowCurrentPrice.InnerHtml, NumberStyles.Currency),

                DOWPercentChange = double.Parse(dowPercentChange.InnerHtml.Trim( new char[] { '%', ' ', '(', ')' } ) ) / 100,
                
                DOWPriceChange = double.Parse(dowPriceChange.InnerHtml),
                
                NASDAQCurrentPrice = double.Parse(nasdaqCurrentPrice.InnerHtml),
                
                NASDAQPercentChange = double.Parse(nasdaqPercentChange.InnerHtml.Trim( new char[] { '%', ' ', '(', ')' } ) ) / 100,
                
                NASDAQPriceChange = double.Parse(nasdaqPriceChange.InnerHtml)
            };

            return await Task.FromResult(StockMarketInfo);

        }
    }
}