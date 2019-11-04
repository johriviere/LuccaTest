namespace Domain.Service.Dijkstra.Model
{
    public class Predecessor
    {
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }

        public Predecessor(Vertex v1, Vertex v2)
        {
            Vertex1 = v1;
            Vertex2 = v2;
        }
    }
}
