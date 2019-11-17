using Domain.Model;
using Infrastructure.Adapter;
using Infrastructure.Dijkstra;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Dijkstra.Model;
using FluentAssertions;

namespace Infrastructure.UnitTests
{
    [TestClass]
    public class DijkstraAdapterTest
    {
        [TestMethod]
        public void Should_find_path_with_currencies_in_connected_graph()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>{
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

            var conversionRequest = new ConversionRequest("ARS", "JPY", 35);

            var mock = new Mock<IDijkstraService<string>>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex<string>>(), It.IsAny<Vertex<string>>(), It.IsAny<Graph<string>>()))
                .Returns(new Dijkstra.Result.DijkstraShortestPathResult<string>(true,
                                                                        new List<Vertex<string>>{
                                                                        new Vertex<string>("ARS"),
                                                                        new Vertex<string>("UAH"),
                                                                        new Vertex<string>("BGN"),
                                                                        new Vertex<string>("JPY") }.AsEnumerable()));

            var svc = new DijkstraAdapter(mock.Object);

            // ACT
            var result = svc.Get(conversionRequest, exchangeRates);

            // ASSERT
            result.IsFound.Should().BeTrue();
            result.Path.Should().BeEquivalentTo(new[]{ "ARS", "UAH", "BGN", "JPY"}, options => options.WithStrictOrdering());
        }

        [TestMethod]
        public void Should_not_find_path_with_currencies_in_two_unconnected_graphs()
        {
            // ARRANGE
            // two unconnected graphs : EUR-ARS-UAH and CHF-RON-USD-BGN-JPY
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>{
                    new ExchangeRate("RON", "CHF", 0.2322m),
                    new ExchangeRate("USD", "RON", 4.2841m),
                    new ExchangeRate("USD", "CHF", 0.9946m),
                    new ExchangeRate("EUR", "ARS", 65.9057m),
                    new ExchangeRate("ARS", "UAH", 0.4229m),
                    new ExchangeRate("UAH", "EUR", 0.0359m),
                    new ExchangeRate("BGN", "JPY", 61.8563m),
                    new ExchangeRate("BGN", "USD", 0.5677m)
                };

            var conversionRequest = new ConversionRequest("ARS", "JPY", 35);

            var mock = new Mock<IDijkstraService<string>>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex<string>>(), It.IsAny<Vertex<string>>(), It.IsAny<Graph<string>>()))
                .Returns(new Dijkstra.Result.DijkstraShortestPathResult<string>(false, null));
            
            var svc = new DijkstraAdapter(mock.Object);

            // ACT
            var result = svc.Get(conversionRequest, exchangeRates);

            // ASSERT
            result.IsFound.Should().BeFalse();
            result.Path.Should().BeNull();
        }
    }
}
