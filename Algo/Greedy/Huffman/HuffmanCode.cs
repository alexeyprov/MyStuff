using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Trees.Entities;
using Algo.Heaps.Entities;

namespace Algo.Greedy.Huffman
{
    public sealed class HuffmanCode<T>
    {
        public HuffmanCode(IReadOnlyDictionary<T, double> stats, T eofMarker = default)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats));
            }

            Root = BuildTree(stats, eofMarker);
            EofMarker = eofMarker;
        }

        public BinaryTreeNode<T> Root { get; }

        public T EofMarker { get; }

        private static BinaryTreeNode<T> BuildTree(IReadOnlyDictionary<T, double> stats, T eofMarker)
        {
            IPriorityQueue<double, BinaryTreeNode<T>> priorityQueue = new BinaryHeapPriorityQueue<double, BinaryTreeNode<T>>(
                stats.Values,
                stats.Keys.Select(k => new BinaryTreeNode<T>(k)),
                false);
            if (eofMarker != null)
            {
                priorityQueue.Add(0.0D, new BinaryTreeNode<T>(eofMarker));
            }

            while (priorityQueue.Count > 1)
            {
                (double w1, BinaryTreeNode<T> left) = priorityQueue.Extract();
                (double w2, BinaryTreeNode<T> right) = priorityQueue.Extract();
                BinaryTreeNode<T> newNode = new BinaryTreeNode<T>()
                {
                    Left = left,
                    Right = right
                };
                priorityQueue.Add(w1 + w2, newNode);
            }

            (_, BinaryTreeNode<T> root) = priorityQueue.Extract();
            return root;
        }
    }
}