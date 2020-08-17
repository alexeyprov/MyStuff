using System;
using System.Diagnostics;

using Algo.DynamicProgramming.Entities;
using Algo.Trees.Entities;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal sealed class Solution
    {
        public Solution(FrugalMatrix<int> roots, decimal expectedValue)
        {
            Roots = roots ?? throw new ArgumentNullException(nameof(roots));
            ExpectedValue = expectedValue > 0.0M ? 
                expectedValue : 
                throw new ArgumentOutOfRangeException(nameof(expectedValue));    
        }

        public FrugalMatrix<int> Roots { get; }

        public decimal ExpectedValue { get; }

        public void Print()
        {
            Console.WriteLine($"Search expected value: {ExpectedValue}");
            BinaryTreeNode<string> tree = BuildTree(1, Roots.Size);
            tree.Print();
        }

        private BinaryTreeNode<string> BuildTree(int startIndex, int endIndex)
        {
            int rootIndex = Roots[startIndex, endIndex];
            Debug.Assert(rootIndex >= startIndex);
            Debug.Assert(rootIndex <= endIndex);
            Debug.Assert(startIndex < endIndex || (startIndex == rootIndex && endIndex == rootIndex));

            BinaryTreeNode<string> left = rootIndex == startIndex ?
                new BinaryTreeNode<string>($"d{startIndex - 1}") :
                BuildTree(startIndex, rootIndex - 1);
            BinaryTreeNode<string> right = rootIndex == endIndex ?
                new BinaryTreeNode<string>($"d{endIndex}") :
                BuildTree(rootIndex + 1, endIndex);
            return new BinaryTreeNode<string>($"k{rootIndex}")
            {
                Left = left,
                Right = right
            };
        }
    }
}