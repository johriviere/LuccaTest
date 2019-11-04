using System.Collections.Generic;
using System.Linq;

namespace Domain.Service.Dijkstra.Model
{
    public class Graph
    {
        public IEnumerable<Vertex> Vertices { get; set; }
        public IEnumerable<Edge> Edges { get; set; }

        public void SetDistance(Vertex vertex, double distance)
        {
            VertexComparer vComparer = new VertexComparer();
            var target = this.Vertices.SingleOrDefault(v => vComparer.Equals(vertex, v));
            if (target != null)
            {
                target.Distance = distance;
            }
        }
    }
}
