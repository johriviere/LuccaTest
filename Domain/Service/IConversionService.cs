using Domain.Model;
using Domain.Service.Result;
using System.Collections.Generic;

namespace Domain.Service
{
    public interface IConversionService
    {
        ConversionResult Convert(ConversionRequest request, IEnumerable<ExchangeRate> exchangeRates);
    }
}
