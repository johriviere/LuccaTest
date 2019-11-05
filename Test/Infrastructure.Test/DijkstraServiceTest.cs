using Infrastructure.Dijkstra;
using Infrastructure.Dijkstra.Model;
using Infrastructure.Dijkstra.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Test
{
    [TestClass]
    public class DijkstraServiceTest
    {
        [TestMethod]
        public void Should_find_path_with_currencies_in_connected_graph()
        {
            // ARRANGE
            var vSource = new Vertex<string>("EUR");
            var vTarget = new Vertex<string>("USD");
            var vCHF = new Vertex<string>("CHF");

            List<Vertex<string>> vertices = new List<Vertex<string>>
            {
                vSource,
                vTarget,
                vCHF
            };
            List<Edge<string>> edges = new List<Edge<string>>
            {
                new Edge<string> { Vertex1 = vSource, Vertex2 = vCHF },
                new Edge<string> { Vertex1 = vCHF, Vertex2 = vTarget },
            };

            var graph = new Graph<string> { Vertices = vertices, Edges = edges };

            var expectedResult = new DijkstraShortestPathResult<string>(true, new List<Vertex<string>>
            {
                vSource, vCHF, vTarget
            });

            var svc = new DijkstraService<string>();

            // ACT
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            Assert.IsTrue(expectedResult.IsFound);
            Assert.IsTrue(Enumerable.SequenceEqual(expectedResult.Path, result.Path, new VertexComparer<string>()));
        }
       
        [TestMethod]
        public void Should_not_find_path_with_currencies_in_two_unconnected_graphs()
        {
            // ARRANGE
            var vSource = new Vertex<string>("EUR");
            var vTarget = new Vertex<string>("USD");
            var vCHF = new Vertex<string>("CHF");
            var vJPY = new Vertex<string>("JPY");

            List<Vertex<string>> vertices = new List<Vertex<string>>
            {
                vSource,
                vTarget,
                vCHF,
                vJPY
            };
            List<Edge<string>> edges = new List<Edge<string>>
            {
                new Edge<string> { Vertex1 = vSource, Vertex2 = vCHF},
                new Edge<string> { Vertex1 = vTarget, Vertex2 = vJPY},
            };

            var graph = new Graph<string> { Vertices = vertices, Edges = edges };

            var svc = new DijkstraService<string>();

            // ACT
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            Assert.IsFalse(result.IsFound);
            Assert.IsNull(result.Path);
        }
    }
}
