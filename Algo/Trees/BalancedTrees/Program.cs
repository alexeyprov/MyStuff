using System;

namespace Algo.Trees.BalancedTrees
{
    internal static class Program
    {
        private static readonly TreeNode<int> _balancedTree;
        private static readonly TreeNode<int> _unbalancedTree;

        static Program()
        {
            //       0
            //   1       5
            // 2   3  6
            //4
            _balancedTree = new TreeNode<int>(0)
            {
                Left = new TreeNode<int>(1)
                {
                    Left = new TreeNode<int>(2)
                    {
                        Left = new TreeNode<int>(4)
                    },
                    Right = new TreeNode<int>(3)
                },
                Right = new TreeNode<int>(5)
                {
                    Left = new TreeNode<int>(6)
                }
            };

            //       0
            //   1      5
            // 2      6   7
            //  4
            _unbalancedTree = new TreeNode<int>(0)
            {
                Left = new TreeNode<int>(1)
                {
                    Left = new TreeNode<int>(2)
                    {
                        Right = new TreeNode<int>(4)
                    }
                },
                Right = new TreeNode<int>(5)
                {
                    Left = new TreeNode<int>(6),
                    Right = new TreeNode<int>(7)
                }
            };
        }

        public static void Main()
        {
            TestTree(_balancedTree);
            TestTree(_unbalancedTree);
        }

        private static void TestTree(TreeNode<int> tree)
        {
            TreeWalker<int> walker = new TreeWalker<int>(tree);
            TreeNode<int> unbalancedSubtree = walker.FindUnbalancedSubtree();
            if (unbalancedSubtree == null)
            {
                Console.WriteLine("Tree is balanced");
            }
            else
            {
                Console.WriteLine($"Tree is unbalanced at {unbalancedSubtree.Data}");
            }
        }
    }
}
