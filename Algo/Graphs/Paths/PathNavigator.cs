using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Graphs.Paths
{
    public class PathNavigator
    {
        private IReadOnlyList<int> _distances;
        private IReadOnlyList<int> _parents;

        public PathNavigator(
            IEnumerable<int> distances,
            IEnumerable<int> parents)
        {
            _distances = ReadAsList(distances);
            _parents = ReadAsList(parents);
            if (_distances.Count != _parents.Count)
            {
                throw new ArgumentException("Mismatching arguments lengths");
            }
        }

        public IEnumerable<int> GetPath(int vertex) => GetPathHelper(vertex).Reverse();

        public int GetDistance(int vertex) => _distances[vertex];

        private static IReadOnlyList<int> ReadAsList(IEnumerable<int> data) =>
            data as IReadOnlyList<int> ??
            data?.ToArray() ??
            throw new ArgumentNullException(nameof(data));

        private IEnumerable<int> GetPathHelper(int vertex)
        {
            do
            {
                yield return vertex;
                vertex = _parents[vertex];
            }
            while (vertex != 0);
        }
    }
}