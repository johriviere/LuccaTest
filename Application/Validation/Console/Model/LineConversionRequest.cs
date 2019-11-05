namespace Application.Validation.Console.Model
{
    public class LineConversionRequest
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public int Amount { get; }

        public LineConversionRequest(string line)
        {
            SourceCurrency = line.Substring(0, Constants.CurrencyLength);
            TargetCurrency = line.Substring(line.LastIndexOf(Constants.Separator) + 1, Constants.CurrencyLength);

            var am = line.Substring(line.IndexOf(Constants.Separator) + 1,
                                   line.LastIndexOf(Constants.Separator) - line.IndexOf(Constants.Separator) - 1);
            Amount = int.Parse(am);
        }
    }
}
