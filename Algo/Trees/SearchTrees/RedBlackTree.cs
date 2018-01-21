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

            if (current != null)
            {
                current.IsRed = false;
            }

            return deleted;
        }
    }
}