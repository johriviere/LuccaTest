using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using Application.Validation.Console;
using Domain.Service;
using Application.ConsoleClient;
using Application.ConsoleClient.Adapter;
using Application.ConsoleClient.Validation;
using Domain.PrimaryPort;
using System.Collections.Generic;
using System;
using Domain.Model;
using Domain.Service.Result;

namespace Test.Application.UnitTests
{
    [TestClass]
    public class ConsoleAdapterTest
    {
        [TestMethod]
        public void Should_occurs_scenario_in_standard_case()
        {
            // ARRANGE
            var fakeStringPath = "myfile.txt";
            var fakeConversionResult = new ConversionResult(true, 444, null);
            var fakeFileReader = Mock.Of<IFileReader>();

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

            /* Bug : mon test échoue :( => exception dans l'exécution...
             * Explications : 
             * L'appel mockée de _conversionService.Convert(...) a en paramètres 2 méthodes privées : ToConversionRequest(..) et ToExchangesRates(..).
             * Ces 2 methodes privées prennent en paramètre 'lines' qui est le resultat de la methode Read(..) du mock 'fakeFileReader'.
             * 'lines' est un string[] vide car je n'ai pas Setup la méthode Read(..). du mock 'fakeFileReader'.
             * les 2 méthodes privées ToConversionRequest(..) et ToExchangesRates(..) ne savent pas gérer un string[] vide ===> Exception ! */
             

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            writerResult.Should().Be(fakeConversionResult.ToString());

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConversionService).Verify(cs => cs.Convert(It.IsAny<ConversionRequest>(), It.IsAny<IEnumerable<ExchangeRate>>()), Times.Once());
        }



        [TestMethod]
        public void Should_occurs_scenario_if_data_are_inconsistent()
        {
            // ARRANGE
            var fakeStringPath = "myfile.txt";
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
    }
 
}
