using Domain.Model;
using Domain.Service.Result;
using System.Collections.Generic;

namespace Domain.PrimaryPort
{
    public interface IConversionService
    {
        ConversionResult Convert(ConversionRequest request, IEnumerable<ExchangeRate> exchangeRates);
    }
}
