using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Cashier
{
    static class Program
    {
        private static readonly IReadOnlyList<int> _nominations;

        static Program()
        {
#if TROUBLESHOOTING
            _nominations = new int[] { 2, 3, 4 };
#else
            Random random = new Random();
            int size = random.Next(2, 6); // 2 to 5 different nominations

            int[] nominations = new int[size];
            for (int index = 0; index < size; ++index)
            {
                int cap, floor;

                if (index == 0)
                {
                    floor = 1;
                    cap = 4;
                }
                else
                {
                    floor = nominations[index - 1] + 1;
                    cap = floor * 2;
                }

                nominations[index] = random.Next(floor, cap + 1);
            }

            _nominations = nominations;
#endif
        }

        private static void Main()
        {
#if TROUBLESHOOTING
            Problem problem = new Problem(
                new []
                {
                    new Slot
                    {
                        Nomination = 2,
                        Count = 22
                    },
                    new Slot
                    {
                        Nomination = 3,
                        Count = 20
                    },
                    new Slot
                    {
                        Nomination = 4,
                        Count = 13
                    }
                },
                92);
#else
            Problem problem = new Problem(_nominations);
#endif
            Console.WriteLine(problem);

            UseSolver(new RecursiveSolver(problem.Register), problem);
            UseSolver(new DynamicSolver(problem.Register), problem);
        }

        private static void UseSolver(ISolver solver, Problem problem)
        {
            Console.WriteLine($"Using {solver.GetType()}");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            IEnumerable<int> solution = solver.FindSolution(problem.Goal);
            if (VerifySolution(problem, solution))
            {
                PrintSolution(problem.Goal, solution);
            }
            else
            {
                Console.WriteLine("Solution is invalid");

#if TROUBLESHOOTING
                if (solution != null)
                {
                    PrintSolution(problem.Goal, solution);
                }
#endif
            }

            stopWatch.Stop();
            Console.WriteLine($"Using {solver.GetType()}: done in {stopWatch.ElapsedMilliseconds} ms");
        }

        private static bool VerifySolution(Problem problem, IEnumerable<int> solution)
        {
            if (solution == null)
            {
                return true;
            }

            int sum = 0;
            foreach (var part in problem.Register.Zip(
                solution,
                (s, q) => new
                {
                    s.Nomination,
                    Allowed = s.Count,
                    Used = q
                }))
            {
                if (part.Used > part.Allowed)
                {
#if TROUBLESHOOTING
                    Console.WriteLine(
                        $"Validation error: {part.Used} coins of nomination {part.Nomination} used ({part.Allowed} allowed)");
#endif
                    return false;
                }

                sum += part.Nomination * part.Used;
            }

#if TROUBLESHOOTING
            if (sum != problem.Goal)
            {
                Console.WriteLine(
                    $"Validation error: solution total is {sum}, but goal is {problem.Goal}");
                return false;
            }
            else
            {
                return true;
            }
#else
            return sum == problem.Goal;
#endif
        }

        private static void PrintSolution(int goal, IEnumerable<int> solution)
        {
            if (solution == null)
            {
                Console.WriteLine("Unable to find solution");
            }
            else
            {
                Console.WriteLine(
                    "{0} = {1}",
                    goal,
                    string.Join(
                        " + ",
                        _nominations.Zip(
                            solution,
                            (n, s) => $"{s}*{n}")));
            }
        }
    }
}
