using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PoissonJugs
{
    internal abstract class BaseSolver : ISolver
    {
        private IList<int> _volumes;
        private int _goal;
        private readonly bool _isTracing;

        protected BaseSolver(bool isTracing = false)
        {
            _isTracing = isTracing;
        }

        IEnumerable<IList<int>> ISolver.FindSolution(IList<int> volumes, int goal)
        {
            VerifyVolumes(volumes);
            _volumes = volumes;

            VerifyGoal(goal);
            _goal = goal;

            IList<int> initialStep = Enumerable.Repeat(0, _volumes.Count).ToArray();
            initialStep[_volumes.Count - 1] = _volumes[_volumes.Count - 1];

            return SeekSolution(initialStep);
        }

        protected IList<int> Volumes => _volumes;

        protected int Goal => _goal;

        protected abstract IEnumerable<IList<int>> SeekSolution(IList<int> initialStep);

        protected IList<int> Pour(IList<int> step, int fromIndex, int toIndex)
        {
            // can pour into target?
            int targetCapacity = _volumes[toIndex] - step[toIndex];

            Debug.Assert(targetCapacity >= 0);

            if (fromIndex == toIndex || targetCapacity == 0)
            {
                return null;
            }

            int amount = Math.Min(step[fromIndex], targetCapacity);
                
            Debug.Assert(amount > 0, "Trying to pour negative volume.");

            IList<int> newStep = step.ToArray();
            newStep[fromIndex] -= amount;
            newStep[toIndex] += amount;

            if (_isTracing)
            {
                Console.WriteLine(string.Join(", ", newStep));
                Console.ReadKey();
            }

            return newStep;
        }

        private static void VerifyVolumes(IList<int> volumes) 
        {
            if (volumes == null)
            {
                throw new ArgumentNullException(nameof(volumes));
            }

            int count = volumes.Count;
            if (count < 3)
            {
                throw new ArgumentException(
                    "Must supply volumes of at least three vessels", 
                    nameof(volumes));
            }

            int sum = 0;
            for (int firstIndex = 0; firstIndex < count - 1; ++firstIndex)
            {
                if (volumes[firstIndex] <= 0)
                {
                    throw new ArgumentException("Volume must be a positive number", nameof(volumes));
                }

                if (volumes[firstIndex] >= volumes[firstIndex + 1])
                {
                    throw new ArgumentException(
                        "Volumes must be sorted in an increasing order",
                        nameof(volumes));
                }

                for (int secondIndex = firstIndex + 1; secondIndex < count - 1; ++secondIndex)
                {
                    if (!AreMutuallyPrime(volumes[firstIndex], volumes[secondIndex]))
                    {
                        throw new ArgumentException(
                            "Volumes must be mutually prime", 
                            nameof(volumes));
                    }
                }

                sum += volumes[firstIndex];
            }

            if (sum != volumes[count - 1])
            {
                throw new ArgumentException(
                    $"Sum of the first {count - 1} volumes must equal to the last vessel's volume",
                    nameof(volumes));
            }
        }

        private static bool AreMutuallyPrime(int a, int b)
        {
            int threshold = (int)Math.Sqrt(Math.Min(a, b));

            for (int divisor = 2; divisor <= threshold; ++divisor)
            {
                if ((a % divisor) == 0 && (b % divisor) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void VerifyGoal(int goal)
        {
            if (goal <= 0)
            {
                throw new ArgumentException("Goal must be positive", nameof(goal));
            }
        }
    }
}