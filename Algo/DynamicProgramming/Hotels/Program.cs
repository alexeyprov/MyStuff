using System;
using System.Collections.Generic;

namespace Hotels
{
    static class Program
    {
        private const int DailyDistance = 200;

        private static readonly int[] _hotels;

        static Program()
        {
            Random random = new Random();
            int size = 5 + random.Next(10);
            _hotels = new int[size];

            int lastHotel = 0;
            for (int index = 0; index < size; ++index)
            {
                lastHotel += DailyDistance / 4 + random.Next(3 * DailyDistance / 2);
                _hotels[index] = lastHotel;
            }
        }

        private static void Main(string[] args)
        {
            bool isTroubleshooting = ParseCommandLine(args);
            int[] hotels = _hotels;

            if (isTroubleshooting)
            {
                //hotels = new int[] { 98, 162, 423, 500, 553, 758, 967, 1194 };
                hotels = new int[] { 58, 362, 610, 954, 1135, 1268, 1520, 1763, 1880, 2077, 2344, 2423, 2656 };
            }

            Console.WriteLine($"Problem: {string.Join(" ", hotels)}");

            TrySolver(new IterativeSolver(isTroubleshooting), hotels);
            TrySolver(new DynamicSolver(isTroubleshooting), hotels);
        }

        private static void TrySolver(ISolver solver, int[] hotels)
        {
            Console.WriteLine($"Using {solver.GetType().Name}:");
            Console.WriteLine($"\tMinimal penalty is {solver.FindSolution(hotels, DailyDistance)}");
        }

        private static bool ParseCommandLine(string[] args)
        {
            return args.Length == 1 && args[0].ToUpper() == "/T";
        }
    }
}