using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Heaps.Entities;

namespace Algo.Sorting.Engines
{
    public class HeapSortEngine<T> : ISortEngine<T>
        where T : IComparable<T>
    {
        IEnumerable<T> ISortEngine<T>.Sort(IEnumerable<T> data)
        {
            IList<T> array = data?.ToArray() ?? 
                throw new ArgumentNullException(nameof(data));
            IHeap<T, T> heap = new SimpleBinaryHeap<T>(array, true);

            for (int index = array.Count - 1; index > 0; --index)
            {
                T max = heap.Extract();
                array[index] = max;
            }

            return array;
        }
    }
}