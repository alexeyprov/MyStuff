using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Graphs.Entities
{
    public class MatrixGraph : IGraph
    {
        private readonly BitArray _matrix;
        private readonly int _size;

        public MatrixGraph(int[][] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                throw new ArgumentNullException(nameof(rows));
            }

            _size = rows.Length;
            _matrix = new BitArray(_size * _size);

            for (int rowIndex = 0; rowIndex < _size; ++rowIndex)
            {
                int[] row = rows[rowIndex];
                if (row == null)
                {
                    throw new ArgumentException(
                        $"Row {rowIndex} is undefined",
                        nameof(rows));
                }

                foreach (int colIndex in row)
                {
                    if (colIndex < 0 || colIndex >= _size)
                    {
                        throw new ArgumentException(
                            $"Invalid column value of {colIndex} for row {rowIndex}",
                            nameof(rows));
                    }

                    _matrix[rowIndex * _size + colIndex] = true;
                }
            }
        }

        private MatrixGraph(BitArray matrix)
        {
            _matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            _size = (int)Math.Sqrt(matrix.Length);
        }

        int IGraph.Size => _size;

        IEnumerable<int> IGraph.GetAdjacentVertices(int source)
        {
            if (source < 0 || source >= _size)
            {
                throw new ArgumentOutOfRangeException(
                    $"Source vertex index should be between 0 and {_size - 1}",
                    nameof(source));
            }

            int startIndex = source * _size;
            return Enumerable.Range(0, _size)
                .Where(i => _matrix[startIndex + i])
                .ToList();
        }

        IGraph IGraph.Reverse()
        {
            BitArray transposedMatrix = new BitArray(_matrix.Length);
            for (int rowIndex = 0; rowIndex < _size; ++rowIndex)
            {
                for (int colIndex = 0; colIndex < _size; ++colIndex)
                {
                    transposedMatrix[rowIndex + colIndex * _size] = _matrix[rowIndex * _size + colIndex];
                }
            }

            return new MatrixGraph(transposedMatrix);
        }
    }
}
