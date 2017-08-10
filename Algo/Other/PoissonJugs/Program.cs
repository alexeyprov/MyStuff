using System;
using System.Collections.Generic;
using System.Linq;

namespace PoissonJugs
{
    static class Program
    {
        private const int SoughtVolume = 6;
        
        private static readonly int[] _jugVolumes;

        static Program()
        {
            _jugVolumes = new int[] { 5, 8, 13 }; // ordered ascending
        }

        private static void Main()
        {
            Run(new BruteForceSolver());
            Run(new ScientificSolver());    
        }

        private static void Run(ISolver solver)
        {
            Console.WriteLine($"Solving with {solver.GetType()}:");

            IEnumerable<IList<int>> solution = solver.FindSolution(_jugVolumes, SoughtVolume);

            if (solution != null)
            {
                foreach (IList<int> step in solution)
                {
                    Console.WriteLine(string.Join(", ", step));
                }
            }
            else
            {
                Console.WriteLine("Unable to find solution.");
            }
        }
    }
}                 