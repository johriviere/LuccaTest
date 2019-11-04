using Domain.Model;
using Domain.Service;
using Domain.Service.Dijkstra;
using Domain.Service.Dijkstra.Model;
using Domain.Service.Dijkstra.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Test.Domain.Test
{
    [TestClass]
    public class ConversionServiceTest
    {
        [TestMethod]
        public void Should_conversion_1_RON_return_25_JPY_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("RON", "JPY", 1);

            // create dependency mock
            var mock = new Mock<IDijkstraService>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex>(), It.IsAny<Vertex>(), It.IsAny<Graph>()))
                .Returns(new ShortestPathResult(true,
                            new List<Vertex> {
                                new Vertex("RON"),
                                new Vertex("USD"),
                                new Vertex("BGN"),
                                new Vertex("JPY")
                            }.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(25, result.Amount);
        }

        [TestMethod]
        public void Should_conversion_46_EUR_return_51_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("EUR", "USD", 46);

            // create dependency mock
            var mock = new Mock<IDijkstraService>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex>(), It.IsAny<Vertex>(), It.IsAny<Graph>()))
                .Returns(new ShortestPathResult(true,
                            new List<Vertex> {
                                new Vertex("EUR"),
                                new Vertex("CHF"),
                                new Vertex("USD")
                            }.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(51, result.Amount);
        }

        [TestMethod]
        public void Should_conversion_45_EUR_return_49_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("EUR", "USD", 45);

            // create dependency mock
            var mock = new Mock<IDijkstraService>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex>(), It.IsAny<Vertex>(), It.IsAny<Graph>()))
                .Returns(new ShortestPathResult(true,
                            new List<Vertex> {
                                new Vertex("EUR"),
                                new Vertex("CHF"),
                                new Vertex("USD")
                            }.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(49, result.Amount);
        }


        [TestMethod]
        public void Should_conversion_EUR_USD_49_fail_with_not_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesNotConnected;
            var conversionRequest = new ConversionRequest("USD", "UAH", 1);

            // create dependency mock
            var mock = new Mock<IDijkstraService>();
            mock.Setup(m => m.GetShortestPath(It.IsAny<Vertex>(), It.IsAny<Vertex>(), It.IsAny<Graph>()))
                .Returns(new ShortestPathResult(false, null));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
