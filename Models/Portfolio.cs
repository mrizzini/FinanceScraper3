using System;
using System.Collections.Generic;

 

namespace FinanceScraper3.Models
{
    public class Portfolio
    {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double TotalValue { get; set; }

        public double DayGain { get; set; }

        public double DayGainPercent { get; set; }

        public double TotalGain { get; set; }

        public double TotalGainPercent { get; set; }

        public string UserId { get; set; }
        
        public virtual List<Stock> Stocks { get; set; }
    }
}

