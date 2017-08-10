using System;
using System.Collections.Generic;

namespace Algo.Sorting.Median
{
    internal static class Program
    {
        private static void Main()
        {
            for (int i = 0; i < 1000; ++i)
            {
                IList<byte> source = GenerateSource();
                //ClassicTest(source);
                InPlaceTest(source);
            }
        }

        private static void ClassicTest(IList<byte> source)
        {
            SearchEngine<byte> engine = new SearchEngine<byte>(source);
            Console.WriteLine($"Median is {engine.FindMedian()}");
        }

        private static void InPlaceTest(IList<byte> source)
        {
            InPlaceSearchEngine<byte> inplaceEngine = new InPlaceSearchEngine<byte>(source);
            Console.WriteLine($"Median (in-place) is {inplaceEngine.FindMedian()}");
        }

        private static IList<byte> GenerateSource()
        {
            byte[] source = new byte[20];
            Random rnd = new Random((int)DateTime.Now.Ticks);
            rnd.NextBytes(source);

            List<byte> sortedSource = new List<byte>(source);
            sortedSource.Sort();

            Console.WriteLine(string.Join(" ", sortedSource));

            return source;
        }
    }
}
