using System;
using System.Collections.Generic;
using System.Diagnostics;

using Algo.Trees.Entities;
using Algo.Trees.SearchTrees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class RedBlackTree<T> : BinarySearchTree<T, RedBlackTreeNode<T>>
        where T : IComparable<T>
    {
        public RedBlackTree(IComparer<T> comparer = null) : base(comparer)
        {
        }

        public RedBlackTree(RedBlackTree<T> tree) : 
            this((tree?? throw new ArgumentNullException(nameof(tree))).Comparer)
        {
            Count = tree.Count;
            Height = tree.Height;
            Root = new RedBlackTreeNode<T>(tree.Root);
        }

        public int Height { get; private set; }

        public override void Clear()
        {
            base.Clear();
            Height = 0;
        }

        public RedBlackTree<T> Union(RedBlackTree<T> other) =>
            UnionWith(other, default(T), false);

        public RedBlackTree<T> Union(RedBlackTree<T> other, T median) =>
            UnionWith(other, median, true);

        protected override bool AddNode(RedBlackTreeNode<T> node)
        {
            if (!base.AddNode(node))
            {
                return false;
            }

            node.IsRed = true;
            InsertFixup(node);
            return true;
        }

        protected override RedBlackTreeNode<T> RemoveNode(
            RedBlackTreeNode<T> node, 
            RedBlackTreeNode<T> parent)
        {
            RedBlackTreeNode<T> deleted = base.RemoveNode(node, parent);
            parent = deleted.Parent;
            if (deleted.IsRed)
            {
                return deleted;
            }

            Debug.Assert(deleted.Left == null || deleted.Right == null);
            RedBlackTreeNode<T> current = deleted.Left ?? deleted.Right;

            while (current?.IsRed != true && current != Root)
            {
                Debug.Assert(parent != null);

                if (parent.Left == current)
                {
                    RedBlackTreeNode<T> brother = parent.Right;
                    Debug.Assert(brother != null);

                    if (brother.IsRed)
                    {
                        RotateLeft(brother);
                        parent.IsRed = true;
                        brother.IsRed = false;
                        brother = parent.Right;
                    }

                    if (brother.Right?.IsRed != true && brother.Left?.IsRed != true)
                    {
                        brother.IsRed = true;
                        current = parent;
                    }
                    else
                    {
                        if (brother.Right?.IsRed != true)
                        {
                            Debug.Assert(brother.Left.IsRed);
                            RotateRight(brother.Left);
                            brother.IsRed = true;
                            brother.Parent.IsRed = false;
                            brother = parent.Right;
                        }

                        Debug.Assert(brother.Right.IsRed);
                        RotateLeft(brother);
                        brother.IsRed = parent.IsRed;
                        parent.IsRed = false;
                        brother.Right.IsRed = false;

                        // double black color is pushed to a red node, can exit now
                        current = Root;
                    }
                }
                else
                {
                    RedBlackTreeNode<T> brother = parent.Left;
                    Debug.Assert(brother != null);

                    if (brother.IsRed)
                    {
                        RotateRight(brother);
                        parent.IsRed = true;
                        brother.IsRed = false;
                        brother = parent.Left;
                    }

                    if (brother.Right?.IsRed != true && brother.Left?.IsRed != true)
                    {
                        brother.IsRed = true;
                        current = parent;
                    }
                    else
                    {
                        if (brother.Left?.IsRed != true)
                        {
                            Debug.Assert(brother.Right.IsRed);
                            RotateLeft(brother.Right);
                            brother.IsRed = true;
                            brother.Parent.IsRed = false;
                            brother = parent.Left;
                        }

                        Debug.Assert(brother.Left.IsRed);
                        RotateRight(brother);
                        brother.IsRed = parent.IsRed;
                        parent.IsRed = false;
                        brother.Left.IsRed = false;

                        // double black color is pushed to a red node, can exit now
                        current = Root;
                    }
                }

                parent = current.Parent;
            }

            if (current == Root && current?.IsRed != true)
            {
                Height--;
            }

            if (current != null)
            {
                current.IsRed = false;
            }

            return deleted;
        }

        private static RedBlackTree<T> Merge(
            RedBlackTree<T> source,
            RedBlackTree<T> destination,
            T medianValue,
            bool isLeftSideMerge)
        {
            destination = new RedBlackTree<T>(destination);
            source = new RedBlackTree<T>(source);

            Debug.Assert(destination.Height >= source.Height);
            int currentHeight = destination.Height;
            RedBlackTreeNode<T> currentNode = destination.Root;
            while (currentNode != null && (currentHeight > source.Height || currentNode.IsRed))
            {
                if (!currentNode.IsRed)
                {
                    currentHeight--;
                }

                currentNode = isLeftSideMerge ? currentNode.Left : currentNode.Right;
            }

            if (currentNode != null)
            {
                RedBlackTreeNode<T> parent = currentNode.Parent;
                RedBlackTreeNode<T> medianNode = new RedBlackTreeNode<T>(medianValue)
                {
                    IsRed = true
                };

                BinaryTreeNode<T, RedBlackTreeNode<T>>.Link(parent, medianNode, parent?.Left == currentNode);
                BinaryTreeNode<T, RedBlackTreeNode<T>>.Link(medianNode, currentNode, !isLeftSideMerge);
                BinaryTreeNode<T, RedBlackTreeNode<T>>.Link(medianNode, source.Root, isLeftSideMerge);

                destination.Count += source.Count + 1;
                destination.InsertFixup(medianNode);
            }

            return destination;
        }

        private RedBlackTree<T> UnionWith(RedBlackTree<T> other, T median, bool shouldUseMedian)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            T min = Min(),
              max = Max(),
              otherMin = other.Min(),
              otherMax = other.Max();

            if (Compare(otherMax, min) <= 0)
            {
                if (shouldUseMedian && (Compare(otherMax, median) > 0 || Compare(median, min) > 0))
                {
                    throw new ArgumentOutOfRangeException(nameof(median));
                }

                return Height >= other.Height ?
                    Merge(other, this, shouldUseMedian ? median : otherMax, true) :
                    Merge(this, other, shouldUseMedian ? median : min, false);
            }
            else if (Compare(max, otherMin) <= 0)
            {
                if (shouldUseMedian && (Compare(max, median) > 0 || Compare(median, otherMin) > 0))
                {
                    throw new ArgumentOutOfRangeException(nameof(median));
                }

                return Height >= other.Height ?
                    Merge(other, this, shouldUseMedian ? median : otherMin, false) :
                    Merge(this, other, shouldUseMedian ? median : max, true);
            }

            throw new ArgumentException(
                nameof(other),
                "Union is supported for trees with non-overlapping ranges only");
        }

        private void InsertFixup(RedBlackTreeNode<T> node)
        {
            while (node.Parent?.IsRed == true)
            {
                RedBlackTreeNode<T> parent = node.Parent;
                RedBlackTreeNode<T> grandParent = parent.Parent;
                Debug.Assert(grandParent != null);

                if (grandParent.Left == parent)
                {
                    RedBlackTreeNode<T> uncle = grandParent.Right;

                    if (uncle?.IsRed == true)
                    {
                        grandParent.IsRed = true;
                        parent.IsRed = false;
                        if (uncle != null)
                        {
                            uncle.IsRed = false;
                        }

                        node = grandParent;
                    }
                    else
                    {
                        if (parent.Right == node)
                        {
                            RotateLeft(node);
                            node = parent;
                        }

                        RotateRight(node.Parent);
                        node.Parent.IsRed = false;
                        grandParent.IsRed = true;
                    }
                }
                else
                {
                    RedBlackTreeNode<T> uncle = grandParent.Left;

                    if (uncle?.IsRed == true)
                    {
                        grandParent.IsRed = true;
                        parent.IsRed = false;
                        if (uncle != null)
                        {
                            uncle.IsRed = false;
                        }

                        node = grandParent;
                    }
                    else
                    {
                        if (parent.Left == node)
                        {
                            RotateRight(node);
                            node = parent;
                        }

                        RotateLeft(node.Parent);
                        node.Parent.IsRed = false;
                        grandParent.IsRed = true;
                    }
                }
            }

            if (Root.IsRed)
            {
                Height++;
            }

            Root.IsRed = false;
        }
    }
}