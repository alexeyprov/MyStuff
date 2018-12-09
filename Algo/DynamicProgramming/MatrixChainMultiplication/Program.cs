using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Trees.Entities;

namespace Algo.DynamicProgramming.MatrixChainMultiplication
{
    internal static class Program
    {
        private static void Main()
        {
            SolveProblem(new int[] { 10, 100, 5, 50 });

            Random random = new Random();
            int size = random.Next(4, 10);

            SolveProblem(Enumerable.Range(0, size).Select(_ => random.Next(2, 101)));
        }

        private static void SolveProblem(IEnumerable<int> problem)
        {
            IReadOnlyList<int> dimensions = problem as IReadOnlyList<int> ??
                problem.ToArray();

            string matrixChain = string.Join(
                " * ",
                dimensions.Skip(1).Select((d, i) => $"{dimensions[i]}x{dimensions[i+1]}"));
                
            Solver solver = new Solver();
            Solution solution = solver.FindSolution(dimensions);

            Console.WriteLine($"Multiplication cost for {matrixChain} is {solution.Cost}: {solution}. Solution tree is:");
            solution.Structure.Print();
        }
    }
}
