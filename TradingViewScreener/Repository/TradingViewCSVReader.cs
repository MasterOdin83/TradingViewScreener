using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingViewScreener.Models;

namespace TradingViewScreener.Repository
{
    public class TradingViewCSVReader : IRepository<CompanyModel>
    {
        private List<CompanyModel> Companies = new List<CompanyModel>();

        public TradingViewCSVReader()
        {
            var CSVFilePath = @"C:\Users\he20135258\source\repos\TradingViewScreener\TradingViewScreener\TradingViewFiles";
            var CSVFileToUse = Directory.GetFiles(CSVFilePath).FirstOrDefault();
            if (CSVFileToUse != null)
            {
                Companies.AddRange(File.ReadAllLines(CSVFileToUse).Skip(1).Select(p => new CompanyModel(p)));
            }
        }

        public List<CompanyModel> GetCompanies()
        {
            return Companies;
        }

        
    }
}
