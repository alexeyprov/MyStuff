using System;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Random problem:");
            Solve(new Problem());

            Console.WriteLine("Well-known problem:");
            Problem wellKnownProblem = new Problem(null, null);
            Solve(wellKnownProblem);
        }

        private static void Solve(Problem problem)
        {
            Solver solver = new Solver();
            Solution solution = solver.FindSolution(problem);
            solution.Print();
        }
    }
}
