using System;
using System.Collections.Generic;

namespace Test.Application.Test.Validation.Console
{
    public static class Fake
    {
        public static List<string> ConsistentFileLines = new List<string>
        {
            "USD;5012;EUR",
            "3",
            "EUR;CHF;60.1046",
            "USD;CHF;0.9946",
            "RON;CHF;0.2322"
        };

        public static List<string> InconsistentFileLinesWithIncorrectConversionRequestLine = new List<string>
        {
            "US;123;EUR",
            "3",
            "EUR;CHF;60.1040",
            "USD;CHF;0.9946",
            "RON;CHF;0.2322"
        };

        public static List<string> InconsistentFileLinesWithIncorrectExchangeRateCountLine = new List<string>
        {
            "USD;123;EUR",
            "A",
            "EUR;CHF;60.1040",
            "USD;CHF;0.9946",
            "RON;CHF;0.2322"
        };

        public static List<string> InconsistentFileLinesWithAtLeastOneIncorrectExchangeRateLine = new List<string>
        {
            "USD;5012;EUR",
            "3",
            "EUR;CHF;60.1040m",
            "USD;CHF;0.9946",
            "RON;CHF;0.2322"
        };

        public static List<string> InconsistentFileLinesWithLessThan3Lines = new List<string>
        {
            "USD;5012;EUR",
            "0"
        };

        public static List<string> InconsistentFileLinesWithDeclaredCountAndRealCountNotMatch = new List<string>
        {
            "USD;5012;EUR",
            "4",
            "EUR;CHF;1.1046",
            "USD;CHF;0.9946",
            "RON;CHF;0.2322"
        };

        public static List<string> InconsistentFileLinesWitExchangeRateContainingDuplicateCurrency = new List<string>
        {
            "USD;5012;EUR",
            "4",
            "EUR;CHF;1.1046",
            "USD;CHF;0.9946",
            "RON;CHF;.2322",
            "CHF;CHF;0.1234"
        };

        public static List<string> InconsistentFileLinesWitNoExchangeRateContainingSourceCurrency = new List<string>
        {
            "USD;5012;EUR",
            "5",
            "ARS;UAH;0.4229",
            "RON;CHF;0.2322",
            "BGN;UAH;14.2381",
            "BGN;JPY;61.8563",
            "BGN;USD;0.5677"
        };

        public static List<string> InconsistentFileLinesWitNoExchangeRateContainingTargetCurrency = new List<string>
        {
            "USD;5012;EUR",
            "2",
            "EUR;CHF;60.1046",
            "RON;CHF;0.2322"
        };
    }
}
