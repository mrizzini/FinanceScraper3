using System.Collections.Generic;

namespace FinanceScraper3.Models
{
    public class StockViewModel
    {
        public List <Stock> Stocks { get; set; }
        public int PortfolioId { get; set; }
    }
}