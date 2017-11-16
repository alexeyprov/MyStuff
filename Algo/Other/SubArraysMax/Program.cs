using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Other.SubArraysMax
{
    /// <summary>
    /// In array of size n, find maximum for all continuous sub-arrays of size k &lt;&eq; n
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Parameters parameters = ParseArguments(args);

            if (parameters == null)
            {
                Console.WriteLine("Usage: dotnet run <array_size> <chunk_size>");
                return;
            }

            Random rnd = new Random();
            IReadOnlyList<int> array = Enumerable.Range(0, parameters.ArraySize)
                .Select(_ => rnd.Next(parameters.ArraySize) + 1)
                .ToArray();

            Console.WriteLine(string.Join(" ", array));
            TestIterator<SimpleIterator>(array, parameters.ChunkSize);
            TestIterator<SearchTreeIterator>(array, parameters.ChunkSize);
        }

        private static Parameters ParseArguments(IReadOnlyList<string> args)
        {
            int chunkSize, arraySize;

            if (args.Count != 2 || 
                !int.TryParse(args[0], out arraySize) || 
                !int.TryParse(args[1], out chunkSize))
            {
                return null;
            }

            if (arraySize < 1 || chunkSize < 1)
            {
                return null;
            }

            chunkSize = Math.Min(arraySize, chunkSize);

            return new Parameters
            {
                ArraySize = arraySize,
                ChunkSize = chunkSize
            };
        }

        private static void TestIterator<T>(IReadOnlyList<int> array, int chunkSize)
            where T : IArrayIterator, new()
        {
            IArrayIterator iterator = new T();

            Console.WriteLine(iterator.GetType().Name);
            Console.WriteLine(string.Join(" ", iterator.FindMaximums(array, chunkSize)));
        }

        private class Parameters
        {
            public int ArraySize { get; set; }

            public int ChunkSize { get; set; }
        }
    }
}
