using Domain.Model;
using Domain.Service.Result;
using System.Collections.Generic;

namespace Domain.SecondaryPort
{
    public interface IShortestPathService
    {
        ShortestPathResult Get(ConversionRequest conversionRequest, IEnumerable<ExchangeRate> exchangeRates);
    }
}
