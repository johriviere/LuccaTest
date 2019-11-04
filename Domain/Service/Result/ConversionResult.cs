namespace Domain.Service.Result
{
    public class ConversionResult
    {
        public bool IsSuccess { get; set; }
        public int? Amount { get; set; }
        public string ErrorMessage { get; set; }

        public ConversionResult(bool isSuccess, int? amount, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            Amount = amount;
            ErrorMessage = errorMessage;
        }
    }
}
