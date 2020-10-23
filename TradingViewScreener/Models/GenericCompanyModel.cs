using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingViewScreener.ExtensionMethods;

namespace TradingViewScreener.Models
{
    public class GenericCompanyModel 
    {
        public string Ticker { get; set; }
        public string? CompanyNotes { get; set; }
        public double CurrentPrice { get; set; }
        public double ExpectedPrice { get; set; }
        public double ExpectedGains 
        {
            get
            {
                return (ExpectedPrice /CurrentPrice)-1;
            }
        }

        public GenericCompanyModel(string p)
        {
            string[] RowDetails = p.Split(",");
            Ticker = RowDetails[0].ToString();
            CompanyNotes = RowDetails[1].ToString();
            if (RowDetails.Length>2)
            {
                CurrentPrice = RowDetails[2].ToString().ToDouble();
                ExpectedPrice = RowDetails[3].ToString().ToDouble();
            }
        }
    }
}
