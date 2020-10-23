using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TradingViewScreener.ExtensionMethods;

namespace TradingViewScreener.Models
{
    public class CompanyModel:IEquatable<CompanyModel>
    {
        static readonly public int MaxRSI = 70;
        static readonly public int MedRSI = 50;
        static readonly public int MinRSI = 30;
        static readonly public int MinVolumen = 500;
        static readonly public int MidVolumen = 5000;

        public CompanyModel(string p)
        {
            string[] RowDetails = p.Split(",");
            Ticker = RowDetails[0];
            Valoracion = RowDetails[1];
            Volumen = RowDetails[2].ToDouble();
            UltimoPrecio = RowDetails[3].ToDouble();
            SMA20 = RowDetails[4].ToDouble();
            SMA50 = RowDetails[5].ToDouble();
            SMA200 = RowDetails[6].ToDouble();
            BollingerBandUpper = RowDetails[7].ToDouble();
            BollingerBandLower = RowDetails[8].ToDouble();
            NivelMACD = RowDetails[9].ToDouble();
            SenalMACD= RowDetails[10].ToDouble();
            RSI = RowDetails[11].ToDouble();
        }

        [Required]
        public string Ticker { get; set; }
        public string Valoracion { get; set; }
        public double UltimoPrecio { get; set; }
        public double SMA20 { get; set; }
        public double SMA50 { get; set; }
        public double SMA200 { get; set; }
        public double BollingerBandUpper { get; set; }
        public double BollingerBandLower { get; set; }
        public double NivelMACD { get; set; }
        public double SenalMACD { get; set; }
        public double Volumen { get; set; }
        public double RSI { get; set; }

        public bool Equals([NotNull] CompanyModel other)
        {
            return Ticker.Equals(other.Ticker,StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(CompanyModel) ? Ticker.Equals((obj as CompanyModel).Ticker, StringComparison.InvariantCultureIgnoreCase):false ;
        }
    }
}
