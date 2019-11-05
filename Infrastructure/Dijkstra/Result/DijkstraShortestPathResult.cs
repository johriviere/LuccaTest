using Infrastructure.Dijkstra.Model;
using System;
using System.Collections.Generic;

namespace Infrastructure.Dijkstra.Result
{
    public class DijkstraShortestPathResult<Tv>
        where Tv : IEquatable<Tv>
    {
        public bool IsFound { get; }
        public IEnumerable<Vertex<Tv>> Path { get; }

        public DijkstraShortestPathResult(bool isFound, IEnumerable<Vertex<Tv>> path = null)
        {
            IsFound = isFound;
            Path = path;
        }
    }
}
