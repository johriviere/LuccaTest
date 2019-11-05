using Application.ConsoleClient.Adapter;
using Application.Validation;
using Domain.PrimaryPort;
using System;
using System.Collections.Generic;
using System.IO;

namespace Application.ConsoleClient
{
    public class ConsoleService
    {
        private IValidationService<IEnumerable<string>> _validationService;

        private IConversionService _conversionService;

        public ConsoleService(IValidationService<IEnumerable<string>> validationService, IConversionService conversionService)
        {
            _validationService = validationService;
            _conversionService = conversionService;
        }

        public void Run(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            if (_validationService.IsValid(lines))
            {
                // Faire la conversion
                var cAdapter = new ConsoleAdapter();
                var result = _conversionService.Convert(cAdapter.ToConversionRequest(lines), cAdapter.ToExchangesRates(lines));
                if (result.IsSuccess)
                {
                    Console.WriteLine(result.Amount);
                }
                else
                {
                    Console.WriteLine(result.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine(ErrorMessage.INCONSISTENT_INPUT_DATA);
                Console.WriteLine($"the file {filePath} contains inconsistent datas.");
            }
        }
    }
}
