using System;
using System.Collections.Generic;

using Algo.Heaps.Entities;

namespace Algo.Heaps.TestConsole
{
    internal static class Program
    {
        private static void Main()
        {
            IEnumerable<int> heap = new SimpleBinaryHeap<int>(
                new int[] 
                {
                    5,
                    7,
                    2,
                    9,
                    6,
                    4,
                    3,
                    8,
                    1
                },
                true);
            Console.WriteLine(string.Join(", ", heap));
        }
    }
}
