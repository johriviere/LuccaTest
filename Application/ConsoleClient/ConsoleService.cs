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
        public IConversionService IConversionService { get; set; }
        [Dependency]
        public IValidationService<IEnumerable<string>> IValidationService { get; set; }


        public void Run(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            if (IValidationService.IsValid(lines))
            {
                // Faire la conversion
                var cAdapter = new ConsoleAdapter();
                var result = IConversionService.Convert(cAdapter.ToConversionRequest(lines), cAdapter.ToExchangesRates(lines));
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
