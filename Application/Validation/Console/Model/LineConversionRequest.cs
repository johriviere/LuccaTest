namespace Application.Validation.Console.Model
{
    public class LineConversionRequest
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public int Amount { get; }

        public LineConversionRequest(string line)
        {
            SourceCurrency = line.Substring(0, Constants.CURRENCY_LENGTH);
            TargetCurrency = line.Substring(line.LastIndexOf(Constants.SEPARATOR) + 1, Constants.CURRENCY_LENGTH);

            var am = line.Substring(line.IndexOf(Constants.SEPARATOR) + 1,
                                   line.LastIndexOf(Constants.SEPARATOR) - line.IndexOf(Constants.SEPARATOR) - 1);
            Amount = int.Parse(am);
        }
    }
}
