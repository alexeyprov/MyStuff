using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algo.Common
{
    public static class CommonExtensions
    {
        public static void Swap<T>(this IList<T> data, int index, int otherIndex)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (index == otherIndex)
            {
                return;
            }

            T temp = data[index];
            data[index] = data[otherIndex];
            data[otherIndex] = temp;
        }

        public static void Swap<T>(this Span<T> data, int index, int otherIndex)
        {
            if (index == otherIndex)
            {
                return;
            }

            T temp = data[index];
            data[index] = data[otherIndex];
            data[otherIndex] = temp;
        }

        public static int PartitionTwoWay<T>(this IList<T> data, int start, int end, int pivotIndex = -1)
            where T : IComparable<T>
        {
            pivotIndex = CheckArguments(data, start, end, pivotIndex);
            if (start == end)
            {
                return start;
            }

            if (pivotIndex != end)
            {
                data.Swap(pivotIndex, end);
            }

            int boundary = start - 1;
            T pivotElement = data[end];
            for (int index = start; index < end; ++index)
            {
                if (data[index].CompareTo(pivotElement) < 0)
                {
                    data.Swap(++boundary, index);
                }
            }

            Debug.Assert(boundary < end);
            data.Swap(++boundary, end);

            return boundary;
        }

        public static (int lessThanBoundary, int equalsBoundary) PartitionThreeWay<T>(
            this IList<T> data, 
            int start, 
            int end, 
            int pivotIndex = -1)
                where T : IComparable<T>
        {
            pivotIndex = CheckArguments(data, start, end, pivotIndex);
            if (start == end)
            {
                return (start, end);
            }

            if (pivotIndex != end)
            {
                data.Swap(pivotIndex, end);
            }

            // inclusive upper boundaries for the "< x" and "= x" intervals
            int lessThanBoundary = start - 1,
                equalsBoundary = start - 1;
            T pivotElement = data[end];
            for (int index = start; index < end; ++index)
            {
                int comparison = data[index].CompareTo(pivotElement);
                if (comparison < 0)
                {
                    // three-way swap
                    T current = data[index];
                    data[index] = data[++equalsBoundary];
                    data[equalsBoundary] = data[++lessThanBoundary];
                    data[lessThanBoundary] = current;
                }
                else if (comparison == 0)
                {
                    data.Swap(++equalsBoundary, index);
                }
            }

            Debug.Assert(equalsBoundary < end);
            data.Swap(++equalsBoundary, end);

            return (lessThanBoundary, equalsBoundary);
        }

        private static int CheckArguments<T>(ICollection<T> data, int start, int end, int pivotIndex)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (start < 0 || start >= data.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (end < start || end >= data.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(end));
            }

            if (pivotIndex == -1)
            {
                pivotIndex = end;
            }
            else if (pivotIndex < start || pivotIndex > end)
            {
                throw new ArgumentOutOfRangeException(nameof(pivotIndex));
            }

            return pivotIndex;
        }
    }
}