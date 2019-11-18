namespace Application.ConsoleClient
{
    public interface IWriter
    {
        void WriteLine(object value);

        void Write(object value);
    }
}
