using System;

using Algo.Trees.Entities;
using Algo.Trees.SearchTrees;

namespace Algo.Trees.TestConsole
{
    internal static class Program
    {
        private static readonly BinaryTreeNode<int> _balancedTree;
        private static readonly BinaryTreeNode<int> _unbalancedTree;

        static Program()
        {
            //       0
            //   1       5
            // 2   3  6
            //4
            _balancedTree = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(2)
                    {
                        Left = new BinaryTreeNode<int>(4)
                    },
                    Right = new BinaryTreeNode<int>(3)
                },
                Right = new BinaryTreeNode<int>(5)
                {
                    Left = new BinaryTreeNode<int>(6)
                }
            };

            //       0
            //   1      5
            // 2      6   7
            //  4
            _unbalancedTree = new BinaryTreeNode<int>(0)
            {
                Left = new BinaryTreeNode<int>(1)
                {
                    Left = new BinaryTreeNode<int>(2)
                    {
                        Right = new BinaryTreeNode<int>(4)
                    }
                },
                Right = new BinaryTreeNode<int>(5)
                {
                    Left = new BinaryTreeNode<int>(6),
                    Right = new BinaryTreeNode<int>(7)
                }
            };
        }

        private static void Main()
        {
            TestTreeBalance(_balancedTree);
            TestTreeBalance(_unbalancedTree);
            TestBst();
        }

        private static void TestTreeBalance(BinaryTreeNode<int> tree)
        {
            BalanceChecker<int, BinaryTreeNode<int>> checker = 
                new BalanceChecker<int, BinaryTreeNode<int>>(tree);
            BinaryTreeNode<int> unbalancedSubtree = checker.FindUnbalancedSubtree();
            if (unbalancedSubtree == null)
            {
                Console.WriteLine("Tree is balanced");
            }
            else
            {
                Console.WriteLine($"Tree is unbalanced at {unbalancedSubtree.Data}");
            }
        }

        private static void TestBst()
        {
            BinarySearchTree<int, BinaryTreeNode<int>> bst = new BinarySearchTree<int, BinaryTreeNode<int>>();

            bst.Add(5);
            bst.Add(9);
            bst.Add(2);
            bst.Add(4);
            bst.Add(1);

            Console.WriteLine($"BST: {string.Join(", ", bst)}");
        }
    }
}
