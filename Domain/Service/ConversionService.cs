using Domain.Model;
using Domain.Service.Constants;
using Domain.Service.Dijkstra;
using Domain.Service.Dijkstra.Model;
using Domain.Service.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Domain.Service
{
    public class ConversionService : IConversionService
    {
        [Dependency]
        private IDijkstraService _dijkstraService;

        public ConversionService(IDijkstraService dijkstraService)
        {
            _dijkstraService = dijkstraService;
        }


        public ConversionResult Convert(ConversionRequest request, IEnumerable<ExchangeRate> exchangeRates)
        {
            /*  Converts exchange rates into a data structure that can be applied 
             *  the Dijkstra's search algorithm */
            var graph = ToGraph(exchangeRates);

            // Find, if exist, the shortest conversion path that will be used to process the request
            Vertex vSource = new Vertex(request.SourceCurrency);
            Vertex vTarget = new Vertex(request.TargetCurrency);
            var shorterPathResult = _dijkstraService.GetShortestPath(vSource, vTarget, graph);

            if (shorterPathResult.IsFound)
            {
                // Process the conversion request
                var convertedAmount = ApplyRates(request.Amount, shorterPathResult.Path.ToList(), graph.Edges);
                return new ConversionResult(true, Decimal.ToInt32(convertedAmount));
            }
            else
            {
                return new ConversionResult(false, null, ConversionErrorMessage.NO_PATH);
            }
        }

        private Graph ToGraph(IEnumerable<ExchangeRate> exchangesRates)
        {
            Graph graph = new Graph();

            var srcCurrencies = exchangesRates.Select(er => er.SourceCurrency);
            var dstCurrencies = exchangesRates.Select(er => er.TargetCurrency);
            var allCurrencies = srcCurrencies.Union(dstCurrencies).Distinct();

            graph.Vertices = allCurrencies.Select(c => new Vertex(c)).ToList();

            graph.Edges = exchangesRates.Select(er => new Edge
            {
                Vertex1 = graph.Vertices.Single(v => v.Currency == er.SourceCurrency),
                Vertex2 = graph.Vertices.Single(v => v.Currency == er.TargetCurrency),
                Rate = er.Rate
            }).ToList();
            return graph;
        }

        private decimal ApplyRates(decimal amountToConvert, List<Vertex> shortestConversionPath, IEnumerable<Edge> exchangeRates)
        {
            var result = amountToConvert;
            for (var i = 0; i < shortestConversionPath.Count - 1; i++)
            {
                var rate = GetRate(exchangeRates, shortestConversionPath[i], shortestConversionPath[i + 1]);
                result = Decimal.Round(result * rate, 4, MidpointRounding.AwayFromZero);

            }
            return result;
        }

        private decimal GetRate(IEnumerable<Edge> exchangesRates, Vertex vSource, Vertex vTarget)
        {
            var vComparer = new VertexComparer();
            decimal? rate1;
            decimal rate2 = 0.0000m;

            rate1 = exchangesRates.SingleOrDefault(e => vComparer.Equals(e.Vertex1, vSource) && vComparer.Equals(e.Vertex2, vTarget))?.Rate;
            if (!rate1.HasValue)
            {
                rate2 = exchangesRates.SingleOrDefault(e => vComparer.Equals(e.Vertex1, vTarget) && vComparer.Equals(e.Vertex2, vSource)).Rate;
                rate2 = Decimal.Round((1 / rate2), 4, MidpointRounding.AwayFromZero);
            }
            return rate1 ?? rate2;
        }
    }
}
