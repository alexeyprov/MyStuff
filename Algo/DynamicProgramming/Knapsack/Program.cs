using System;
using System.Collections.Generic;

namespace Algo.DynamicProgramming.Knapsack
{
    internal static class Program
    {
        private static void Main()
        {
            Problem smallWellKnown = new Problem(
                50,
                new Item(60, 10),
                new Item(100, 20),
                new Item(120, 30));

            Test(smallWellKnown);

            Problem mediumWellKnown = new Problem(
                67,
                new Item(505, 23),
                new Item(352, 26),
                new Item(458, 20),
                new Item(220, 18),
                new Item(354, 32),
                new Item(414, 27),
                new Item(498, 29),
                new Item(545, 26),
                new Item(473, 30),
                new Item(543, 27)
            );

            Test(mediumWellKnown);

            Problem random = new Problem();

            Test(random);
        }

        private static void Test(Problem problem)
        {
            Console.WriteLine("=====");
            problem.Print();

            Solve<MemoizationSolver>(problem);
            Solve<DynamicSolver>(problem);
        }

        private static void Solve<T>(Problem problem)
            where T : ISolver, new()
        {
            ISolver solver = new T();
            Console.WriteLine($"Solving with {typeof(T)}...");

            IEnumerable<Item> solution = solver.FindSolution(problem);

            int weight = 0,
                value = 0;
            foreach (Item item in solution)
            {
                Console.Write($"{item} ");
                weight += item.Weight;
                value += item.Value;
            }

            Console.WriteLine(
                weight == 0 ? 
                    "No solution" : 
                    $"Total value: {value}, total weight: {weight}");
        }
    }
}
