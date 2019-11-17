using Domain.Model;
using Domain.SecondaryPort;
using Domain.Service;
using Domain.Service.Constants;
using Domain.Service.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Test.Domain.UnitTests
{
    [TestClass]
    public class ConversionServiceTest
    {
        [TestMethod]
        public void Should_conversion_550_EUR_return_59033_JPY_with_connected_currencies_of_exam_instructions()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesOfExamInstructions;
            var conversionRequest = new ConversionRequest("EUR", "JPY", 550);

            // create dependency mock
            var mock = new Mock<IShortestPathService>();
            mock.Setup(m => m.Get(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(new ShortestPathResult(true, new List<string> { "EUR", "CHF", "AUD", "JPY" }.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
            result.Amount.Should().Be(59033);
            result.ErrorMessage.Should().BeNull();
        }

        [TestMethod]
        public void Should_conversion_1_RON_return_25_JPY_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("RON", "JPY", 1);

            var mock = new Mock<IShortestPathService>();
            mock.Setup(m => m.Get(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(new ShortestPathResult(true, new List<string> { "RON", "USD", "BGN", "JPY"}.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
            result.Amount.Should().Be(25);
            result.ErrorMessage.Should().BeNull();
        }

        [TestMethod]
        public void Should_conversion_46_EUR_return_51_USD_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("EUR", "USD", 46);

            var mock = new Mock<IShortestPathService>();
            mock.Setup(m => m.Get(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(new ShortestPathResult(true, new List<string> { "EUR", "CHF", "USD"}.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
            result.Amount.Should().Be(51);
            result.ErrorMessage.Should().BeNull();
        }

        [TestMethod]
        public void Should_conversion_45_EUR_return_50_USD_with_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesConnected;
            var conversionRequest = new ConversionRequest("EUR", "USD", 45);

            var mock = new Mock<IShortestPathService>();
            mock.Setup(m => m.Get(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(new ShortestPathResult(true, new List<string> { "EUR", "CHF", "USD" }.AsEnumerable()));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            result.IsSuccess.Should().BeTrue();
            result.Amount.Should().Be(50);
            result.ErrorMessage.Should().BeNull();
        }

        [TestMethod]
        public void Should_conversion_EUR_USD_49_fail_with_not_connected_currencies()
        {
            // ARRANGE
            List<ExchangeRate> exchangeRates = ConversionServiceFake.ExchangeRatesNotConnected;
            var conversionRequest = new ConversionRequest("USD", "UAH", 1);

            var mock = new Mock<IShortestPathService>();
            mock.Setup(m => m.Get(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(new ShortestPathResult(false, null));

            var svc = new ConversionService(mock.Object);

            // ACT
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            result.IsSuccess.Should().BeFalse();
            result.Amount.Should().BeNull();
            result.ErrorMessage.Should().Be(ConversionErrorMessage.NoPath);
        }
    }
}
