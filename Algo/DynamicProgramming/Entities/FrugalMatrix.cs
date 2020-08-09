using System;
using System.Diagnostics;

namespace Algo.DynamicProgramming.Entities
{
    /// <summary>
    /// An optimized two-dimensional array, storing only data above a specific diagonal.
    /// Useful in scenarios when two-dimensional array elements A[i, j] represent subsequences
    /// s[i..j] of original sequence s[1..N].
    /// </summary>
    /// <remarks>
    /// For some scenarios (e.g., optimized BST), it may be necessary store data below main diagonal, i.e.
    /// A[i, j] where j &lt; i. The data structure allows it by supporting optional negative offset.
    /// </remarks>
    public sealed class FrugalMatrix<T>
    {
        private readonly T[] _data;
        private readonly int _size;
        private readonly int _negativeOffset;

        public FrugalMatrix(int size, int negativeOffset = 0)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (negativeOffset < 0 || negativeOffset >= size)
            {
                throw new ArgumentOutOfRangeException(nameof(negativeOffset));
            }

            int rowCount = size + negativeOffset;

            _data = new T[rowCount * (rowCount + 1) / 2];
            _size = size;
            _negativeOffset = negativeOffset;
        }

        public T this[int i, int j]
        {
            get
            {
                int index = TranslateIndex(i, j);
                return _data[index];
            }

            set
            {
                int index = TranslateIndex(i, j);
                _data[index] = value;
            }
        }

        private int TranslateIndex(int i, int j)
        {
            if (i < 1 || i > _size + _negativeOffset)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }

            if (j < i - _negativeOffset || j > _size)
            {
                throw new ArgumentOutOfRangeException(nameof(j));
            }

            int blockOffset = (i - 1) * (2 * _size - 2 * (_negativeOffset + 1) - i) / 2;
            Debug.Assert(blockOffset >= 0);
            Debug.Assert(blockOffset <= _data.Length);

            int position = j - i + _negativeOffset;
            return position + blockOffset;
        }
    }
}
