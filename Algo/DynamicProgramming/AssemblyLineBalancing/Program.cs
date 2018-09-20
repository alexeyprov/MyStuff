using System;
using System.Linq;

namespace Algo.DynamicProgramming.AssemblyLineBalancing
{
    internal static class Program
    {
        private const int OutputWidth = 4;

        private static void Main(string[] args)
        {
            Console.WriteLine("Solving random problem:");
            Problem randomProblem = Problem.CreateRandom();
            SolveProblem(randomProblem);

            Console.WriteLine("Solving example problem:");
            Problem exampleProblem = GetExampleProblem();
            SolveProblem(exampleProblem);
        }

        private static void SolveProblem(Problem problem)
        {
            OutputProblem(problem);

            Solver solver = new Solver();
            Solution solution = solver.FindSolution(problem);

            OutputSolution(solution);
        }

        private static void OutputProblem(Problem problem)
        {
            PrintCostLine(problem, 0);
            PrintTransferLine(problem, 0);
            PrintTransferLine(problem, 1);
            PrintCostLine(problem, 1);
        }

        private static void OutputSolution(Solution solution)
        {
            Console.WriteLine("Costs:");

            PrintArrayRow(solution.Costs, 0, 1);
            PrintArrayRow(solution.Costs, 1, 1);

            PrintArrayRow(solution.Paths, 0, 2, x => (byte)(x + 1));
            PrintArrayRow(solution.Paths, 1, 2, x => (byte)(x + 1));

            Console.WriteLine("Optimal path (line/cost): " +
                string.Join(" ", solution.GetOptimalPath().Select(p => $"({p.line + 1}, {p.cost})")));
        }

        private static void PrintArrayRow<T>(
            T[,] array, 
            int rowIndex, 
            int offset,
            Func<T, T> transform = null)
        {
            transform = transform ?? (x => x);

            Console.Write(new string(' ', offset * OutputWidth));
            for (int colIndex = array.GetLowerBound(1), max = array.GetUpperBound(1); colIndex <= max; ++colIndex)
            {
                Console.Write($"{transform(array[rowIndex, colIndex]), OutputWidth}");
            }

            Console.WriteLine();
        }

        private static void PrintCostLine(Problem problem, byte lineIndex)
        {
            Console.Write($"{problem.EntryCosts[lineIndex], OutputWidth}");
            for (int stageIndex = 0; stageIndex < problem.Size; ++stageIndex)
            {
                Console.Write($"{problem.StageCosts[lineIndex, stageIndex], OutputWidth}");
            }

            Console.WriteLine($"{problem.ExitCosts[lineIndex], OutputWidth}");
        }

        private static void PrintTransferLine(Problem problem, byte lineIndex)
        {
            Console.Write(new string(' ', 2 * OutputWidth));
            for (int stageIndex = 1; stageIndex < problem.Size; ++stageIndex)
            {
                Console.Write($"{problem.TransferCosts[lineIndex, stageIndex], OutputWidth}");
            }

            Console.WriteLine();
        }

        private static Problem GetExampleProblem()
        {
            return new Problem(
                new int[] { 2, 4 },
                new int[2, 6]
                {
                    { 7, 9, 3, 4, 8, 4 },
                    { 8, 5, 6, 4, 5, 7 }
                },
                new int[] { 3, 2 },
                new int[2, 5]
                {
                    { 2, 1, 2, 2, 1 },
                    { 2, 3, 1, 3, 4 }
                });
        }
    }
}
