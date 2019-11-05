using Domain.Model;
using Domain.SecondaryPort;
using Domain.Service.Result;
using Infrastructure.Dijkstra;
using Infrastructure.Dijkstra.Model;
using Infrastructure.Dijkstra.Result;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Adapter
{
    public class DijkstraAdapter : IShortestPathService
    {
        private IDijkstraService<string> _dijkstraService;

        public DijkstraAdapter(IDijkstraService<string> dijkstraService)
        {
            _dijkstraService = dijkstraService;
        }

        public ShortestPathResult Get(ConversionRequest conversionRequest, IEnumerable<ExchangeRate> exchangeRates)
        {
            var graph = ToGraph(exchangeRates);
            Vertex<string> vSource = new Vertex<string>(conversionRequest.SourceCurrency);
            Vertex<string> vTarget = new Vertex<string>(conversionRequest.TargetCurrency);
            var result = _dijkstraService.GetShortestPath(vSource, vTarget, graph);
            return ToShortestPathResult(result);
        }

        private Graph<string> ToGraph(IEnumerable<ExchangeRate> exchangesRates)
        {
            Graph<string> graph = new Graph<string>();

            var srcCurrencies = exchangesRates.Select(er => er.SourceCurrency);
            var dstCurrencies = exchangesRates.Select(er => er.TargetCurrency);
            var allCurrencies = srcCurrencies.Union(dstCurrencies).Distinct();

            graph.Vertices = allCurrencies.Select(c => new Vertex<string>(c)).ToList();

            graph.Edges = exchangesRates.Select(er => new Edge<string>
            {
                Vertex1 = graph.Vertices.Single(v => v.Id == er.SourceCurrency),
                Vertex2 = graph.Vertices.Single(v => v.Id == er.TargetCurrency),
            }).ToList();
            return graph;
        }

        private ShortestPathResult ToShortestPathResult(DijkstraShortestPathResult<string> result)
        {
            if (result.IsFound)
            {
                var path = result.Path.Select(v => v.Id);
                return new ShortestPathResult(true, path);
            }
            return new ShortestPathResult(false, null);
        }
    }
}
