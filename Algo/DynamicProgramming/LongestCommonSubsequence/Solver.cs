using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LongestCommonSubsequence
{
    /// <summary>
    /// Given two sequences X[1..m] and Y[1..n],
    /// the maximum length of their common subsequence L(m, n) is defined as: 
    /// following recurrent formula:
    ///           { 1 + L(m - 1, n - 1), if X[m] = Y[n]            }
    /// L(m, n) = { max(L(m, n - 1), L(m - 1, n)), if X[m] != Y[n] }
    ///           {                                                }
    /// </summary>
    public sealed class Solver<T>
    {
        private readonly IEqualityComparer<T> _comparer;

        public Solver(IEqualityComparer<T> comparer = null)
        {
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public IEnumerable<T> FindSolution(Problem<T> problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            int m = problem.FirstSequence.Count,
                n = problem.SecondSequence.Count;
            int[,] lengthMatrix = new int[m, n];
            PreviousPoint[,] pathMatrix = new PreviousPoint[m, n];

            int i, j;
            for (i = 0; i < m; ++i)
            {
                T x = problem.FirstSequence[i];

                for (j = 0; j < n; ++j)
                {
                    T y = problem.SecondSequence[j];
                    if (_comparer.Equals(x, y))
                    {
                        int previousLength = i == 0 || j == 0 ? 0 : lengthMatrix[i - 1, j - 1];
                        lengthMatrix[i, j] = previousLength + 1;
                        pathMatrix[i, j] = PreviousPoint.Diagonal;
                    }
                    else
                    {
                        int topLength = i == 0 ? 0 : lengthMatrix[i - 1, j],
                            leftLength = j == 0 ? 0 : lengthMatrix[i, j - 1];
                        if (topLength >= leftLength)
                        {
                            pathMatrix[i, j] = PreviousPoint.Top;
                            lengthMatrix[i, j] = topLength;
                        }
                        else
                        {
                            pathMatrix[i, j] = PreviousPoint.Left;
                            lengthMatrix[i, j] = leftLength;
                        }
                    }
                }
            }

            i = m - 1;
            j = n - 1;
            int len = lengthMatrix[m - 1, n - 1];
            Stack<T> solution = new Stack<T>(len);
            while (i >= 0 && j >= 0)
            {
                PreviousPoint predecessor = pathMatrix[i, j];
                switch (predecessor)
                {
                    case PreviousPoint.Diagonal:
                        solution.Push(problem.FirstSequence[i--]);
                        --j;
                        break;

                    case PreviousPoint.Top:
                        --i;
                        break;

                    case PreviousPoint.Left:
                        --j;
                        break;

                    default:
                        Debug.Fail("Unexpected predecessor");
                        break;
                }
            }

            return solution;
        }

        private enum PreviousPoint : byte
        {
            None = 0,
            Top = 1,
            Left = 2,
            Diagonal = 3
        }
    }
}           