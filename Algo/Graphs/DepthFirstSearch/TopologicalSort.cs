using System.Collections.Generic;
using Algo.Graphs.Entities;

namespace Algo.Graphs.DepthFirstSearch
{
    public sealed class TopologicalSort : SearchTreeBase
    {
        private readonly Stack<int> _stack;
        private readonly bool _checkForLoops;
        private bool _hasLoops;

        public TopologicalSort(IGraph graph, bool checkForLoops = true) : base(graph)
        {
            _stack = new Stack<int>();
            _checkForLoops = checkForLoops;
        }

        protected override void VisitBackwardEdge(int vertex, int parent)
        {
            if (_checkForLoops)
            {
                _hasLoops = true;
            }
        }

        protected override void PostVisit(int vertex, bool isNewComponent)
        {
            base.PostVisit(vertex, isNewComponent);
            _stack.Push(vertex);
        }

        public IEnumerable<int> SortedGraph => _hasLoops ? null : _stack;
    }
}