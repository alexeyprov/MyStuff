using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Common;

namespace Algo.Sorting.Median
{
    public sealed class InPlaceSearchEngine<T> : ISelectEngine<T>
        where T : IComparable<T>
    {
        T ISelectEngine<T>.FindByRank(IEnumerable<T> data, int rank)
        {
            IList<T> source = data?.ToArray() ?? throw new ArgumentNullException(nameof(data));
            if (rank < 0 || rank >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            SelectTask task = new SelectTask(source);
            return task.FindElement(0, source.Count - 1, rank);
        }

        private sealed class SelectTask
        {
            private readonly IList<T> _source;
            private readonly Random _rnd;

            internal SelectTask(IList<T> source)
            {
                _source = source;
                _rnd = new Random();
            }

            internal T FindElement(int start, int end, int rank)
            {
                Debug.Assert(start >= 0);
                Debug.Assert(end < _source.Count);
                Debug.Assert(end >= start);
                Debug.Assert(rank >= 0);
                Debug.Assert(rank <= end - start);

                if (start == end)
                {
                    Debug.Assert(rank == 0);
                    return _source[start];
                }

                int probeIndex = start + _rnd.Next(end - start + 1);

                // inclusive upper boundaries for the "< x" and "= x" regions
                (int leftIndex, int rightIndex) = _source.PartitionThreeWay(start, end, probeIndex);

                if (rank <= leftIndex - start)
                {
                    return FindElement(start, leftIndex, rank);
                }
                else if (rank > leftIndex - start && rank <= rightIndex - start)
                {
                    return _source[probeIndex];
                }

                return FindElement(rightIndex + 1, end, rank - rightIndex - 1 + start);
            }
        }
    }
}