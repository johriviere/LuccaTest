using Application.ConsoleClient.Adapter;
using Application.ConsoleClient.Validation;
using System.Collections.Generic;

namespace Application.ConsoleClient
{
    public class Orchestrator : IOrchestrator
    {
        private IValidationService<IEnumerable<string>> _validationService;
        private IFileReader _fileReader;
        private IWriter _writer;
        private IConsoleAdapter _consoleAdapter { get; }

        public Orchestrator(IFileReader fileReader, IWriter writer, IValidationService<IEnumerable<string>> validationService, IConsoleAdapter consoleAdapter)
        {
            _fileReader = fileReader;
            _writer = writer;
            _validationService = validationService;
            _consoleAdapter = consoleAdapter;
        }
        public void Run(string filePath)
        {
            var lines = _fileReader.Read(filePath);

            if (_validationService.IsValid(lines))
            {
                var result = _consoleAdapter.Convert(lines);
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
    }
}
