using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;
using Algo.Heaps.Entities;

namespace Algo.Graphs.Paths
{
    public class DijkstraAlgorithmSlim : IShortestPathAlgorithm
    {
        private readonly IWeighedGraph _graph;

        public DijkstraAlgorithmSlim(IWeighedGraph graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        PathNavigator IShortestPathAlgorithm.Run()
        {
            int[] distances = Enumerable.Repeat(int.MaxValue, _graph.Size).ToArray();
            distances[0] = 0;
            IList<int> parents = Enumerable.Repeat(-1, _graph.Size).ToArray();
            IHeap<int, int> heap = CreateHeap(distances);
            ICollection<int> processedVertices = new HashSet<int>();

            while (heap.Count != 0)
            {
                int current = heap.Extract();

                if (processedVertices.Contains(current))
                {
                    continue;
                }

                processedVertices.Add(current);
                foreach (WeighedDistance neighbor in _graph.GetDistances(current))
                {
                    int next = neighbor.Vertex;
                    int oldDistance = distances[next];
                    int newDistance = distances[current] + neighbor.Weight;
                    if (newDistance < oldDistance)
                    {
                        distances[next] = newDistance;
                        parents[next] = current;
                        heap.Add(newDistance, next);
                    }
                }
            }

            return new PathNavigator(distances, parents);
        }

        protected virtual IHeap<int, int> CreateHeap(IReadOnlyCollection<int> distances) =>
            new BinaryHeap<int, int>(
                distances,
                Enumerable.Range(0, distances.Count),
                false);
    }
}