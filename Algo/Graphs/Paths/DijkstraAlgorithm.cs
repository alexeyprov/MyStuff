using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;
using Algo.Heaps.Entities;

namespace Algo.Graphs.Paths
{
    public class DijkstraAlgorithm : IShortestPathAlgorithm
    {
        private readonly IWeighedGraph _graph;
       
        public DijkstraAlgorithm(IWeighedGraph graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        PathNavigator IShortestPathAlgorithm.Run()
        {
            int[] distances = Enumerable.Repeat(int.MaxValue, _graph.Size).ToArray();
            distances[0] = 0;
            IList<int> parents = Enumerable.Repeat(-1, _graph.Size).ToArray();
            IPriorityQueue<int, int> queue = CreatePriorityQueue(distances);

            while (queue.Count != 0)
            {
                int current = queue.Extract();

                foreach (WeighedDistance neighbor in _graph.GetDistances(current))
                {
                    int next = neighbor.Vertex;
                    int oldDistance = distances[next];
                    int newDistance = distances[current] + neighbor.Weight;
                    if (newDistance < oldDistance)
                    {
                        distances[next] = newDistance;
                        parents[next] = current;
                        queue.UpdateKey(next, oldDistance);
                    }
                }
            }

            return new PathNavigator(distances, parents);
        }

        protected virtual IPriorityQueue<int, int> CreatePriorityQueue(
            IReadOnlyList<int> distances) => new BinaryHeapPriorityQueue<int, int>(
                Enumerable.Range(0, distances.Count), 
                false, 
                i => distances[i]);
    }
}