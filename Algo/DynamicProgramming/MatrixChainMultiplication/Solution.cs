using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algo.Trees.Entities;

namespace Algo.DynamicProgramming.MatrixChainMultiplication
{
    internal sealed class Solution
    {
        public Solution(int[,] pathMatrix, int cost, IReadOnlyList<int> dimensions)
        {
            if (dimensions == null)
            {
                throw new ArgumentNullException(nameof(dimensions));
            }

            int size = pathMatrix?.GetLength(0) ??
                throw new ArgumentNullException(nameof(pathMatrix));

            if (dimensions.Count != size + 1)
            {
                throw new ArgumentException(nameof(dimensions));
            }

            Cost = cost;
            Structure = ParseStructure(pathMatrix, 0, size - 1, dimensions);
        }

        public BinaryTreeNode<string> Structure { get; }

        public int Cost { get; }

        private static BinaryTreeNode<string> ParseStructure(
            int[,] pathMatrix, 
            int leftIndex, 
            int rightIndex,
            IReadOnlyList<int> dimensions)
        {
            if (leftIndex == rightIndex)
            {
                return new BinaryTreeNode<string>($"{dimensions[leftIndex]}x{dimensions[leftIndex + 1]}");
            }

            if (leftIndex > rightIndex)
            {
                return null;
            }

            int cut = pathMatrix[leftIndex, rightIndex];
            Debug.Assert(cut > leftIndex);
            Debug.Assert(cut <= rightIndex);

            return new BinaryTreeNode<string>("*")
            {
                Left = ParseStructure(pathMatrix, leftIndex, cut - 1, dimensions),
                Right = ParseStructure(pathMatrix, cut, rightIndex, dimensions)
            };
        }
    }
}