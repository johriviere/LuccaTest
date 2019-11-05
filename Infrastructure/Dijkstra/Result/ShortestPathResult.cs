using Infrastructure.Dijkstra.Model;
using System;
using System.Collections.Generic;

namespace Infrastructure.Dijkstra.Result
{
    public class ShortestPathResult<Tv>
        where Tv : IEquatable<Tv>
    {
        public bool IsFound { get; }
        public IEnumerable<Vertex<Tv>> Path { get; }

        public ShortestPathResult(bool isFound, IEnumerable<Vertex<Tv>> path = null)
        {
            IsFound = isFound;
            Path = path;
        }
    }
}
