using Domain.Model;
using Domain.Service.Result;
using System.Collections.Generic;

namespace Application.ConsoleClient.Adapter
{
    public interface IConsoleAdapter
    {
        ConversionRequest ToConversionRequest(IEnumerable<string> lines);
        IEnumerable<ExchangeRate> ToExchangesRates(IEnumerable<string> lines);
        ConversionResult Convert(string[] lines);
    }
}
