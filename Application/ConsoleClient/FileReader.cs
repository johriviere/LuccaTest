using System.IO;

namespace Application.ConsoleClient
{
    public class FileReader : IFileReader
    {
        public string[] Read(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
