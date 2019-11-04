namespace Application.Validation.Console.Model
{
    public class LineExchangeRateCount
    {
        public int Count { get; }

        public LineExchangeRateCount(string line)
        {
            Count = int.Parse(line);
        }
    }
}
