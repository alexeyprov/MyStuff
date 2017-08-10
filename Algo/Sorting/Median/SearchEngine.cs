using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algo.Sorting.Median
{
    public sealed class SearchEngine<T>
        where T : IComparable<T>
    {
        private readonly IList<T> _source;
        private readonly Random _rnd;

        public SearchEngine(IList<T> source)
        {
            VerifySource(source);
            _source = source;
            _rnd = new Random();
        }

        public T FindMedian()
        {
            return FindElement(_source, (_source.Count - 1) / 2);
        }

        private T FindElement(IList<T> source, int rank)
        {
            VerifySource(source);

            if (source.Count == 1)
            {
                Debug.Assert(rank == 0);
                return source[0];
            }

            T probe = source[_rnd.Next(source.Count)];

            IList<T> left = new List<T>();
            IList<T> middle = new List<T>();
            IList<T> right = new List<T>();

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

        private static void VerifySource(IList<T> source)
        {
            if (source == null || source.Count == 0)
            {
                throw new ArgumentNullException(nameof(source));
            }
        }
    }
}