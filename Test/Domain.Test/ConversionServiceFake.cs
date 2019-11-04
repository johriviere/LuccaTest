using Domain.Model;
using System.Collections.Generic;

namespace Test.Domain.Test
{
    public class ConversionServiceFake
    {
        // One connected graph
        public static List<ExchangeRate> ExchangeRatesConnected = new List<ExchangeRate>
        {
            new ExchangeRate("EUR", "CHF", 1.1046m),
            new ExchangeRate("RON", "CHF", 0.2322m),
            new ExchangeRate("USD", "RON", 4.2841m),
            new ExchangeRate("USD", "CHF", 0.9946m),
            new ExchangeRate("EUR", "ARS", 65.9057m),
            new ExchangeRate("ARS", "UAH", 0.4229m),
            new ExchangeRate("UAH", "EUR", 0.0359m),
            new ExchangeRate("BGN", "UAH", 14.2381m),
            new ExchangeRate("BGN", "JPY", 61.8563m),
            new ExchangeRate("BGN", "USD", 0.5677m)
        };

        // Two unconnected graphs : USD-BGN-JPY and RON-CHF-EUR-ARS-UAH
        public static List<ExchangeRate> ExchangeRatesNotConnected = new List<ExchangeRate>
        {
            new ExchangeRate("EUR", "CHF", 1.1046m),
            new ExchangeRate("RON", "CHF", 0.2322m),
            new ExchangeRate("EUR", "ARS", 65.9057m),
            new ExchangeRate("ARS", "UAH", 0.4229m),
            new ExchangeRate("UAH", "EUR", 0.0359m),
            new ExchangeRate("BGN", "JPY", 61.8563m),
            new ExchangeRate("BGN", "USD", 0.5677m)
        };
    }
}
