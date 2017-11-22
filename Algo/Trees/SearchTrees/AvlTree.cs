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
                        }
                        else
                        {
                            // LR case
                            RotateLeft(grandChild);
                            RotateRight(grandChild);
                        }

                        return true;

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
                        }
                        else
                        {
                            // RR case
                            RotateLeft(child);
                        }

                        return true;

                    default:
                        Debug.Fail($"Unexpected balance value {current.Balance} at node {current.Data}");
                        break;
                }

                // advance pointers
                grandChild = child;
                child = current;
                current = current.Parent;

                // update path directions
                isLeftGrandChild = child.Left == grandChild;
                isLeftChild = current?.Left == child;
            }

            return true;
        }

        protected override AvlTreeNode<T> RemoveNode(AvlTreeNode<T> node, AvlTreeNode<T> parent)
        {
            node = base.RemoveNode(node, parent);

            AvlTreeNode<T> current = node.Parent;
            bool isLeftChild = current != null && Compare(node, current) < 0;
            while (current != null)
            {
                bool isRotated = false;
                switch (current.Balance)
                {
                    case NodeBalance.Even:
                        current.Balance = isLeftChild ? 
                            NodeBalance.RightSkewed : 
                            NodeBalance.LeftSkewed;
                        return node;

                    case NodeBalance.LeftSkewed:
                        if (isLeftChild)
                        {
                            current.Balance = NodeBalance.Even;
                            break;
                        }

                        if (current.Left.Balance == NodeBalance.RightSkewed)
                        {
                            // LR case
                            RotateLeft(current.Left.Right);
                            RotateRight(current.Left);
                        }
                        else
                        {
                            // LL case
                            RotateRight(current.Left);
                        }

                        isRotated = true;
                        break;

                    case NodeBalance.RightSkewed:
                        if (!isLeftChild)
                        {
                            current.Balance = NodeBalance.Even;
                            break;
                        }

                        if (current.Right.Balance == NodeBalance.LeftSkewed)
                        {
                            // RL case
                            RotateRight(current.Right.Left);
                            RotateLeft(current.Right);
                        }
                        else
                        {
                            // RR case
                            RotateLeft(current.Right);
                        }

                        isRotated = true;
                        break;

                    default:
                        Debug.Fail($"Unexpected balance value {current.Balance} at node {current.Data}");
                        break;
                }

                AvlTreeNode<T> child = isRotated ? current.Parent : current;
                Debug.Assert(child != null);

                current = child.Parent;
                isLeftChild = current?.Left == child;
            }

            return node;
        }

        protected override void RotateRight(AvlTreeNode<T> node)
        {
            AvlTreeNode<T> parent = node?.Parent;
            base.RotateRight(node);

            node.ShiftBalanceRight();
            parent.ShiftBalanceRight();
        }

        protected override void RotateLeft(AvlTreeNode<T> node)
        {
            AvlTreeNode<T> parent = node?.Parent;
            base.RotateLeft(node);

            node.ShiftBalanceLeft();
            parent.ShiftBalanceLeft();
        }
    }
}