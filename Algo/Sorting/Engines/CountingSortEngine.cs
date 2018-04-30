using System;
using System.Linq;
using System.Collections.Generic;

namespace Algo.Sorting.Engines
{
    public sealed class CountingSortEngine : ISortEngine<int>
    {
        IEnumerable<int> ISortEngine<int>.Sort(IEnumerable<int> data)
        {
            IReadOnlyCollection<int> collection = 
                data as IReadOnlyCollection<int> ??
                data?.ToArray() ??
                throw new ArgumentNullException(nameof(data));

            (int minValue, int maxValue) = GetDataRange(collection);
            if (minValue >= maxValue)
            {
                return data;
            }

            IList<int> counts = CountValues(collection, minValue, maxValue);

            return GetSortedValues(collection, counts, minValue, maxValue);
        }

        private static (int minValue, int maxValue) GetDataRange(IEnumerable<int> data)
        {
            int minValue = int.MaxValue,
                maxValue = int.MinValue;

            foreach (int item in data)
            {
                if (item < minValue)
                {
                    minValue = item;
                }

                if (item > maxValue)
                {
                    maxValue = item;
                }
            }

            return (minValue, maxValue);
        }

        private static IList<int> CountValues(IEnumerable<int> data, int minValue, int maxValue)
        {
            int length = maxValue - minValue + 1;
            IList<int> counts = new int[length];
            foreach (int item in data)
            {
                counts[item - minValue]++;
            }

            for (int index = 1; index < length; ++index)
            {
                counts[index] += counts[index - 1];
            }

            return counts;
        }

        private static IEnumerable<int> GetSortedValues(
            IReadOnlyCollection<int> data,
            IList<int> counts,
            int minValue,
            int maxValue)
        {
            IList<int> sortedData = new int[data.Count];
            foreach (int item in data.Reverse())
            {
                sortedData[--counts[item - minValue]] = item; 
            }

            return sortedData;
        }
    }
}