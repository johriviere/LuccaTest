using Domain.Service.Dijkstra;
using Domain.Service.Dijkstra.Model;
using Domain.Service.Dijkstra.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Test.Domain.Test
{
    [TestClass]
    public class DijkstraServiceTest
    {
        [TestMethod]
        public void Should_find_path_with_currencies_in_connected_graph()
        {
            // ARRANGE
            var vSource = new Vertex("EUR");
            var vTarget = new Vertex("USD");
            var vCHF = new Vertex("CHF");

            List<Vertex> vertices = new List<Vertex>
            {
                vSource,
                vTarget,
                vCHF
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge { Vertex1 = vSource, Vertex2 = vCHF, Rate = 1.1046m},
                new Edge { Vertex1 = vCHF, Vertex2 = vTarget, Rate = 1.0124m},
            };

            var graph = new Graph { Vertices = vertices, Edges = edges };

            var expectedResult = new ShortestPathResult(true, new List<Vertex>
            {
                vSource, vCHF, vTarget
            });

            // ACT
            var svc = new DijkstraService();
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            Assert.IsTrue(expectedResult.IsFound);
            Assert.IsTrue(Enumerable.SequenceEqual(expectedResult.Path, result.Path, new VertexComparer()));
        }
       
        [TestMethod]
        public void Should_not_find_path_with_currencies_in_two_unconnected_graphs()
        {
            // ARRANGE
            var vSource = new Vertex("EUR");
            var vTarget = new Vertex("USD");
            var vCHF = new Vertex("CHF");
            var vJPY = new Vertex("JPY");

            List<Vertex> vertices = new List<Vertex>
            {
                vSource,
                vTarget,
                vCHF,
                vJPY
            };
            List<Edge> edges = new List<Edge>
            {
                new Edge { Vertex1 = vSource, Vertex2 = vCHF, Rate = 1.1046m},
                new Edge { Vertex1 = vTarget, Vertex2 = vJPY, Rate = 108.4440m},
            };

            var graph = new Graph { Vertices = vertices, Edges = edges };

            // ACT
            var svc = new DijkstraService();
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            Assert.IsFalse(result.IsFound);
            Assert.IsNull(result.Path);
        }


    }
}
