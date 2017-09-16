using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;

namespace Algo.Graphs.Paths
{
    public class BellmanFordAlgorithm : IShortestPathAlgorithm
    {
        private readonly IWeighedGraph _graph;

        public BellmanFordAlgorithm(IWeighedGraph graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        PathNavigator IShortestPathAlgorithm.Run()
        {
            IList<int> distances = Enumerable.Repeat(int.MaxValue, _graph.Size).ToArray();
            distances[0] = 0;
            IList<int> parents = new int[_graph.Size];

            for (int iteration = 0; iteration < _graph.Size; ++iteration)
            {
                if (!DoIteration(distances, parents))
                {
                    break;
                }
                else if (iteration == _graph.Size - 1)
                {
                    throw new NotSupportedException("Negative cycle(s) detected");
                }
            }

            return new PathNavigator(distances, parents);
        }

        private bool DoIteration(IList<int> distances, IList<int> parents)
        {
            bool isUpdated = false;

            foreach (var edge in distances
                .Select((d, i) => new { From = i, Distance = d})
                .Where(d => d.Distance != int.MaxValue)
                .SelectMany(f => _graph.GetDistances(f.From)
                    .Select(
                        d => new
                        {
                            f.From,
                            To = d.Vertex,
                            d.Weight
                        })))
            {
                int newDistance = distances[edge.From] + edge.Weight;
                if (newDistance < distances[edge.To])
                {
                    distances[edge.To] = newDistance;
                    parents[edge.To] = edge.From;
                    isUpdated = true;
                }
            }

            return isUpdated;
        }
    }
}