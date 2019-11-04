using Domain.Service.Dijkstra.Model;
using Domain.Service.Dijkstra.Result;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service.Dijkstra
{
    public class DijkstraService : IDijkstraService
    {
        public ShortestPathResult GetShortestPath(Vertex vSource, Vertex vTarget, Graph graph)
        {
            /* Video source from which the algorithm was built
            * https://www.youtube.com/watch?v=4gvV7X1vcws */

            graph.SetDistance(vSource, 0);
            var predecessors = GetPredecessors(vSource, graph);
            var shorterPath = GetShortestPath(predecessors, vTarget);
            return shorterPath.Count > 1 ? new ShortestPathResult(true, shorterPath) : new ShortestPathResult(false);
        }

        private List<Predecessor> GetPredecessors(Vertex vSource, Graph graph)
        {
            var predecessors = new List<Predecessor>();
            var vComparer = new VertexComparer();
            var vTreatables = graph.Vertices.Where(v => vComparer.Equals(vSource, v)).ToList();

            while (vTreatables.Any() && vTreatables.Any(v => v.Distance != double.PositiveInfinity))
            {
                var vToTreat = vTreatables.OrderBy(v => v.Distance).First(); // Find the new vertex to treat

                // Get its untreated vertex brothers
                var s1 = graph.Edges.Where(e => vComparer.Equals(e.Vertex1, vToTreat)).Select(v => v.Vertex2);
                var s2 = graph.Edges.Where(e => vComparer.Equals(e.Vertex2, vToTreat)).Select(v => v.Vertex1);
                var vSiblings = s1.Union(s2, vComparer).Where(v => !v.IsTreated).OrderByDescending(s => s.Distance).ToList();

                if (vSiblings.Any())
                {
                    foreach (var sibling in vSiblings)
                    {
                        var dis = vToTreat.Distance + 1;
                        if (dis < sibling.Distance) // distance from source vertex is shorter
                        {
                            sibling.Distance = dis; // Set the new distance on the current vertex
                            var pred = predecessors.SingleOrDefault(p => vComparer.Equals(p.Vertex1, sibling)); // !!! to check : is it the right test ? It is possible in my case (all road have same weight)
                            if (pred == null)
                            {
                                predecessors.Add(new Predecessor(sibling, vToTreat));
                            }
                            else
                            {
                                pred.Vertex2 = vToTreat;
                            }
                        }
                    }
                }
                vToTreat.IsTreated = true;
                vTreatables = graph.Vertices.Where(v => !v.IsTreated).ToList();
            }
            return predecessors;
        }

        private List<Vertex> GetShortestPath(List<Predecessor> predecessors, Vertex vDestination)
        {
            List<Vertex> shortestPath = new List<Vertex> { vDestination };
            VertexComparer vComparer = new VertexComparer();
            var stop = false;
            Vertex vTarget = vDestination;

            while (!stop)
            {
                var pred = predecessors.SingleOrDefault(v => vComparer.Equals(v.Vertex1, vTarget));
                if (pred != null)
                {
                    shortestPath.Add(pred.Vertex2);
                    vTarget = pred.Vertex2;
                }
                else
                {
                    stop = true;
                }
            }

            shortestPath.Reverse();
            return shortestPath;
        }
    }
}
