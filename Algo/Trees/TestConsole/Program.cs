using System;
using System.Diagnostics;

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
            TestAvl();
            TestRedBlack();
        }

        private static void TestTreeBalance(BinaryTreeNode<int> tree)
        {
            Console.WriteLine("Checking tree balance for:");
            tree.Print();

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
            bst.Add(3);
            bst.Add(1);
            bst.Add(12);
            bst.Add(7);
            bst.Add(8);

            //         5
            //      2     9
            //    1   4 7   12
            //       3   8
            Console.WriteLine($"Initial BST: {string.Join(", ", bst)}");

            Debug.Assert(bst.Remove(5));
            Debug.Assert(bst.Remove(4));
            Debug.Assert(!bst.Remove(6));

            Console.WriteLine($"BST after removal: {string.Join(", ", bst)}");
        }

        private static void TestAvl()
        {
            AvlTree<int> avl = new AvlTree<int>();

            avl.Add(5);
            avl.Add(9);
            avl.Add(12); // RR case
            avl.Add(2);
            avl.Add(4);  // LR case

            avl.Add(1);  // LL case
            avl.Add(8);
            avl.Add(10);

            //        4     
            //     2     9
            //   1     5    12
            //          8 10
            Console.WriteLine($"Initial AVL tree: {string.Join(", ", avl)}");
              
            Debug.Assert(avl.Remove(4));
            Debug.Assert(avl.Remove(8)); // RL case
            Debug.Assert(!avl.Remove(6));

            Console.WriteLine($"AVL tree after removal: {string.Join(", ", avl)}");
        }

        private static void TestRedBlack()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();

            tree.Add(5);
            tree.Add(9);
            tree.Add(12); // left rotate (case 3)
            tree.Add(2);  // red uncle (case 1)
            tree.Add(4);  // left, right rotate (cases 2, 3)
            tree.Add(1);  // red uncle
            tree.Add(8);
            tree.Add(10);
            tree.Validate();
            
            //       9   
            //   4       12
            // 2   5  10
            //1     8
            Console.WriteLine($"Initial red-black tree: {string.Join(", ", tree)}");

            Debug.Assert(tree.Remove(8));
            Debug.Assert(tree.Remove(2));
            Debug.Assert(tree.Remove(4));
            Debug.Assert(tree.Remove(9));
            Debug.Assert(!tree.Remove(15));
            tree.Validate();

            Console.WriteLine($"Red-black tree after removal: {string.Join(", ", tree)}");

            RedBlackTree<int> exerciseTree = new RedBlackTree<int>();
            exerciseTree.Add(41);
            exerciseTree.Add(38);
            exerciseTree.Add(31);
            exerciseTree.Add(12);
            exerciseTree.Add(19);
            exerciseTree.Add(8);

            exerciseTree.Validate();
            Console.WriteLine(
                $"Ex. 13.4-3 red-black tree: {string.Join(", ", exerciseTree)}. Height={exerciseTree.Height}");

            tree = new RedBlackTree<int>(exerciseTree);
            Debug.Assert(tree.Remove(8));
            Debug.Assert(tree.Remove(12));
            Debug.Assert(tree.Remove(19));
            Debug.Assert(tree.Remove(31));
            Debug.Assert(tree.Remove(38));
            Debug.Assert(tree.Remove(41));
            tree.Validate();

            Console.WriteLine($"Ex 13.4-3 red-black tree after removal: {string.Join(", ", tree)}. Height={tree.Height}");

            tree.Clear();
            tree.Add(42);
            tree.Add(43);
            tree.Add(44);
            tree.Validate();

            // right-side union
            tree = exerciseTree.Union(tree);
            Debug.Assert(tree.Count == 10);
            Debug.Assert(tree.Height == 2);
            tree.Validate();

            RedBlackTree<int> smallTree = new RedBlackTree<int>
            {
                1,
                2
            };
            smallTree.Validate();

            // left-side union
            tree = smallTree.Union(tree, 4);
            Debug.Assert(tree.Count == 13);
            Debug.Assert(tree.Height == 3);
            tree.Validate();
        }
    }
}
