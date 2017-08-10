using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotels
{
    internal sealed class DynamicSolver : ISolver
    {
        private readonly bool _isTroubleshooting;

        public DynamicSolver(bool isTroubleshooting)
        {
            _isTroubleshooting = isTroubleshooting;
        }

        int ISolver.FindSolution(int[] hotels, int idealDistance)
        {
            IList<int> penalties = new int[hotels.Length];
            IList<int> predecessors = Enumerable.Repeat(-1, hotels.Length).ToArray();

            for (int currentIndex = 0, day = 0; currentIndex < hotels.Length; ++currentIndex, ++day)
            {
                int minPenalty = GetPenalty(idealDistance, hotels[currentIndex]);
                int usedHotel = -1;
                for (int previousIndex = 0; previousIndex < currentIndex; ++previousIndex)
                {
                    int penalty = penalties[previousIndex] + 
                        GetPenalty(idealDistance, hotels[currentIndex] - hotels[previousIndex]);
                    if (penalty < minPenalty)
                    {
                        minPenalty = penalty;
                        usedHotel = previousIndex;
                    }
                }

                penalties[currentIndex] = minPenalty;
                predecessors[currentIndex] = usedHotel;
            }

            if (_isTroubleshooting)
            {
                PrintRoute(hotels, penalties, predecessors, idealDistance);
            }

            return penalties[hotels.Length - 1];
        }

        private static int GetPenalty(int idealDistance, int realDistance)
        {
            int delta = realDistance - idealDistance;
            return delta * delta;
        }

        private static void PrintRoute(
            IList<int> hotels, 
            IList<int> penalties, 
            IList<int> predecessors,
            int idealDistance)
        {
            Stack<string> steps = new Stack<string>();

            for (int index = hotels.Count - 1; index >= 0; index = predecessors[index])
            {
                int previousIndex = predecessors[index];
                int startPoint = 0;
                int penalty = penalties[index];
                if (previousIndex != -1)
                {
                    startPoint = hotels[previousIndex];
                    penalty -= penalties[previousIndex];
                }

                steps.Push($">>>>Distance is {hotels[index] - startPoint - idealDistance}; incurred penalty is {penalty}.");
                steps.Push($"staying at hotel #{index}, {hotels[index]} mi from the start");
            }

            int day = 0;
            foreach (string step in steps)     
            {
                Console.WriteLine(
                    step.StartsWith(">>>>") ?
                    step :
                    $"Day {day++}: {step}");
            }
        }
    }
}