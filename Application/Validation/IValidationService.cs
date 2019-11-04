namespace Application.Validation
{
    /// <summary>
    /// Service of validation of input data
    /// </summary>
    /// <typeparam name="T">input data</typeparam>
    public interface IValidationService<T>
    {
        bool IsValid(T input);
    }
}
