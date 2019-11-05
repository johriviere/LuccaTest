using System;

namespace Infrastructure.Dijkstra.Model
{
    public class Predecessor<T>
        where T : IEquatable<T>
    {
        public Vertex<T> Vertex1 { get; set; }
        public Vertex<T> Vertex2 { get; set; }

        public Predecessor(Vertex<T> v1, Vertex<T> v2)
        {
            Vertex1 = v1;
            Vertex2 = v2;
        }
    }
}
