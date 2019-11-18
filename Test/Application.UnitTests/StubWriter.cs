using Application.ConsoleClient;
using System.Text;

namespace Test.Application.UnitTests
{
    public class StubWriter : IWriter
    {
        public StringBuilder StringBuilder { get; set; }

        public StubWriter()
        {
            StringBuilder = new StringBuilder();
        }

        public void Write(object value)
        {
            StringBuilder.Append(value.ToString());
        }

        public void WriteLine(object value)
        {
            StringBuilder.AppendLine(value.ToString());
        }
    }
}
