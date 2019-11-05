using System.Collections.Generic;

namespace Domain.Service.Result
{
    public class ShortestPathResult
    {
        public bool IsFound { get; }
        public IEnumerable<string> Path { get; }

        public ShortestPathResult(bool isFound, IEnumerable<string> path = null)
        {
            IsFound = isFound;
            Path = path;
        }
    }
}
