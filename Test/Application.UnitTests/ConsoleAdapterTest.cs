using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using Application.ConsoleClient;
using Application.ConsoleClient.Adapter;
using Application.ConsoleClient.Validation;
using Domain.PrimaryPort;
using System.Collections.Generic;
using System;
using Domain.Model;
using Domain.Service.Result;
using Domain.Service.Constants;

namespace Test.Application.UnitTests
{
    [TestClass]
    public class ConsoleAdapterTest
    {
        [TestMethod]
        public void Should_occurs_scenario_in_standard_case()
        {
            // ARRANGE
            var fakeStringPath = "oneFakeFile.txt";
            var fakeConversionResult = new ConversionResult(true, 444, null);
            var fakeFileContent = new string[]
            {
                "USD;5012;EUR",
                "3",
                "EUR;CHF;60.1046",
                "USD;CHF;0.9946",
                "RON;CHF;0.2322"
            }; /* Ici pour 'fakeFileContent', je suis obligé de mettre des données cohérentes, 
                * car pour la méthode publique Run(..) que je teste, de la classe 'ConsoleAdapter',
                * on fait un appel à la methode Convert(...) d'une de ses dépendances : '_conversionService'.
                * Cet appel Convert(..) prend 2 paramètres.
                * Ces 2 paramètres sont calculés chacun par des appels à des méthodes privées de 'ConsoleAdapter' : ToConversionRequest(..) et ToExchangesRates(..).
                * Ces 2 méthodes privées ne savent pas gérer des données incohérentes. */

            var fakeFileReader = Mock.Of<IFileReader>();
            Mock.Get(fakeFileReader).Setup(vs => vs.Read(fakeStringPath)).Returns(fakeFileContent);

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(true);

            var fakeWriter = new StubWriter();
            var fakeConversionService = Mock.Of<IConversionService>();
            Mock.Get(fakeConversionService).Setup(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(fakeConversionResult);

            // ACT
            var adapter = new ConsoleAdapter(fakeValidationService, fakeConversionService, fakeFileReader, fakeWriter);
            adapter.Run(fakeStringPath);

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            writerResult.Should().Be($"{fakeConversionResult.Amount.ToString()}{Environment.NewLine}");

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConversionService).Verify(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()), Times.Once());
        }

        [TestMethod]
        public void Should_occurs_scenario_if_data_are_inconsistent()
        {
            // ARRANGE
            var fakeStringPath = "oneFakeFile.txt";
            var fakeFileReader = Mock.Of<IFileReader>();

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(false);

            var fakeWriter = new StubWriter();
            var fakeConversionService = Mock.Of<IConversionService>();

            // ACT
            var adapter = new ConsoleAdapter(fakeValidationService, fakeConversionService, fakeFileReader, fakeWriter);
            adapter.Run(fakeStringPath);

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            var expectedMessage = $"{ErrorMessage.InconsistentInputData}{Environment.NewLine}the file {fakeStringPath} contains inconsistent datas.{Environment.NewLine}";
            writerResult.Should().Be(expectedMessage);

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConversionService).Verify(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()), Times.Never());
            

        }

        [TestMethod]
        public void Should_occurs_scenario_if_data_are_consistent_but_no_conversion_possible()
        {
            // ARRANGE
            var fakeStringPath = "oneFakeFile.txt";
            var fakeConversionResult = new ConversionResult(false, null, ConversionErrorMessage.NoPath); // in this scenario, the conversion must fail.
            var fakeFileContent = new string[]
            {
                "USD;5012;EUR",
                "3",
                "EUR;CHF;60.1046",
                "USD;CHF;0.9946",
                "RON;CHF;0.2322"
            };

            var fakeFileReader = Mock.Of<IFileReader>();
            Mock.Get(fakeFileReader).Setup(vs => vs.Read(fakeStringPath)).Returns(fakeFileContent);

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(true);

            var fakeWriter = new StubWriter();
            var fakeConversionService = Mock.Of<IConversionService>();
            Mock.Get(fakeConversionService).Setup(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()))
                .Returns(fakeConversionResult);

            // ACT
            var adapter = new ConsoleAdapter(fakeValidationService, fakeConversionService, fakeFileReader, fakeWriter);
            adapter.Run(fakeStringPath);

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            writerResult.Should().Be($"{ConversionErrorMessage.NoPath}{Environment.NewLine}");

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConversionService).Verify(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()), Times.Once());
        }
    }
 
}
