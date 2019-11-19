using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using Application.ConsoleClient.Adapter;
using Domain.PrimaryPort;
using Domain.Model;
using System.Collections.Generic;

namespace Test.Application.UnitTests
{
    [TestClass]
    public class ConsoleAdapterTest
    {
        [TestMethod]
        public void Should_conversion_request_generated_in_standard_case()
        {
            // ARRANGE
            var fakeConversionService = Mock.Of<IConversionService>();
            var lines = new string[] // consistent lines
            {
                "EUR;5012;USD",
                "3",
                "EUR;CHF;60.1046",
                "USD;CHF;0.9946",
                "RON;CHF;0.2322"
            };

            // ACT
            ConsoleAdapter adapter = new ConsoleAdapter(fakeConversionService);
            var result = adapter.ToConversionRequest(lines);

            // ASSERT
            result.Amount.Should().Be(5012);
            result.SourceCurrency.Should().Be("EUR");
            result.TargetCurrency.Should().Be("USD");
        }

        [TestMethod]
        public void Should_exchanges_rates_generated_in_standard_case()
        {
            // ARRANGE
            var fakeConversionService = Mock.Of<IConversionService>();
            var lines = new string[] // consistent lines
            {
                "EUR;5012;USD",
                "3",
                "EUR;CHF;60.1046",
                "USD;CHF;0.9946",
                "RON;CHF;0.2322"
            };

            // ACT
            ConsoleAdapter adapter = new ConsoleAdapter(fakeConversionService);
            var result = adapter.ToExchangesRates(lines);

            // ASSERT
            result.Should().HaveCount(3);
            var expectedResult = new ExchangeRate[] {
                new ExchangeRate("EUR", "CHF", 60.1046m),
                new ExchangeRate("USD", "CHF", 0.9946m),
                new ExchangeRate("RON", "CHF", 0.2322m),
            };
            result.Should().BeEquivalentTo(expectedResult, options => options.WithStrictOrdering());
        }

        [TestMethod]
        public void Should_conversion_succeed_in_standard_case()
        {
            /* Ici, la méthode à tester ne fait quasi rien d'autre que d'appeler une dépendance.
             * Cela me semble INUTILE de faire un assert sur le retour de la méthode car c'est equivalent
             * au resultat de la dépendance que je mock.
             * Par contre, étant donné que les paramètres de la méthode testée sont générés par 2 méthodes de conversion
             * ToConversionRequest(..) et ToExchangesRates(..), il est UTILE de tester si la dépendance a été appelée une fois, correctement. 
             * En effet les méthodes de conversion pourraient potentiellement planter. */

            // ARRANGE
            var fakeConversionService = Mock.Of<IConversionService>();
            var lines = new string[] // consistent lines
            {
                "EUR;5012;USD",
                "3",
                "EUR;CHF;60.1046",
                "USD;CHF;0.9946",
                "RON;CHF;0.2322"
            };

            // ACT
            ConsoleAdapter adapter = new ConsoleAdapter(fakeConversionService);
            var result = adapter.Convert(lines);

            // ASSERT
            Mock.Get(fakeConversionService).Verify(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()), Times.Once());
        }
    }
}
