using Infrastructure.Dijkstra;
using Infrastructure.Dijkstra.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;

namespace Infrastructure.UnitTests
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
            var svc = new DijkstraService<string>();

            // ACT
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            result.IsFound.Should().BeTrue();

            var expectedPath = new List<Vertex<string>> { vSource, vCHF, vTarget };
            result.Path.Should().BeEquivalentTo(expectedPath, options => options.Including(v => v.Id).WithStrictOrdering());
        }

        [TestMethod]
        public void Should_find_path_with_currencies_in_connected_graph_with_multiple_paths_with_same_distance()
        {
            // ARRANGE
            var vSource = new Vertex<string>("EUR");
            var vTarget = new Vertex<string>("USD");
            var vARS = new Vertex<string>("ARS");
            var vGBF = new Vertex<string>("GBF");
            var vIDR = new Vertex<string>("IDR");
            var vBIF = new Vertex<string>("BIF");
            var vFKP = new Vertex<string>("FKP");
            var vHTG = new Vertex<string>("HTG");
            var vCAD = new Vertex<string>("CAD");
            var vDJF = new Vertex<string>("DJF");
            var vJMD = new Vertex<string>("JMD");

            List<Vertex<string>> vertices = new List<Vertex<string>>
            {
                vSource,
                vTarget,
                vARS, vGBF, vIDR,
                vBIF, vFKP, vHTG,
                vCAD, vDJF, vJMD
            };
            List<Edge<string>> edges = new List<Edge<string>>
            {
                new Edge<string> { Vertex1 = vSource, Vertex2 = vARS },
                new Edge<string> { Vertex1 = vARS, Vertex2 = vGBF },
                new Edge<string> { Vertex1 = vGBF, Vertex2 = vIDR },
                new Edge<string> { Vertex1 = vIDR, Vertex2 = vTarget },

                new Edge<string> { Vertex1 = vSource, Vertex2 = vBIF },
                new Edge<string> { Vertex1 = vBIF, Vertex2 = vFKP },
                new Edge<string> { Vertex1 = vFKP, Vertex2 = vHTG },
                new Edge<string> { Vertex1 = vHTG, Vertex2 = vTarget },

                new Edge<string> { Vertex1 = vSource, Vertex2 = vCAD },
                new Edge<string> { Vertex1 = vCAD, Vertex2 = vDJF },
                new Edge<string> { Vertex1 = vDJF, Vertex2 = vJMD },
                new Edge<string> { Vertex1 = vJMD, Vertex2 = vTarget },
            };

            var graph = new Graph<string> { Vertices = vertices, Edges = edges };
            var svc = new DijkstraService<string>();

            // ACT
            var result = svc.GetShortestPath(vSource, vTarget, graph);

            // ASSERT
            result.IsFound.Should().BeTrue();

            /* if it exists 2 or more paths of same distance as here : 
             * EUR-ARS-GBF-IDR-USD
             * EUR-BIF-FKP-HTG-USD
             * EUR-CAD-DJF-JMD-USD,
             * the algorithm take the path with the 'before last' ID is lowest alphabetically (closest of letter A) */
            var expectedPath = new List<Vertex<string>> { vSource, vBIF, vFKP, vHTG, vTarget };
            result.Path.Should().BeEquivalentTo(expectedPath, options => options.Including(v => v.Id).WithStrictOrdering());
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
            result.IsFound.Should().BeFalse();
            result.Path.Should().BeNull();
        }
    }
}
