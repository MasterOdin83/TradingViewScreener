using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingViewScreener.Models;

namespace TradingViewScreener.Repository
{
    public class GenericCSVRepository : IRepository<GenericCompanyModel>
    {
        List<GenericCompanyModel> Companies = new List<GenericCompanyModel>();
        public GenericCSVRepository(string CSVFile, int SkipLines = 0)
        {
            var CSVFilePath = @$"C:\Users\he20135258\Documents\Confidential\{CSVFile}.txt";
            Companies.AddRange(File.ReadAllLines(CSVFilePath).Skip(SkipLines)
                .Where(Line => !string.IsNullOrEmpty(Line))
                .Select(CSVString => new GenericCompanyModel(CSVString)));
        }
        public List<GenericCompanyModel> GetCompanies()
        {
            return Companies;
        }
        public List<string> GetCompaniesTikers()
        {
            return Companies.Select(Company=> Company.Ticker).ToList();
        }
    }
}