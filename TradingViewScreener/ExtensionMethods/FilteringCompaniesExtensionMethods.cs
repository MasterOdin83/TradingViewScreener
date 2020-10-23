using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingViewScreener.Models;

namespace TradingViewScreener.ExtensionMethods
{
    public static class FilteringCompaniesExtensionMethods
    {
        public static IEnumerable<string> AddRange(this List<string> Lista, IEnumerable<CompanyModel> Companies)
        {
            Lista.AddRange(Companies.Select(Company => Company.Ticker));
            return Lista;
        }
        public static IEnumerable<CompanyModel> GetCompaniesMACDBUY(this IEnumerable<CompanyModel> Companies)
        {
            return Companies.Where(Company => Company.SenalMACD < Company.NivelMACD);
        }
        public static IEnumerable<CompanyModel> GetCompaniesMACDSell(this IEnumerable<CompanyModel> Companies)
        {
            return Companies.Where(Company => Company.SenalMACD >= Company.NivelMACD);
        }
        public static IEnumerable<CompanyModel> GetBetweenRSI(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.RSI < CompanyModel.MaxRSI)
                .Where(Company => Company.RSI > CompanyModel.MinRSI);
        }
        public static IEnumerable<CompanyModel> GetBelowMaximumRSI(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.RSI < CompanyModel.MaxRSI);
        }
        public static IEnumerable<CompanyModel> GetBelowMinimumRSI(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.RSI < CompanyModel.MinRSI);
        }
        public static IEnumerable<CompanyModel> GetBelowMediumRSI(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.RSI < CompanyModel.MedRSI);
        }
        public static IEnumerable<CompanyModel> GetVolumeMedium(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.Volumen >= CompanyModel.MidVolumen);
        }
        public static IEnumerable<CompanyModel> GetVolumeMinimum(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.Volumen >= CompanyModel.MinVolumen);
        }
        public static IEnumerable<CompanyModel> GetCompaniesBelowPrice(this IEnumerable<CompanyModel> Companies, double Price)
        {
            return Companies
                .Where(Company => Company.UltimoPrecio < Price);
        }
        public static IEnumerable<CompanyModel> GetGrowingCompanies(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.SMA20 >= 0 && Company.SMA50 > 0 && Company.SMA200 > 0)
                .Where(Company => Company.SMA20 > Company.SMA50);
        }
        public static IEnumerable<CompanyModel> GetContractingCompanies(this IEnumerable<CompanyModel> Companies)
        {
            return Companies
                .Where(Company => Company.SMA20 >= 0 && Company.SMA50 > 0 && Company.SMA200 > 0)
                .Where(Company => Company.SMA20 <= Company.SMA50);
        }
        public static IEnumerable<CompanyModel> RemoveCompaniesFromList(this IEnumerable<CompanyModel> Companies, IEnumerable<CompanyModel> RemoveCompanies)
        {
            return Companies.Where(Company => !RemoveCompanies.Contains(Company)).ToList();
        }
        public static IEnumerable<CompanyModel> RemoveCompaniesFromList(this IEnumerable<CompanyModel> Companies, List<string> RemoveCompanies)
        {
            return Companies.Where(Company => !RemoveCompanies.Contains(Company.Ticker)).ToList();
        }

    }
}
