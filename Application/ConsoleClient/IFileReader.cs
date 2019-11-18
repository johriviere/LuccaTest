using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ConsoleClient
{
    public interface IFileReader
    {
        public string[] Read(string filePath);
    }
}
