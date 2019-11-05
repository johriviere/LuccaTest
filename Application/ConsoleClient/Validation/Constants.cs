namespace Application.ConsoleClient.Validation
{
    public static class Constants
    {
        /* Testing regular expression :
        * https://www.regextester.com/ */
        public const string ConversionRequestLineRegexp = @"^[a-zA-Z]{3};[0-9]+;[a-zA-Z]{3}$";
        public const string ExchangeRateCountLineRegexp = @"^[0-9]+$";
        public const string ExchangeRateLineRegexp = @"[a-zA-Z]{3};[a-zA-Z]{3};[0-9]+\.[0-9]{4}$";

        public const char Separator = ';';
        public const int CurrencyLength = 3;
        public const int MinLinesInputFile = 3;
    }
}
