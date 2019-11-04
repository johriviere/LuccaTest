using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Application.Validation.Console.Model;

namespace Application.Validation.Console
{
    public class ValidationService : IValidationService<IEnumerable<string>>
    {
        public bool IsValid(IEnumerable<string> input)
        {
            if (IsValidFormatConsistency(input))
            {
                return IsValidLogicalConsistency(input);
            }
            return false;
        }

        private bool IsValidFormatConsistency(IEnumerable<string> lines)
        {
            if (lines.Count() >= Constants.MIN_LINES_INPUT_FILE)
            {
                var b1 = IsValidFormat(lines.First(), Constants.CONVERSION_REQUEST_LINE_REGEXP);
                var b2 = IsValidFormat(lines.Skip(1).First(), Constants.EXCHANGE_RATE_COUNT_LINE_REGEXP);
                var b3 = lines.Skip(2).All(l => IsValidFormat(l, Constants.EXCHANGE_RATE_LINE_REGEXP));

                return b1 && b2 && b3;
            }
            return false;
        }
        private bool IsValidFormat(string line, string regexp)
        {
            return new Regex(regexp).IsMatch(line);
        }
        private bool IsValidLogicalConsistency(IEnumerable<string> lines)
        {
            var firstLine = new LineConversionRequest(lines.First());
            var secondLine = new LineExchangeRateCount(lines.Skip(1).First());
            var otherLines = lines.Skip(2).Select(l => new LineExchangeRate(l));
            var inputFile = new InputFile(firstLine, secondLine, otherLines);

            /* Check if declared count of 'exchange rate' lines
             * match real count of theses lines */
            var test1 = inputFile.ExchangeRateCount.Count == inputFile.ExchangeRates.Count();

            /* Check if any exchange rate doesn't contain 
             * the same value for source and target currency */
            var test2 = !inputFile.ExchangeRates.Any(er => er.SourceCurrency == er.TargetCurrency);

            /* check that the 'source currency' of the 'conversion request'
             * exists at least once in the exchanges rates */
            var test3 = inputFile.ExchangeRates.Any(er => er.SourceCurrency == inputFile.ConversionRequest.SourceCurrency)
                     || inputFile.ExchangeRates.Any(er => er.TargetCurrency == inputFile.ConversionRequest.SourceCurrency);

            /* check that the 'target currency' of the 'conversion request'
             * exists at least once in the exchanges rates */
            var test4 = inputFile.ExchangeRates.Any(er => er.SourceCurrency == inputFile.ConversionRequest.TargetCurrency)
                    || inputFile.ExchangeRates.Any(er => er.TargetCurrency == inputFile.ConversionRequest.TargetCurrency);

            return test1 && test2 && test3 && test4;
        }


    }
}
