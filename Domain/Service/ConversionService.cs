using Domain.Model;
using Domain.PrimaryPort;
using Domain.SecondaryPort;
using Domain.Service.Constants;
using Domain.Service.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service
{
    public class ConversionService : IConversionService
    {
        private IShortestPathService _shortestPathService;

        public ConversionService(IShortestPathService shortestPathService)
        {
            _shortestPathService = shortestPathService;
        }

        public ConversionResult Convert(ConversionRequest request, IEnumerable<ExchangeRate> exchangeRates)
        {
            var shorterPathResult = _shortestPathService.Get(request, exchangeRates);

            if (shorterPathResult.IsFound)
            {
                // Process the conversion request
                var convertedAmount = ApplyRates(request.Amount, exchangeRates, shorterPathResult.Path.ToList());

                // round
                convertedAmount = Math.Round(convertedAmount, MidpointRounding.AwayFromZero);
                return new ConversionResult(true, Decimal.ToInt32(convertedAmount));
            }
            else
            {
                return new ConversionResult(false, null, ConversionErrorMessage.NoPath);
            }
        }

        private decimal ApplyRates(decimal amountToConvert, IEnumerable<ExchangeRate> exchangeRates, List<string> conversionPath)
        {
            var result = amountToConvert;
            for (var i = 0; i < conversionPath.Count - 1; i++)
            {
                var rate = GetRate(exchangeRates, conversionPath[i], conversionPath[i + 1]);
                result = Decimal.Round(result * rate, 4, MidpointRounding.AwayFromZero);

            }
            return result;
        }

        private decimal GetRate(IEnumerable<ExchangeRate> exchangesRates, string sourceCurrency, string targetCurrency)
        {
            decimal? sourceToTargetRate;
            decimal targetToSourceRate = 0.0000m;

            sourceToTargetRate = exchangesRates.SingleOrDefault(er => er.SourceCurrency.Equals(sourceCurrency) && er.TargetCurrency.Equals(targetCurrency))?.Rate;

            if (!sourceToTargetRate.HasValue)
            {
                targetToSourceRate = exchangesRates.SingleOrDefault(er => er.SourceCurrency.Equals(targetCurrency) && er.TargetCurrency.Equals(sourceCurrency)).Rate;
                targetToSourceRate = Decimal.Round((1 / targetToSourceRate), 4, MidpointRounding.AwayFromZero);
            }
            return sourceToTargetRate ?? targetToSourceRate;
        }
    }
}
