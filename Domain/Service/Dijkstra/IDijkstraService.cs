using Domain.Service.Dijkstra.Model;
using Domain.Service.Dijkstra.Result;

namespace Domain.Service.Dijkstra
{
    public interface IDijkstraService
    {
        ShortestPathResult GetShortestPath(Vertex vSource, Vertex vTarget, Graph graph);
    }
}
