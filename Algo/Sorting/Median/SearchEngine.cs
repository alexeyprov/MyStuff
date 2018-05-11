using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algo.Sorting.Median
{
    public sealed class SearchEngine<T> : ISelectEngine<T>
        where T : IComparable<T>
    {
        private readonly Random _rnd;

        public SearchEngine()
        {
            _rnd = new Random();
        }

        T ISelectEngine<T>.FindByRank(IEnumerable<T> data, int rank)
        {
            IReadOnlyList<T> list = data as IReadOnlyList<T> ??
                data?.ToArray() ??
                throw new ArgumentNullException(nameof(data));

            if (rank < 0 || rank >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            return FindElement(list, rank);
        }

        private T FindElement(IReadOnlyList<T> source, int rank)
        {
            VerifySource(source);

            if (source.Count == 1)
            {
                Debug.Assert(rank == 0);
                return source[0];
            }

            T probe = source[_rnd.Next(source.Count)];

            List<T> left = new List<T>();
            List<T> middle = new List<T>();
            List<T> right = new List<T>();

            foreach (T element in source)
            {
                int compareResult = element.CompareTo(probe);
                if (compareResult < 0)
                {
                    left.Add(element);
                }
                else if (compareResult > 0)
                {
                    right.Add(element);
                }
                else
                {
                    middle.Add(element);
                }
            }

            if (rank < left.Count)
            {
                return FindElement(left, rank);
            }

            int leftAndMiddleCount = left.Count + middle.Count;
            Debug.Assert(leftAndMiddleCount > left.Count);

            return rank < leftAndMiddleCount ?
                middle[0] :
                FindElement(right, rank - leftAndMiddleCount);
        }

        private static void VerifySource(IReadOnlyList<T> source)
        {
            if (source == null || source.Count == 0)
            {
                throw new ArgumentNullException(nameof(source));
            }
        }
    }
}