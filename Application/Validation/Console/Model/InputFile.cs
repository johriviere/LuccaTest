using System.Collections.Generic;

namespace Application.Validation.Console.Model
{
    public class InputFile
    {
        public LineConversionRequest ConversionRequest { get; }
        public LineExchangeRateCount ExchangeRateCount { get; }
        public IEnumerable<LineExchangeRate> ExchangeRates { get; }

        public InputFile(LineConversionRequest conversionRequest, LineExchangeRateCount exchangeRateCount, IEnumerable<LineExchangeRate> exchangeRates)
        {
            ConversionRequest = conversionRequest;
            ExchangeRateCount = exchangeRateCount;
            ExchangeRates = exchangeRates;
        }
    }
}
