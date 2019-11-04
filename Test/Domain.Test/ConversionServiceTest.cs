using Domain.Model;
using Domain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Unity;

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

            // ACT
            var svc = new ConversionService();
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

            // ACT
            var svc = new ConversionService();
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

            // ACT
            var svc = new ConversionService();
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

            // ACT
            var svc = new ConversionService();
            var result = svc.Convert(conversionRequest, exchangeRates);

            // ASSERT
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
