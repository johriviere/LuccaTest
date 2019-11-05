using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Dijkstra.Model
{
    public class Graph<T>
        where T : IEquatable<T>
    {
        public IEnumerable<Vertex<T>> Vertices { get; set; }
        public IEnumerable<Edge<T>> Edges { get; set; }

        public void SetDistance(Vertex<T> vertex, double distance)
        {
            var target = this.Vertices.SingleOrDefault(v => v.Id.Equals(vertex.Id));
            if (target != null)
            {
                target.Distance = distance;
            }
        }
    }
}
