using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Trees.Entities;
using Algo.Heaps.Entities;

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

        public BinaryTreeNode<T> Root => _root;

        private static BinaryTreeNode<T> BuildTree(IReadOnlyDictionary<T, double> stats)
        {
            IPriorityQueue<double, BinaryTreeNode<T>> priorityQueue = new BinaryHeapPriorityQueue<double, BinaryTreeNode<T>>(
                stats.Values,
                stats.Keys.Select(k => new BinaryTreeNode<T>(k)),
                false);
            while (priorityQueue.Count > 1)
            {
                BinaryTreeNode<T> left = priorityQueue.Extract(),
                                  right = priorityQueue.Extract();
                BinaryTreeNode<T> newNode = new BinaryTreeNode<T>()
                {
                    Left = left,
                    Right = right
                };
                priorityQueue.Add(stats[left.Data] + stats[right.Data], newNode);
            }

            return priorityQueue.Extract();
        }
    }
}