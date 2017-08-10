using System;
using System.Collections.Generic;

namespace Algo.Heaps.Entities
{
    public sealed class SimpleBinaryHeap<T> : BinaryHeap<T, T>
        where T : IComparable<T>
    {
        public SimpleBinaryHeap(
            IEnumerable<T> data,
            bool isMaxHeap,
            IComparer<T> comparer = null) :
            base(data, isMaxHeap, x => x, comparer)
        {
        }
    }
}