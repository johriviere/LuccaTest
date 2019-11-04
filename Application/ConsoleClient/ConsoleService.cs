using Application.Validation;
using Domain.Service;
using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace Application.ConsoleClient
{
    public class ConsoleService
    {
        [Dependency]
        private IValidationService<IEnumerable<string>> _validationService { get; set; }

        [Dependency]
        private IConversionService _conversionService { get; set; }

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
