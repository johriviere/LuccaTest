using Application.ConsoleClient;
using Application.ConsoleClient.Adapter;
using Application.ConsoleClient.Validation;
using Domain.Service.Constants;
using Domain.Service.Result;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Test.Application.UnitTests
{
    [TestClass]
    public class OrchestratorTest
    {
        [TestMethod]
        public void Should_occurs_scenario_in_standard_case()
        {
            // ARRANGE
            var fakeConversionResult = new ConversionResult(true, 444, null); // In this scenario the conversion is success


            var fakeFileReader = Mock.Of<IFileReader>();
            Mock.Get(fakeFileReader).Setup(vs => vs.Read(It.IsAny<string>())).Returns(It.IsAny<string[]>());

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(true); // In this scenario the validation is success

            var fakeWriter = new StubWriter();

            var fakeConsoleAdapter = Mock.Of<IConsoleAdapter>();
            Mock.Get(fakeConsoleAdapter).Setup(ca => ca.Convert(It.IsAny<string[]>())).Returns(fakeConversionResult);

            // ACT
            var orchestrator = new Orchestrator(fakeFileReader, fakeWriter, fakeValidationService, fakeConsoleAdapter);
            orchestrator.Run(It.IsAny<string>());

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            writerResult.Should().Be($"{fakeConversionResult.Amount.ToString()}{Environment.NewLine}");

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConsoleAdapter).Verify(cs => cs.Convert(It.IsAny<string[]>()), Times.Once());
        }

        [TestMethod]
        public void Should_occurs_scenario_if_data_are_inconsistent()
        {
            // ARRANGE
            var fakeStringPath = "oneFakeFile.txt";
            var fakeFileReader = Mock.Of<IFileReader>();

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(false); // In this scenario, the validation fail

            var fakeWriter = new StubWriter();
            var fakeConsoleAdapter = Mock.Of<IConsoleAdapter>();

            // ACT
            var orchestrator = new Orchestrator(fakeFileReader, fakeWriter, fakeValidationService, fakeConsoleAdapter);
            orchestrator.Run(fakeStringPath);

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            var expectedMessage = $"{ErrorMessage.InconsistentInputData}{Environment.NewLine}the file {fakeStringPath} contains inconsistent datas.{Environment.NewLine}";
            writerResult.Should().Be(expectedMessage);

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConsoleAdapter).Verify(cs => cs.Convert(It.IsAny<string[]>()), Times.Never());
        }

        [TestMethod]
        public void Should_occurs_scenario_if_data_are_consistent_but_no_conversion_possible()
        {
            // ARRANGE
            var fakeFileReader = Mock.Of<IFileReader>();
            var fakeConversionResult = new ConversionResult(false, null, ConversionErrorMessage.NoPath); // In this scenario the conversion fail

            var fakeValidationService = Mock.Of<IValidationService<IEnumerable<string>>>();
            Mock.Get(fakeValidationService).Setup(vs => vs.IsValid(It.IsAny<string[]>()))
                .Returns(true); // In this scenario, the validation success

            var fakeWriter = new StubWriter();
            var fakeConsoleAdapter = Mock.Of<IConsoleAdapter>();
            Mock.Get(fakeConsoleAdapter).Setup(ca => ca.Convert(It.IsAny<string[]>())).Returns(fakeConversionResult);

            // ACT
            var orchestrator = new Orchestrator(fakeFileReader, fakeWriter, fakeValidationService, fakeConsoleAdapter);
            orchestrator.Run(It.IsAny<string>());

            // ASSERT
            var writerResult = fakeWriter.StringBuilder.ToString();
            writerResult.Should().Be($"{ConversionErrorMessage.NoPath}{Environment.NewLine}");

            // Verify method calls with Moq
            Mock.Get(fakeFileReader).Verify(cs => cs.Read(It.IsAny<string>()), Times.Once());
            Mock.Get(fakeValidationService).Verify(cs => cs.IsValid(It.IsAny<string[]>()), Times.Once());
            Mock.Get(fakeConsoleAdapter).Verify(cs => cs.Convert(It.IsAny<string[]>()), Times.Once());
        }
    }
}
