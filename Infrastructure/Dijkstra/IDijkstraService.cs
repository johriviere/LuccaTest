using Infrastructure.Dijkstra.Model;
using Infrastructure.Dijkstra.Result;
using System;

namespace Infrastructure.Dijkstra
{
    public interface IDijkstraService<Tv>
        where Tv : IEquatable<Tv>
    {
        ShortestPathResult<Tv> GetShortestPath(Vertex<Tv> vSource, Vertex<Tv> vTarget, Graph<Tv> graph);
    }
}