using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;
using Algo.Graphs.Paths;
using Algo.Heaps.Entities;

namespace Algo.Graphs.TestConsole
{
    internal sealed class BfsTests
    {
        private readonly IGraph _figure41;
        private readonly IWeighedGraph _figure46;
        private readonly IWeighedGraph _figure414;
        private readonly IWeighedGraph _negativeCycles;

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

            _figure46 = new MatrixWeighedGraph(
                new WeighedDistance[][]
                {
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 2),
                        new WeighedDistance(2, 1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(0, 2),
                        new WeighedDistance(2, 1),
                        new WeighedDistance(3, 2),
                        new WeighedDistance(4, 3)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(0, 1),
                        new WeighedDistance(1, 1),
                        new WeighedDistance(4, 4)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 2),
                        new WeighedDistance(4, 2)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 3),
                        new WeighedDistance(2, 4),
                        new WeighedDistance(3, 2)
                    }
                });

            _figure414 = new MatrixWeighedGraph(
                new WeighedDistance[][]
                {
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 10),
                        new WeighedDistance(7, 8)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(5, 2)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 1),
                        new WeighedDistance(3, 1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(4, 3)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(5, -1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(2, -2)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, -4),
                        new WeighedDistance(5, -1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(6, 1)
                    }
                });

            _negativeCycles = new MatrixWeighedGraph(
                new WeighedDistance[][]
                {
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, 10),
                        new WeighedDistance(7, 8)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(5, 2)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, -1),
                        new WeighedDistance(3, 1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(4, 3)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(5, -1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(2, -2)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(1, -4),
                        new WeighedDistance(5, -1)
                    },
                    new WeighedDistance[]
                    {
                        new WeighedDistance(6, 1)
                    }
                });
        }

        internal void BreadthFirst()
        {
            Console.WriteLine("========== Breadth-first ==========");
            BfsDistanceCalculator calculator = new BfsDistanceCalculator(_figure41);
            calculator.Run();
            Console.WriteLine(string.Join(", ", calculator.Distances));
        }

        internal void ShortestPath()
        {
            IShortestPathAlgorithm algo = new DijkstraAlgorithm(_figure46);
            TestShortestPath(algo, "4.6", 3, 4);

            algo = new DijkstraAlgorithmSlim(_figure46);
            TestShortestPath(algo, "4.6", 3, 4);

            algo = new DijkstraSlimWithDaryHeap(_figure46, 3);
            TestShortestPath(algo, "4.6", 3, 4);

            algo = new BellmanFordAlgorithm(_figure46);
            TestShortestPath(algo, "4.6", 3, 4);

            algo = new DijkstraAlgorithm(_figure414);
            TestShortestPath(algo, "4.14", 3, 4, 6);

            algo = new BellmanFordAlgorithm(_figure414);
            TestShortestPath(algo, "4.14", 3, 4, 6);

            algo = new BellmanFordAlgorithm(_negativeCycles);
            TestShortestPath(algo, "4.14 (w/ negative cycles)", 1, 3, 4);
        }

        private void TestShortestPath(
            IShortestPathAlgorithm algo, 
            string description, 
            params int[] targets)
        {
            try
            {
                Console.WriteLine(
                    $"========== Shortest-path ({description} via {algo.GetType().Name}) ==========");
                PathNavigator navigator = algo.Run();
                foreach (int target in targets)
                {
                    DumpPathFor(target, navigator);
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void DumpPathFor(int target, PathNavigator navigator)
        {
            string path = string.Join("->", navigator.GetPath(target));
            Console.WriteLine(
                $"{target} is at distance {navigator.GetDistance(target)}. Path is {path}");
        }

        private sealed class DijkstraSlimWithDaryHeap : DijkstraAlgorithmSlim
        {
            private readonly int _d;

            public DijkstraSlimWithDaryHeap(IWeighedGraph graph, int d) : base(graph)
            {
                _d = d;
            }

            protected override IHeap<int, int> CreateHeap(IReadOnlyCollection<int> distances)
            {
                return new DHeap<int, int>(_d, Enumerable.Range(0, distances.Count), distances, false);
            }
        }
    }
}