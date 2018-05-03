using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Sorting.Engines;

namespace Algo.Sorting.TestConsole
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            SortRandomArray(new QuickSortEngine<int>());
            SortRandomArray(new HeapSortEngine<int>());
            SortRandomArray(new CountingSortEngine());
            SortRandomArray(new RadixSortEngine(10), 10000);
            SortRandomArray(new RadixSortEngine(), 10000);
        }

        private static void SortRandomArray(
            ISortEngine<int> engine,
            int maxValue = 50)
        {
            Random random = new Random();
            IEnumerable<int> data = Enumerable.Range(0, 10)
                .Select(i => random.Next(maxValue))
                .ToArray();

            string engineName = engine.GetType().Name;
            PrintData($"Before ({engineName})", data);
            data = engine.Sort(data);
            PrintData($"After ({engineName})", data);
        }

        private static void PrintData(string prefix, IEnumerable<int> data)
        {
            Console.WriteLine(
                "{0}: {1}",
                prefix,
                string.Join(", ", data));
        }
    }
}
