using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.AssemblyLineBalancing
{
    internal sealed class Problem
    {
        private readonly int[] _entryCosts;
        private readonly int[] _exitCosts;

        public Problem(
            IReadOnlyCollection<int> entryCosts,
            int[,] stageCosts,
            IReadOnlyCollection<int> exitCosts,
            int[,] transferCosts) :
                this(stageCosts?.GetLength(1) ?? throw new ArgumentNullException(nameof(stageCosts)))
        {
            _entryCosts = CheckVector(entryCosts);
            _exitCosts = CheckVector(exitCosts);

            LoadMatrix(stageCosts, StageCosts);

            if (transferCosts == null)
            {
                throw new ArgumentNullException(nameof(transferCosts));
            }

            LoadMatrix(transferCosts, TransferCosts);
        }

        public Problem(int size)
        {
            Size = size >= 2 ? size : throw new ArgumentOutOfRangeException(nameof(size));
            StageCosts = new int[2, size];
            TransferCosts = (int[,])Array.CreateInstance(typeof(int), new[] {2, size - 1}, new[] { 0, 1 });
            _entryCosts = new int[2];
            _exitCosts = new int[2];
        }

        public int Size { get; }

        public IReadOnlyList<int> EntryCosts => _entryCosts;

        public IReadOnlyList<int> ExitCosts => _exitCosts;

        /// <summary>
        /// Gets costs of line stages, where row index stands for assembly line and
        /// column index stands for stage.
        /// </summary>
        public int[,] StageCosts { get; }

        /// <summary>
        /// Gets cost of transfer between assembly lines, where row index stands for 
        /// destination assembly line and column index defines destination stage.
        /// </summary>
        public int[,] TransferCosts { get; }

        public static Problem CreateRandom(int maxSize = 10, int maxCost = 99)
        {
            if (maxSize < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(maxSize));
            }

            Random random = new Random();
            int size = random.Next(2, maxSize);
            Problem problem = new Problem(size);
            for (int rowIndex = 0; rowIndex < 2; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex < size; ++columnIndex)                
                {
                    problem.StageCosts[rowIndex, columnIndex] = random.Next(maxCost + 1);
                    if (columnIndex > 0)
                    {
                        problem.TransferCosts[rowIndex, columnIndex] = random.Next(maxCost + 1);
                    }
                }

                problem._entryCosts[rowIndex] = random.Next(maxCost + 1);
                problem._exitCosts[rowIndex] = random.Next(maxCost + 1);
            }

            return problem;
        }

        private static int[] CheckVector(IReadOnlyCollection<int> vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector));
            }

            if (vector.Count != 2)
            {
                throw new ArgumentException(nameof(vector));
            }

            return vector.ToArray();
        }

        private static void LoadMatrix(int[,] source, int[,] destination)
        {
            int rowCount = source.GetLength(0);
            int columnCount = source.GetLength(1);
            if (rowCount != destination.GetLength(0) ||
                columnCount != destination.GetLength(1))
            {
                throw new ArgumentException(nameof(source));
            }

            int startColumnIndex = destination.GetLowerBound(1);
            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                for (int columnIndex = 0; columnIndex < columnCount; ++columnIndex)
                {
                    destination[rowIndex, startColumnIndex + columnIndex] = source[rowIndex, columnIndex];
                }
            }
        }
    }
}