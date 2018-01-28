using System;

using Algo.Trees.SearchTrees.Entities;

namespace Algo.Trees.SearchTrees
{
    public static class RedBlackTreeExtensions
    {
        public static void Validate<T>(this RedBlackTree<T> tree) where T : IComparable<T>
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            if (tree.Root == null)
            {
                return;
            }

            // property 2
            if (tree.Root.IsRed)
            {
                throw new ArgumentException("Root should have black color");
            }

            CheckNode(tree.Root);
        }

        private static (int BlackHeight, bool IsRed) CheckNode<T>(RedBlackTreeNode<T> node)
        {
            if (node == null)
            {
                return (0, false);
            }

            (int BlackHeight, bool IsRed) leftStatus = CheckNode(node.Left),
                                          rightStatus = CheckNode(node.Right);

            // property 4
            if (node.IsRed && (leftStatus.IsRed || rightStatus.IsRed))
            {
                throw new ArgumentException("Red node should have black children only");
            }

            // property 5
            if (leftStatus.BlackHeight != rightStatus.BlackHeight)
            {
                throw new ArgumentException("Black heights for right and left children should match");
            }

            return
                (
                    node.IsRed ? leftStatus.BlackHeight : leftStatus.BlackHeight + 1,
                    node.IsRed
                );
        }
    }
}
