using Application.ConsoleClient.Validation.Model;
using Domain.Model;
using Domain.PrimaryPort;
using Domain.Service.Result;
using System.Collections.Generic;
using System.Linq;

namespace Application.ConsoleClient.Adapter
{
    public class ConsoleAdapter : IConsoleAdapter
    {
        private IConversionService _conversionService;

        public ConsoleAdapter(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        public ConversionResult Convert(string[] lines)
        {
            var result = _conversionService.Convert(ToConversionRequest(lines), ToExchangesRates(lines));
            return result;
        }

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
