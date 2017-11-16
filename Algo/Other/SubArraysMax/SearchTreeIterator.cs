using System;
using System.Collections.Generic;

using Algo.Trees.SearchTrees;

namespace Algo.Other.SubArraysMax
{
    internal sealed class SearchTreeIterator : IArrayIterator
    {
        IReadOnlyList<int> IArrayIterator.FindMaximums(IReadOnlyList<int> array, int chunkSize)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (chunkSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize));
            }

            int[] maximums = new int[array.Count - chunkSize + 1];
            ISearchTree<int> tree = new AvlTree<int>();
            for (int index = 0; index < chunkSize; ++index)
            {
                tree.Add(array[index]);
            }

            for (int index = 0; index < maximums.Length; ++index)
            {
                maximums[index] = tree.Max();
                tree.Remove(array[index]);
                if (index + chunkSize < array.Count)
                {
                    tree.Add(array[index + chunkSize]);
                }
            }

            return maximums;
        }
    }
}