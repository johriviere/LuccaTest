namespace Application.Validation.Console
{
    public static class Constants
    {
        /* Testing regular expression :
        * https://www.regextester.com/ */
        public const string CONVERSION_REQUEST_LINE_REGEXP = @"^[a-zA-Z]{3};[0-9]+;[a-zA-Z]{3}$";
        public const string EXCHANGE_RATE_COUNT_LINE_REGEXP = @"^[0-9]+$";
        public const string EXCHANGE_RATE_LINE_REGEXP = @"[a-zA-Z]{3};[a-zA-Z]{3};[0-9]+\.[0-9]{4}$";

        public const char SEPARATOR = ';';
        public const int CURRENCY_LENGTH = 3;
        public const int MIN_LINES_INPUT_FILE = 3;
    }
}
