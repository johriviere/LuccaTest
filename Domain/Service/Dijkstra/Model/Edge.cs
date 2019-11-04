namespace Domain.Service.Dijkstra.Model
{
    public class Edge
    {
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }
        public decimal Rate { get; set; }
    }
}
