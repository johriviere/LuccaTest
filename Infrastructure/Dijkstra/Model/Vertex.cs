using System;
using System.Collections.Generic;

namespace Infrastructure.Dijkstra.Model
{
    public class Vertex<T>
        where T : IEquatable<T>
    {
        public T Id { get; set; } // Unique identifier
        public double Distance { get; set; } // Distance to the 'source' vertex
        public bool IsTreated { get; set; } // Treated by 'shortest path' algorithm

        public Vertex(T id)
        {
            Id = id;
            Distance = Double.PositiveInfinity;
        }
    }

    public class VertexComparer<T> : IEqualityComparer<Vertex<T>>
        where T : IEquatable<T>
    {
        public bool Equals(Vertex<T> x, Vertex<T> y)
        {
            if (x == null && y == null)
                return false;
            else if (x == null || y == null)
                return false;
            else if (Equals(x.Id, y.Id))
                return true;
            else
                return false;
        }

        public int GetHashCode(Vertex<T> obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
