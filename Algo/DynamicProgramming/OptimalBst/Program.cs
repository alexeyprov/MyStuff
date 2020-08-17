using System;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal static class Program
    {
        private static void Main()
        {
            Solve(new Problem(), "Random problem:");

            Console.WriteLine();

            Problem wellKnownProblem = new Problem(
                new[] { 0.15M, 0.1M, 0.05M, 0.1M, 0.2M }, 
                new[] { 0.05M, 0.1M, 0.05M, 0.05M, 0.05M, 0.1M });
            Solve(wellKnownProblem, "Well-known problem:");
        }

        private static void Solve(Problem problem, string description)
        {
            Console.WriteLine(description);
            problem.Print();

            Solver solver = new Solver();
            Solution solution = solver.FindSolution(problem);
            solution.Print();
        }
    }
}
