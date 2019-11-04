using Domain.Service.Dijkstra.Model;
using System.Collections.Generic;

namespace Domain.Service.Dijkstra.Result

{
    public class ShortestPathResult
    {
        public bool IsFound { get; }
        public IEnumerable<Vertex> Path { get; }

        public ShortestPathResult(bool isFound, IEnumerable<Vertex> path = null)
        {
            IsFound = isFound;
            Path = path;
        }
    }
}
