

using System.ComponentModel.DataAnnotations;

namespace FinanceScraper3.Models
{
    public class Stock
    {
        
        public string StockSymbol { get; set; }
        
        [DataType(DataType.Currency)]
        public double CurrentPrice { get; set; }
        
        [DataType(DataType.Currency)]
        
        public double ChangeByDollar { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:P2}")]
        
        public double ChangeByPercent { get; set; }
        
        public double Shares { get; set; }
        
        [DataType(DataType.Currency)]       
        
        public double CostBasis { get; set; }
        
        [DataType(DataType.Currency)]
        
        public double MarketValue { get; set; }
        
        [DataType(DataType.Currency)]
        
        public double DayGainByDollar { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:P2}")]
        
        public double DayGainByPercent { get; set; } 
        
        [DataType(DataType.Currency)]
        
        public double TotalGainByDollar { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:P2}")]
        
        public double TotalGainByPercent { get; set; }        
        
        public double Lots { get; set; }
    
        public string Notes {get; set; }
        
        public int Id { get; set; }
        
        public virtual Portfolio Portfolio { get; set; }    
    }
}