using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Graphs.Entities
{
    public sealed class MatrixWeighedGraph : IWeighedGraph
    {
        private readonly int _size;
        private readonly WeighedDistance[][] _distances;

        public MatrixWeighedGraph(WeighedDistance[][] rows)
        {
            _size = rows.Length;
            _distances = new WeighedDistance[_size][];
            for (int index = 0; index < _size; ++index)
            {
                WeighedDistance[] array = Enumerable.Range(0, _size)
                    .Select(i => new WeighedDistance(i, int.MaxValue))
                    .ToArray();
                foreach (WeighedDistance distance in rows[index])
                {
                    array[distance.Vertex] = distance;
                }
                _distances[index] = array;
             }
        }

        IEnumerable<WeighedDistance> IWeighedGraph.GetDistances(int vertex) =>
            _distances[vertex].Where(d => d.Weight != int.MaxValue).ToArray();

        int IGraph.Size => _size;

        IEnumerable<int> IGraph.GetAdjacentVertices(int source) =>
            ((IWeighedGraph)this).GetDistances(source)
                .Select(d => d.Vertex)
                .ToArray();

        IGraph IGraph.Reverse()
        {
            throw new NotImplementedException();
        }
    }
}