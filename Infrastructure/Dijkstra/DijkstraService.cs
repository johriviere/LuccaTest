using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Dijkstra.Model;
using Infrastructure.Dijkstra.Result;

namespace Infrastructure.Dijkstra
{
    public class DijkstraService<Tv> : IDijkstraService<Tv>
        where Tv : IEquatable<Tv>
    {
        public DijkstraShortestPathResult<Tv> GetShortestPath(Vertex<Tv> vSource, Vertex<Tv> vTarget, Graph<Tv> graph)
        {
            /* Video source from which the algorithm was built
            * https://www.youtube.com/watch?v=4gvV7X1vcws */

            graph.SetDistance(vSource, 0);
            var predecessors = GetPredecessors(vSource, graph);
            var shortestPath = GetShortestPath(predecessors, vTarget);
            return shortestPath.Count > 1 ? new DijkstraShortestPathResult<Tv>(true, shortestPath) : new DijkstraShortestPathResult<Tv>(false);
        }

        private List<Predecessor<Tv>> GetPredecessors(Vertex<Tv> vSource, Graph<Tv> graph)
        {
            var predecessors = new List<Predecessor<Tv>>();
            var vComparer = new VertexComparer<Tv>();


            var vTreatables = graph.Vertices.Where(v => v.Id.Equals(vSource.Id)).ToList();

            while (vTreatables.Any() && vTreatables.Any(v => v.Distance != double.PositiveInfinity))
            {
                var vToTreat = vTreatables.OrderBy(v => v.Distance).ThenBy(v => v.Id).First(); // Find the new vertex to treat

                // Get its untreated vertices brothers
                var s1 = graph.Edges.Where(e => e.Vertex1.Id.Equals(vToTreat.Id)).Select(e => e.Vertex2);
                var s2 = graph.Edges.Where(e => e.Vertex2.Id.Equals(vToTreat.Id)).Select(v => v.Vertex1);

                var vSiblings = s1.Union(s2, vComparer).Where(v => !v.IsTreated).OrderByDescending(s => s.Distance).ToList();

                if (vSiblings.Any())
                {
                    foreach (var sibling in vSiblings)
                    {
                        var dis = vToTreat.Distance + 1;
                        if (dis < sibling.Distance) // distance from source vertex is shorter
                        {
                            sibling.Distance = dis; // Set the new distance on the current vertex
                            var pred = predecessors.SingleOrDefault(p => p.Vertex1.Id.Equals(sibling.Id)); // !!! to check : is it the right test ? It is possible in my case (all road have same weight)
                            if (pred == null)
                            {
                                predecessors.Add(new Predecessor<Tv>(sibling, vToTreat));
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

        private List<Vertex<Tv>> GetShortestPath(List<Predecessor<Tv>> predecessors, Vertex<Tv> vDestination)
        {
            List<Vertex<Tv>> shortestPath = new List<Vertex<Tv>> { vDestination };
            var stop = false;
            Vertex<Tv> vTarget = vDestination;

            while (!stop)
            {
                var pred = predecessors.SingleOrDefault(p => p.Vertex1.Id.Equals(vTarget.Id));
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
