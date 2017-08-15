using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Graphs.Entities;

namespace Algo.Graphs.Paths
{
    public sealed class BfsDistanceCalculator : BfsAlgorithmBase
    {
        public readonly List<int> _distances;

        public BfsDistanceCalculator(IGraph graph) : base(graph)
        {
            _distances = Enumerable.Repeat(int.MaxValue, graph.Size).ToList();
        }

        public IEnumerable<int> Distances => _distances;

        public override void Run()
        {
            for (int index = 0; index < _distances.Count; ++index)
            {
                _distances[index] = int.MaxValue;
            }

            base.Run();
        }

        protected override void Explore(Edge edge)
        {
            base.Explore(edge);

            int vertex = edge.To;
            Debug.Assert(vertex >= 0);
            Debug.Assert(vertex < _distances.Count);
            Debug.Assert(_distances[vertex] == int.MaxValue);

            int parent = edge.From;
            _distances[vertex] = parent >= 0 ? _distances[parent] + 1 : 0;
        }
    }
}