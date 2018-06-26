using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FinanceScraper3.Models
{
    public class StockMarket
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        public double SPCurrentPrice { get; set; }

        [DataType(DataType.Currency)]
        public double SPPriceChange { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]        
        public double SPPercentChange { get; set; }

        [DataType(DataType.Currency)]
        public double DOWCurrentPrice { get; set; }

        [DataType(DataType.Currency)]
        public double DOWPriceChange { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]        
        public double DOWPercentChange { get; set; }

        [DataType(DataType.Currency)]
        public double NASDAQCurrentPrice { get; set; }

        [DataType(DataType.Currency)]
        public double NASDAQPriceChange { get; set; }

        [DisplayFormat(DataFormatString = "{0:P2}")]        
        public double NASDAQPercentChange { get; set; }        
    }
}

