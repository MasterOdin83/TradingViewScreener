using System.Globalization;

namespace TradingViewScreener.ExtensionMethods
{
    public static class ParsingExtensionMethods
    {
        private static CultureInfo CultureProvider = new CultureInfo("es-MX", false);

        public static double ToDouble(this string value)
        {
            double Calculated = 0; 
            return  !string.IsNullOrEmpty(value) && double.TryParse(value, NumberStyles.Float, CultureProvider, out Calculated)  ? Calculated : 0;
        }
        
    }
}
