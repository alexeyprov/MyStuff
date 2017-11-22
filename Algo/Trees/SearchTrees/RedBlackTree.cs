using System;
using System.Collections.Generic;
using System.Diagnostics;

using Algo.Trees.SearchTrees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class RedBlackTree<T> : BinarySearchTree<T, RedBlackTreeNode<T>>
        where T : IComparable<T>
    {
        public RedBlackTree(IComparer<T> comparer = null) : base(comparer)
        {
        }

        protected override bool AddNode(RedBlackTreeNode<T> node)
        {
            if (!base.AddNode(node))
            {
                return false;
            }

            node.IsRed = true;
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

            Root.IsRed = false;
            return true;
        }
    }
}