using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;
using Algo.Graphs.DepthFirstSearch;

namespace Algo.Graphs.TestConsole
{
    internal sealed class DfsTests
    {
        private readonly IGraph _simpleLoop;
        private readonly IGraph _triangleNoLoop;

        private readonly IGraph _figure37asIs;
        private readonly IGraph _figure37noLoops;
        private readonly IGraph _figure38;
        private readonly IGraph _figure39;

        internal DfsTests()
        {
            _simpleLoop = CreateGraph(
                new int[][]
                {
                    new int[] { 1 },
                    new int[] { 2 },
                    new int[] { 0 }
                });
            _triangleNoLoop = CreateGraph(
                new int[][]
                {
                    new int[] { 1, 2 },
                    new int[] { 2 },
                    new int[] { }
                });

            _figure37asIs = CreateGraph(
                new int[][]
                {
                    new int[] { 1, 2, 5 },
                    new int[] { 4 },
                    new int[] { 3 },
                    new int[] { 0, 7 },
                    new int[] { 5, 6, 7 },
                    new int[] { 1, 6 },
                    new int[] { },
                    new int[] { 6 }
                });
            _figure37noLoops = CreateGraph(
                new int[][]
                {
                    new int[] { 1, 2, 5 },
                    new int[] { 4 },
                    new int[] { 3 },
                    new int[] { 7 },
                    new int[] { 5, 6, 7 },
                    new int[] { 6 },
                    new int[] { },
                    new int[] { 6 }
                });
            _figure38 = CreateGraph(
                new int[][]
                {
                    new int[] { 2 },
                    new int[] { 0, 3 },
                    new int[] { 4, 5 },
                    new int[] { 2 },
                    new int[] { },
                    new int[] { } 
                });
            _figure39 = CreateGraph(
                new int[][]
                {
                    new int[] { 1 },
                    new int[] { 2, 3, 4 },
                    new int[] { 5 },
                    new int[] { },
                    new int[] { 1, 5, 6 },
                    new int[] { 2, 7 },
                    new int[] { 7, 9 },
                    new int[] { 10 },
                    new int[] { 6 },
                    new int[] { 8 },
                    new int[] { 11 },
                    new int[] { 9 }
                });
        }

        internal void Linearize()
        {
            Console.WriteLine("========== Linearize ==========");
            RunSortTest(_figure38);
            RunSortTest(_figure37noLoops);
        }

        internal void SearchForCycles()
        {
            Console.WriteLine("========== Search for cycles ==========");
            RunCycleTest(_simpleLoop);
            RunCycleTest(_triangleNoLoop);
            RunCycleTest(_figure37asIs);
        }

        internal void Decompose()
        {
            Console.WriteLine("========== Decompose ==========");
            RunDecompositionTest(_figure37asIs);
            RunDecompositionTest(_figure39);
        }

        private static void RunCycleTest(IGraph graph)
        {
            CycleDetector algo = new CycleDetector(graph);
            algo.Run();
            if (!algo.HasCycles)
            {
                Console.WriteLine("No cycles");
                return;
            }

            Console.WriteLine(
                $"Cycle(s) detected. Backward edge(s): {string.Join("; ", algo.BackwardEdges)}");
        }

        private static void RunSortTest(IGraph graph)
        {
            TopologicalSort algo = new TopologicalSort(graph);
            algo.Run();
            IEnumerable<int> sortedGraph = algo.SortedGraph;

            if (sortedGraph == null)
            {
                Console.WriteLine("No sort order");
                return;
            }

            Console.WriteLine($"Sorted graph: {string.Join("; ", sortedGraph)}");
        }

        private static void RunDecompositionTest(IGraph graph)
        {
            DirectedGraphDecomposer algo = new DirectedGraphDecomposer(graph);
            algo.Run();
            IEnumerable<IEnumerable<int>> components = algo.Components;

            if (components == null)
            {
                Console.WriteLine("Unable to decompose");
                return;
            }

            Console.WriteLine(
                $"Graph components: {string.Join("; ", components.Select(FormatComponent))}");
        }

        private static string FormatComponent(IEnumerable<int> component) =>
            $"({string.Join(",", component)})";

        private static IGraph CreateGraph(int[][] links) => new AdjacencyListGraph(links);
    }
}
