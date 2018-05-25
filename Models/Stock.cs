

namespace FinanceScraper3.Models
{
    public class Stock
    {
        
        public string StockSymbol { get; set; }

        public double CurrentPrice { get; set; }

        public double ChangeByDollar { get; set; }

        public double ChangeByPercent { get; set; }

        public double Shares { get; set; }

        public double CostBasis { get; set; }

        public double MarketValue { get; set; }
        
        public double DayGainByDollar { get; set; }

        public double DayGainByPercent { get; set; }
        
        public double TotalGainByDollar { get; set; }

        public double TotalGainByPercent { get; set; }        

        public double Lots { get; set; }

        public string Notes {get; set; }

        public int Id { get; set; }
           
        public virtual Portfolio Portfolio { get; set; }    

    }
}