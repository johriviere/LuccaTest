using Application.Validation.Console;
using System;
using System.Globalization;

namespace Application.ConsoleClient.Validation.Model
{
    public class LineExchangeRate
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public decimal Rate { get; }

        public LineExchangeRate(string line)
        {
            SourceCurrency = line.Substring(0, Constants.CurrencyLength);
            TargetCurrency = line.Substring(line.IndexOf(Constants.Separator) + 1, Constants.CurrencyLength);

            var rate = line.Substring(line.LastIndexOf(Constants.Separator) + 1,
                                       line.Length - line.LastIndexOf(Constants.Separator) - 1);
            Rate = Decimal.Parse(rate, CultureInfo.InvariantCulture);
        }
    }
}
