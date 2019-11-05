using Application.Validation.Console.Model;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Application.ConsoleClient.Adapter
{
    public class ConsoleAdapter
    {
        public ConversionRequest ToConversionRequest(IEnumerable<string> lines)
        {
            var crl = new LineConversionRequest(lines.First());
            return new ConversionRequest(crl.SourceCurrency, crl.TargetCurrency, crl.Amount);
        }

        public IEnumerable<ExchangeRate> ToExchangesRates(IEnumerable<string> lines)
        {
            var myLines = lines.Skip(2).Select(l => new LineExchangeRate(l));
            return myLines.Select(x => new ExchangeRate(x.SourceCurrency, x.TargetCurrency, x.Rate));

        }
    }
}
