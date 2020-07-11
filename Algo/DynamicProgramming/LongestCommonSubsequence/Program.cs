using System;
using System.Collections.Generic;

namespace LongestCommonSubsequence
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Random problem:");
            Solve(Problem<int>.CreateRandom(d => (int)(d * 10), 20));

            Console.WriteLine("Well-known problem:");
            Problem<char> wellKnownProblem = new Problem<char>(
                "ACCGGTCGAGTGCGCGGAAGCCGGCCGAA".ToCharArray(),
                "GTCGTTCGGAATGCCGTTGCTCTGTAAA".ToCharArray());
            Solve(wellKnownProblem);
        }

        private static void Solve<T>(Problem<T> problem)
        {
            Solver<T> solver = new Solver<T>();
            IEnumerable<T> solution = solver.FindSolution(problem);
            PrintSolution(problem, solution);
        }

        private static void PrintSolution<T>(Problem<T> problem, IEnumerable<T> solution)
        {
            PrintSequence("First sequence", problem.FirstSequence);
            PrintSequence("Second sequence", problem.SecondSequence);
            PrintSequence("Longest common subsequence", solution);
        }

        private static void PrintSequence<T>(string name, IEnumerable<T> sequence)
        {
            string combinedSequence = string.Join(", ", sequence);
            Console.WriteLine($"{name}: {combinedSequence}");
        }
    }
}
