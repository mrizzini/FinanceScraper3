using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FinanceScraper3.Models
{
    public class Portfolio
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public double TotalValue { get; set; }

        [DataType(DataType.Currency)]
        public double DayGain { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double DayGainPercent { get; set; }

        [DataType(DataType.Currency)]
        public double TotalGain { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double TotalGainPercent { get; set; }

        public string UserId { get; set; }
        
        public virtual List<Stock> Stocks { get; set; }
    }
}

