using Application.ConsoleClient.Validation;
using Application.ConsoleClient.Validation.Model;
using Domain.Model;
using Domain.PrimaryPort;
using System.Collections.Generic;
using System.Linq;

namespace Application.ConsoleClient.Adapter
{
    public class ConsoleAdapter
    {
        private IValidationService<IEnumerable<string>> _validationService;
        private IConversionService _conversionService;
        private IFileReader _fileReader;
        private IWriter _writer;

        public ConsoleAdapter(IValidationService<IEnumerable<string>> validationService, IConversionService conversionService, IFileReader fileReader, IWriter writer)
        {
            _validationService = validationService;
            _conversionService = conversionService;
            _fileReader = fileReader;
            _writer = writer;
        }

        public void Run(string filePath)
        {
            var lines = _fileReader.Read(filePath);

            if (_validationService.IsValid(lines))
            {
                var result = _conversionService.Convert(ToConversionRequest(lines), ToExchangesRates(lines));
                if (result.IsSuccess)
                {
                    _writer.WriteLine(result.Amount);
                }
                else
                {
                    _writer.WriteLine(result.ErrorMessage);
                }
            }
            else
            {
                _writer.WriteLine(ErrorMessage.InconsistentInputData);
                _writer.WriteLine($"the file {filePath} contains inconsistent datas.");
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
