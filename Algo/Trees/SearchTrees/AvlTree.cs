using System;
using System.Collections.Generic;

using Algo.Trees.SearchTrees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class AvlTree<T> : BinarySearchTree<T, AvlTreeNode<T>>
        where T : IComparable<T>
    {
        public AvlTree(IComparer<T> comparer = null) : base(comparer)
        {
        }
    }
}