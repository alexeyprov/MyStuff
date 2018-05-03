using System;
using System.Linq;
using System.Collections.Generic;

namespace Algo.Sorting.Engines
{
    public abstract class CountingSortEngineBase<T> : ISortEngine<T>
    {
        IEnumerable<T> ISortEngine<T>.Sort(IEnumerable<T> data)
        {
            IReadOnlyCollection<T> collection = 
                data as IReadOnlyCollection<T> ??
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

        protected abstract int GetItemKey(T item);

        private (int minValue, int maxValue) GetDataRange(IEnumerable<T> data)
        {
            int minValue = int.MaxValue,
                maxValue = int.MinValue;

            foreach (T item in data)
            {
                int key = GetItemKey(item);
                if (key < minValue)
                {
                    minValue = key;
                }

                if (key > maxValue)
                {
                    maxValue = key;
                }
            }

            return (minValue, maxValue);
        }

        private IList<int> CountValues(IEnumerable<T> data, int minValue, int maxValue)
        {
            int length = maxValue - minValue + 1;
            IList<int> counts = new int[length];
            foreach (T item in data)
            {
                counts[GetItemKey(item) - minValue]++;
            }

            for (int index = 1; index < length; ++index)
            {
                counts[index] += counts[index - 1];
            }

            return counts;
        }

        private IEnumerable<T> GetSortedValues(
            IReadOnlyCollection<T> data,
            IList<int> counts,
            int minValue,
            int maxValue)
        {
            IList<T> sortedData = new T[data.Count];
            foreach (T item in data.Reverse())
            {
                sortedData[--counts[GetItemKey(item) - minValue]] = item; 
            }

            return sortedData;
        }
    }
}