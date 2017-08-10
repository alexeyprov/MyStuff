using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;

namespace Algo.Graphs.DepthFirstSearch
{
    public sealed class CycleDetector : SearchTreeBase
    {
        private readonly ICollection<Edge> _backwardEdges;

        public CycleDetector(IGraph graph) : base(graph)
        {
            _backwardEdges = new List<Edge>();
        }

        public bool HasCycles => _backwardEdges.Any();

        public IEnumerable<Edge> BackwardEdges => _backwardEdges;

        protected override void VisitBackwardEdge(int vertex, int parent)
        {
            _backwardEdges.Add(
                new Edge
                {
                    From = parent,
                    To = vertex
                });
        }
    }
}
