using System;
using System.Collections.Generic;

namespace Algo.Graphs.Entities
{
    public sealed class AdjacencyListGraph : IGraph
    {
        private readonly IReadOnlyList<LinkedList<int>> _lists;

        public AdjacencyListGraph(int[][] links)
        {
            LinkedList<int>[] lists = new LinkedList<int>[links.Length];
            for (int source = 0; source < links.Length; ++source)
            {
                lists[source] = new LinkedList<int>(links[source]);
            }

            _lists = lists;
        }

        private AdjacencyListGraph(IReadOnlyList<LinkedList<int>> lists)
        {
            _lists = lists ?? throw new ArgumentNullException(nameof(lists));
        }

        int IGraph.Size => _lists.Count;

        IEnumerable<int> IGraph.GetAdjacentVertices(int source) => _lists[source];

        IGraph IGraph.Reverse()
        {
            int index, size = _lists.Count;
            LinkedList<int>[] reverseLists = new LinkedList<int>[size];
            for (index = 0; index < size; ++index)
            {
                reverseLists[index] = new LinkedList<int>();
            }

            for (index = 0; index < size; ++index)
            {
                foreach (int target in _lists[index])
                {
                    reverseLists[target].AddLast(index);
                }
            }

            return new AdjacencyListGraph(reverseLists);
        }
    }
}
