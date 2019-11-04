using System;
using System.Collections.Generic;

namespace Domain.Service.Dijkstra.Model

{
    public class Vertex
    {
        public string Currency { get; set; } // Unique identifier
        public double Distance { get; set; } // Distance to the 'source' vertex
        public bool IsTreated { get; set; } // Treated by 'shorter path' algorithm

        public Vertex(string currency)
        {
            Currency = currency;
            Distance = Double.PositiveInfinity;
        }
    }

    public class VertexComparer : IEqualityComparer<Vertex>
    {
        public bool Equals(Vertex x, Vertex y)
        {
            if (x == null && y == null)
                return false;
            else if (x == null || y == null)
                return false;
            else if (x.Currency == y.Currency)
                return true;
            else
                return false;
        }

        public int GetHashCode(Vertex obj)
        {
            return obj.Currency.GetHashCode();
        }
    }

}
