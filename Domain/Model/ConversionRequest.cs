namespace Domain.Model
{
    public class ConversionRequest
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public int Amount { get; }

        public ConversionRequest(string sourceCurrency, string targetCurrency, int amount)
        {
            SourceCurrency = sourceCurrency;
            TargetCurrency = targetCurrency;
            Amount = amount;
        }
    }
}
