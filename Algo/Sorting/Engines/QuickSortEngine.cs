using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Common;

namespace Algo.Sorting.Engines
{
    public class QuickSortEngine<T> : ISortEngine<T>
        where T : IComparable<T>
    {
        IEnumerable<T> ISortEngine<T>.Sort(IEnumerable<T> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            SortTask task = new SortTask(data.ToArray());
            return task.GetResult();
        }

        private class SortTask
        {
            private readonly IList<T> _data;    

            public SortTask(IList<T> data)
            {
                _data = data;
            }

            public IEnumerable<T> GetResult()
            {
                QuickSort(0, _data.Count - 1);
                return _data;
            }

            private void QuickSort(int p, int r)
            {
                if (p >= r)
                {
                    return;
                }

                int q = Partition(p, r);

                QuickSort(p, q - 1);
                QuickSort(q + 1, r);
            }

            private int Partition(int p, int r)
            {
                T x = _data[r];
                int i = p - 1;

                for (int j = p; j < r; ++j)
                {
                    if (_data[j].CompareTo(x) < 0)
                    {
                        _data.Swap(++i, j);
                    }
                }

                _data.Swap(++i, r);
                return i;
            }
        }
    }
}