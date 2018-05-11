using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Sorting.Engines;
using Algo.Sorting.Median;

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

            SelectMedian(new SearchEngine<int>());
            SelectMedian(new InPlaceSearchEngine<int>());
        }

        private static void SortRandomArray(
            ISortEngine<int> engine,
            int maxValue = 50)
        {
            IEnumerable<int> data = GenerateRandomSequence(maxValue);
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

        private static IEnumerable<int> GenerateRandomSequence(int maxValue)
        {
            Random random = new Random();
            return Enumerable.Range(0, 10)
                .Select(i => random.Next(maxValue))
                .ToArray();
        }

        private static void SelectMedian(
            ISelectEngine<int> engine,
            int maxValue = 100)
        {
            IEnumerable<int> data = GenerateRandomSequence(maxValue);
            string engineName = engine.GetType().Name;
            PrintData($"{engineName} - unsorted", data);

            ISortEngine<int> sortEngine = new QuickSortEngine<int>();
            data = sortEngine.Sort(data);
            PrintData($"{engineName} - sorted", data);

            int median = engine.FindMedian(data);
            Console.WriteLine($"{engineName}'s median is {median}");
        }
    }
}
