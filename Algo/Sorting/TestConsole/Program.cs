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
        }

        private static void SortRandomArray(ISortEngine<int> engine)
        {
            Random random = new Random();
            IEnumerable<int> data = Enumerable.Range(0, 10)
                .Select(i => random.Next(50))
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
