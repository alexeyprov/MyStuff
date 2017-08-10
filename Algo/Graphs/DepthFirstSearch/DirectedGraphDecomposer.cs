using System.Collections.Generic;
using Algo.Graphs.Entities;

namespace Algo.Graphs.DepthFirstSearch
{
    public sealed class DirectedGraphDecomposer : DfsAlgorithmBase
    {
        private readonly Stack<IEnumerable<int>> _components;
        private ICollection<int> _lastComponent;

        public DirectedGraphDecomposer(IGraph graph) :
            base(graph)
        {
            _components = new Stack<IEnumerable<int>>();
        }

        public IEnumerable<IEnumerable<int>> Components => _components;

        public override void Run(IEnumerable<int> vertices = null)
        {
            // first pass: sort a reverse graph
            TopologicalSort sort = new TopologicalSort(Graph.Reverse(), false);
            sort.Run();

            // second pass: use sort order to obtain components
            _lastComponent = new List<int>();
            base.Run(sort.SortedGraph);
        }

        protected override void PostVisit(int vertex, bool isNewComponent)
        {
            _lastComponent.Add(vertex);

            if (isNewComponent)
            {
                _components.Push(_lastComponent);
                _lastComponent = new List<int>();
            }
        }
    }
}