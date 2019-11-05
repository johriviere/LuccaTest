using Application.ConsoleClient.Validation;
using Application.ConsoleClient.Validation.Model;
using Domain.Model;
using Domain.PrimaryPort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.ConsoleClient.Adapter
{
    public class ConsoleAdapter
    {
        private IValidationService<IEnumerable<string>> _validationService;
        private IConversionService _conversionService;

        public ConsoleAdapter(IValidationService<IEnumerable<string>> validationService, IConversionService conversionService)
        {
            _validationService = validationService;
            _conversionService = conversionService;
        }

        public void Run(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            if (_validationService.IsValid(lines))
            {
                var result = _conversionService.Convert(ToConversionRequest(lines), ToExchangesRates(lines));
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
                Console.WriteLine(ErrorMessage.InconsistentInputData);
                Console.WriteLine($"the file {filePath} contains inconsistent datas.");
            }
        }

        private ConversionRequest ToConversionRequest(IEnumerable<string> lines)
        {
            var crl = new LineConversionRequest(lines.First());
            return new ConversionRequest(crl.SourceCurrency, crl.TargetCurrency, crl.Amount);
        }

        private IEnumerable<ExchangeRate> ToExchangesRates(IEnumerable<string> lines)
        {
            var myLines = lines.Skip(2).Select(l => new LineExchangeRate(l));
            return myLines.Select(x => new ExchangeRate(x.SourceCurrency, x.TargetCurrency, x.Rate));

        }

      
    }
}
