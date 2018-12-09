using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Algo.Trees.Entities;

namespace Algo.DynamicProgramming.MatrixChainMultiplication
{
    internal sealed class Solution
    {
        private readonly int[,] _pathMatrix;

        public Solution(int[,] pathMatrix, int cost, IReadOnlyList<int> dimensions)
        {
            if (dimensions == null)
            {
                throw new ArgumentNullException(nameof(dimensions));
            }

            _pathMatrix = pathMatrix ?? throw new ArgumentNullException(nameof(pathMatrix));

            int size = pathMatrix.GetLength(0);
            if (dimensions.Count != size + 1)
            {
                throw new ArgumentException(nameof(dimensions));
            }

            Cost = cost;
            Structure = ParseStructure(pathMatrix, 0, size - 1, dimensions);
        }

        public BinaryTreeNode<string> Structure { get; }

        public int Cost { get; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int size = _pathMatrix.GetLength(0);
            PrintResult(0, size - 1);
            return result.ToString();

            void PrintResult(int leftIndex, int rightIndex)
            {
                if (leftIndex == rightIndex)
                {
                    result.Append($"A{leftIndex + 1}");
                }
                else
                {
                    result.Append("(");
                    int cut = _pathMatrix[leftIndex, rightIndex];
                    PrintResult(leftIndex, cut - 1);
                    PrintResult(cut, rightIndex);
                    result.Append(")");
                }
            }
        }

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