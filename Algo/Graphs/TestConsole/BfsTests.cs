using System;

using Algo.Graphs.Entities;
using Algo.Graphs.Paths;

namespace Algo.Graphs.TestConsole
{
    internal sealed class BfsTests
    {
        private readonly IGraph _figure41;

        public BfsTests()
        {
            _figure41 = new MatrixGraph(
                new int[][]
                {
                    new int[] { 1, 3, 4, 5 },
                    new int[] { 0, 2 },
                    new int[] { 1, 3 },
                    new int[] { 0, 2 },
                    new int[] { 0, 5 },
                    new int[] { 0, 4 }
                });
        }

        internal void BreadthFirst()
        {
            Console.WriteLine("========== Breadth-first ==========");
            BfsDistanceCalculator calculator = new BfsDistanceCalculator(_figure41);
            calculator.Run();
            Console.WriteLine(string.Join(", ", calculator.Distances));
        }
    }
}