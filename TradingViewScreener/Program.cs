using System;
using System.Collections.Generic;
using System.Linq;
using TradingViewScreener.ExtensionMethods;
using TradingViewScreener.Models;
using TradingViewScreener.Repository;

namespace TradingViewScreener
{
    class Program
    {
        static void Main(string[] args)
        {
            var Repository = new TradingViewCSVReader();
            var NoGORepository = new GenericCSVRepository("NoGo");
            var BuyingRepository = new GenericCSVRepository("Comprar");
            var BuyedRepository = new GenericCSVRepository("YaComprado");
            //var ReviewedRepository = new GenericCSVRepository("YaRevisado");

            List<string> RemoveCompanies = new List<string>();
            RemoveCompanies.AddRange(NoGORepository.GetCompaniesTikers());
            RemoveCompanies.AddRange(BuyingRepository.GetCompaniesTikers());
            RemoveCompanies.AddRange(BuyedRepository.GetCompaniesTikers());

            var MACDSell_MM_VM = Repository.GetCompanies()
                .GetCompaniesMACDSell()
                .GetContractingCompanies()
                .GetVolumeMedium()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en bajada, MM de 20 Dias abajo de 50 dias Y Volumen Medio", MACDSell_MM_VM.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACDSell_MM_VM);

            var MACDSell_VM = Repository.GetCompanies()
                .GetCompaniesMACDSell()
                .GetVolumeMedium()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en bajada y Volumen Medio", MACDSell_VM.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACDSell_VM);

            var MACDSell = Repository.GetCompanies()
                .GetCompaniesMACDSell()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en bajada", MACDSell.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACDSell);

            var MACD_MinRSI_RSI_MM_VM = Repository.GetCompanies()
                .GetCompaniesMACDBUY()
                .GetBelowMinimumRSI()
                .GetGrowingCompanies()
                .GetVolumeMedium()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en subida, RSI Menor a la Media, MM de 20 Dias Arriba de 50 dias Y Volumen Medio", MACD_MinRSI_RSI_MM_VM.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACD_MinRSI_RSI_MM_VM);

            var MACD_MidRSI_MM_VMin = Repository.GetCompanies()
                .GetCompaniesMACDBUY()
                .GetBelowMediumRSI()
                .GetGrowingCompanies()
                .GetVolumeMinimum()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en subida, RSI Menor a la Media, MM de 20 Dias Arriba de 50 dias Y Volumen Bajo", MACD_MidRSI_MM_VMin.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACD_MidRSI_MM_VMin);

            var MACD_MAXRSI_MM = Repository.GetCompanies()
                .GetCompaniesMACDBUY()
                .GetBelowMaximumRSI()
                .GetGrowingCompanies()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en subida, RSI Menor al Maximo, Y Media Movil de 20 Dias Arriba de 50 dias", MACD_MAXRSI_MM.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACD_MAXRSI_MM);

            var MACD_MAXRSI = Repository.GetCompanies()
                .GetCompaniesMACDBUY()
                .GetBelowMaximumRSI()
                .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en subida y RSI Menor al Maximo", MACD_MAXRSI.OrderByDescending(p => p.UltimoPrecio).ToList());
            RemoveCompanies.AddRange(MACD_MAXRSI);

            var MACDCompanies = Repository.GetCompanies()
               .GetCompaniesMACDBUY()
               .RemoveCompaniesFromList(RemoveCompanies);
            PrintResult("Companias en MACD en subida", MACDCompanies.OrderByDescending(p => p.UltimoPrecio).ToList());

            PrintGenericCompany("Se Ven Interesantes para Comprar", BuyingRepository.GetCompanies().OrderByDescending(Company=>Company.ExpectedGains).ToList());
            PrintGenericCompany("Ya Compradas", BuyedRepository.GetCompanies());
            PrintGenericCompany("No Go Companies, tan feas", NoGORepository.GetCompanies());
            //PrintGenericCompany("Ya Revisadas y no se ven interesantes pero tampoco tan feas", ReviewedRepository.GetCompanies());
            Console.ReadLine();
        }

        private static void PrintGenericCompany(string InitialMessage, List<GenericCompanyModel> NoGoCompanies)
        {
            Console.WriteLine($"\n>>>>>{InitialMessage}<<<<<");
            foreach (var Company in NoGoCompanies)
            {
                if(Company.ExpectedGains > 0)
                {
                    Console.WriteLine($"Ticker {Company.Ticker}\t\t Razon: {Company.CompanyNotes}\t Ganancia: {Company.ExpectedGains:p}");
                }
                else
                {
                    Console.WriteLine($"Ticker {Company.Ticker}\t\t Razon: {Company.CompanyNotes}");
                }
                
            }
        }
        private static void PrintResult(string InitialMessage , List<CompanyModel> Companies)
        {
            if (Companies.Count > 0)
            {
                Console.WriteLine($"\n>>>>>{InitialMessage}<<<<<");
                foreach (var Company in Companies)
                {
                    Console.WriteLine($"Ticker {Company.Ticker}\t\t Valoracion {Company.Valoracion} \t\t Price {Company.UltimoPrecio:C}\t Volumen {Company.Volumen:N}");
                }
            }
        }
    }
}
