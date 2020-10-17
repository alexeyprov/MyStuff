using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Greedy.Matroids;

namespace Algo.Greedy.ConsoleTest
{
    internal static class Program
    {
        private static void Main()
        {
            IMatroid<float, UnitTimeActivity> matroid = 
                new UnitTimeActivityMatroid(
                    new UnitTimeActivity(1, 4, 70),
                    new UnitTimeActivity(2, 2, 60),
                    new UnitTimeActivity(3, 4, 50),
                    new UnitTimeActivity(4, 4, 40),
                    new UnitTimeActivity(5, 1, 30),
                    new UnitTimeActivity(6, 4, 20),
                    new UnitTimeActivity(7, 6, 10));

            ISet<UnitTimeActivity> optimumSet = matroid.FindOptimum();
            Console.WriteLine("Optimal set: " + string.Join(", ", optimumSet.Select(a => a.Id).OrderBy(i => i)));
        }
    }
}
