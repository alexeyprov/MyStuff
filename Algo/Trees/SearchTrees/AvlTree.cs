using System;
using System.Collections.Generic;
using System.Diagnostics;

using Algo.Trees.Entities;
using Algo.Trees.SearchTrees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class AvlTree<T> : BinarySearchTree<T, AvlTreeNode<T>>
        where T : IComparable<T>
    {
        public AvlTree(IComparer<T> comparer = null) : base(comparer)
        {
        }

        protected override bool AddNode(AvlTreeNode<T> newNode)
        {
            if (!base.AddNode(newNode))
            {
                return false;
            }

            AvlTreeNode<T> current = newNode.Parent,
                           child = newNode,
                           grandChild = null;
            bool isLeftChild = current?.Left == child,
                 isLeftGrandChild = false;

            while (current != null)
            {
                int rotations = 0;
                switch (current.Balance)
                {
                    case NodeBalance.Even:
                        current.Balance = isLeftChild ? 
                            NodeBalance.LeftSkewed : 
                            NodeBalance.RightSkewed;
                        break;

                    case NodeBalance.LeftSkewed:
                        if (!isLeftChild)
                        {
                            current.Balance = NodeBalance.Even;
                            return true;
                        }

                        if (isLeftGrandChild)
                        {
                            // LL case
                            RotateRight(child);
                            rotations = 1;
                        }
                        else
                        {
                            // LR case
                            RotateLeft(grandChild);
                            RotateRight(grandChild);
                            rotations = 2;
                        }

                        break;

                    case NodeBalance.RightSkewed:
                        if (isLeftChild)
                        {
                            current.Balance = NodeBalance.Even;
                            return true;
                        }

                        if (isLeftGrandChild)
                        {
                            // RL case
                            RotateRight(grandChild);
                            RotateLeft(grandChild);
                            rotations = 2;
                        }
                        else
                        {
                            // RR case
                            RotateLeft(child);
                            rotations = 1;
                        }

                        break;

                    default:
                        Debug.Fail($"Unexpected balance value {current.Balance} at node {current.Data}");
                        break;
                }

                // advance pointers
                switch (rotations)
                {
                    case 0:
                        grandChild = child;
                        child = current;
                        current = current.Parent;
                        break;

                    case 1:
                        current = child.Parent;
                        break;

                    case 2:
                        grandChild = child;
                        child = grandChild.Parent;
                        current = child.Parent;
                        break;
                }

                // update path directions
                isLeftGrandChild = child.Left == grandChild;
                isLeftChild = current?.Left == child;
            }

            return true;
        }

        private void RotateRight(AvlTreeNode<T> node)
        {
            if (node?.Parent == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            AvlTreeNode<T> parent = node.Parent;
            AvlTreeNode<T> grandParent = parent.Parent;
            
            BinaryTreeNode<T, AvlTreeNode<T>>.Link(parent, node.Right, true);
            BinaryTreeNode<T, AvlTreeNode<T>>.Link(node, parent, false);
            BinaryTreeNode<T, AvlTreeNode<T>>.Link(grandParent, node, grandParent?.Left == parent);

            node.ShiftBalanceRight();
            parent.ShiftBalanceRight();

            if (Root == parent)
            {
                Root = node;
            }
        }

        private void RotateLeft(AvlTreeNode<T> node)
        {
            if (node?.Parent == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            AvlTreeNode<T> parent = node.Parent;
            AvlTreeNode<T> grandParent = parent.Parent;

            BinaryTreeNode<T, AvlTreeNode<T>>.Link(parent, node.Left, false);
            BinaryTreeNode<T, AvlTreeNode<T>>.Link(node, parent, true);
            BinaryTreeNode<T, AvlTreeNode<T>>.Link(grandParent, node, grandParent?.Left == parent);

            node.ShiftBalanceLeft();
            parent.ShiftBalanceLeft();

            if (Root == parent)
            {
                Root = node;
            }
        }
    }
}