using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Common;

namespace Algo.Sorting.Median
{
    public sealed class DeterministicSearchEngine<T> : ISelectEngine<T>
        where T : IComparable<T>
    {
        T ISelectEngine<T>.FindByRank(IEnumerable<T> data, int rank)
        {
            IList<T> source = data?.ToArray() ??
                throw new ArgumentNullException(nameof(data));

            if (rank < 0 || rank >= source.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(source));
            }

            SelectTask task = new SelectTask(source);

            return task.FindElement(0, source.Count - 1, rank);
        }

        private sealed class SelectTask
        {
            private readonly IList<T> _source;

            internal SelectTask(IList<T> source)
            {
                _source = source;
            }

            internal T FindElement(int start, int end, int rank)
            {
                if (start >= end)
                {
                    Debug.Assert(rank == 0);
                    return _source[start];
                }

                int medianOfMedians = FindMedian(
                    Enumerable.Range(start, end - start + 1).ToArray());
                (int lessThanBoundary, int equalsBoundary) = _source.PartitionThreeWay(
                    start, 
                    end, 
                    medianOfMedians);
                Debug.Assert(lessThanBoundary <= equalsBoundary);

                if (rank <= lessThanBoundary - start)
                {
                    return FindElement(start, lessThanBoundary, rank);
                }
                else if (rank > equalsBoundary - start)
                {
                    return FindElement(equalsBoundary + 1, end, rank - equalsBoundary - 1 + start);
                }

                return _source[equalsBoundary];
            }

            private int FindMedian(Span<int> indexes)
            {
                Debug.Assert(indexes.Length > 0);

                if (indexes.Length <= 5)
                {
                    InsertionSort(indexes);
                    return indexes[(indexes.Length - 1) / 2];
                }

                Span<int> medians = stackalloc int[(indexes.Length - 1) / 5 + 1];
                for (int start = 0, groupNo = 0; start < indexes.Length; start += 5)
                {
                    int size = Math.Min(5, indexes.Length - start);
                    medians[groupNo++] = FindMedian(indexes.Slice(start, size));
                }

                return FindMedian(medians);
            }

            private void InsertionSort(Span<int> indexes)
            {
                for (int targetPos = 0; targetPos < indexes.Length - 1; ++targetPos)
                {
                    int minPos = targetPos;
                    T minValue = _source[indexes[targetPos]];
                    for (int currentPos = targetPos + 1; currentPos < indexes.Length; ++currentPos)
                    {
                        T currentValue = _source[indexes[currentPos]];
                        if (currentValue.CompareTo(minValue) < 0)
                        {
                            minPos = currentPos;
                            minValue = currentValue;
                        }
                    }

                    if (minPos != targetPos)
                    {
                        indexes.Swap(minPos, targetPos);
                    }
                }
            }
        }
    }
}