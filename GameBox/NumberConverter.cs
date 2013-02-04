using System;
using System.Globalization;
using GameBox.XMLSerialization;

namespace GameBox
{
    internal static class NumberConverter
    {
        private static CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();

        static NumberConverter()
        {
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
        }

        public static float ParseFloat(string str)
        {
            return float.Parse(str, NumberStyles.Any, ci);
        }
    }
}
