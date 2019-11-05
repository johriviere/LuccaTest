using System;

namespace Infrastructure.Dijkstra.Model
{
    public class Edge<T>
        where T : IEquatable<T>
    {
        public Vertex<T> Vertex1 { get; set; }
        public Vertex<T> Vertex2 { get; set; }
    }
}
