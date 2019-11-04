using System;
using System.Globalization;

namespace Application.Validation.Console.Model
{
    public class LineExchangeRate
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public decimal Rate { get; }

        public LineExchangeRate(string line)
        {
            SourceCurrency = line.Substring(0, Constants.CURRENCY_LENGTH);
            TargetCurrency = line.Substring(line.IndexOf(Constants.SEPARATOR) + 1, Constants.CURRENCY_LENGTH);

            var rate = line.Substring(line.LastIndexOf(Constants.SEPARATOR) + 1,
                                       line.Length - line.LastIndexOf(Constants.SEPARATOR) - 1);
            Rate = Decimal.Parse(rate, CultureInfo.InvariantCulture);
        }
    }
}
