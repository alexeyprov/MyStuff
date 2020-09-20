using System;
using System.Collections.Generic;

using Algo.Trees.Entities;

namespace Algo.Greedy.Huffman
{
    public sealed class HuffmanCode<T>
    {
        private readonly BinaryTreeNode<T> _root;

        public HuffmanCode(IReadOnlyDictionary<T, double> stats)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats));
            }

            _root = BuildTree(stats);
        }

        private static BinaryTreeNode<T> BuildTree(IReadOnlyDictionary<T, double> stats)
        {
            throw new NotImplementedException();
        }
    }
}