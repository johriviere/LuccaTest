namespace Domain.Model
{
    public class ExchangeRate
    {
        public string SourceCurrency { get; }
        public string TargetCurrency { get; }
        public decimal Rate { get; }

        public ExchangeRate(string sourceCurrency, string targetCurrency, decimal rate)
        {
            SourceCurrency = sourceCurrency;
            TargetCurrency = targetCurrency;
            Rate = rate;
        }
    }
}
