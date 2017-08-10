using System;
using System.Collections.Generic;
using System.Linq;

namespace ContiguousSequences
{
    /// <summary>
    /// Searches for contiguous sub-sequences with maximum sum in an array a[1..n]
    /// Solution is given by the following recurrent equation
    ///        { a[j], if S[j-1] < 0
    /// S[j] = {
    ///        { S[j-1] + a[j], otherwise
    /// </summary>
    static class Program
    {
        private static readonly IReadOnlyList<int> _problem;

        static Program()
        {
            Random random = new Random();
            int size = 5 + random.Next(10);

            int[] problem = new int[size];

            for (int index = 0; index < size; ++index)
            {
                problem[index] = 50 - random.Next(101);
            }

            _problem = problem;
        }

        private static void Main()
        {
            int[] sums = new int[_problem.Count];
            int[] startIndexes = new int[_problem.Count];

            sums[0] = _problem[0];
            startIndexes[0] = 0;

            // pass 1: fill the arrays
            int lastStartIndex = 0;
            for (int index = 1; index < _problem.Count; ++index)
            {
                int lastSum = sums[index - 1];

                if (lastSum >= 0)
                {
                    sums[index] = lastSum + _problem[index];
                }
                else
                {
                    // restart at this index
                    sums[index] = _problem[index];
                    lastStartIndex = index;
                }

                startIndexes[index] = lastStartIndex;
            }

            // pass 2: discover the solution
            int startIndex = 0;
            int endIndex = 0;
            int localMax = int.MinValue;
            for (int index = 0; index < _problem.Count; ++index)
            {
                if (sums[index] > localMax)
                {
                    startIndex = startIndexes[index];
                    endIndex = index;
                    localMax = sums[index];
                }
            }

            PrintSolution(startIndex, endIndex, localMax);
        }

        private static void PrintSolution(int startIndex, int endIndex, int sum)
        {
            Console.WriteLine($"Problem: {string.Join(" ", _problem)}");
            Console.WriteLine($"Solution: {sum} = {string.Join(" + ", _problem.Skip(startIndex).Take(endIndex - startIndex + 1))}");
        }
    }
}