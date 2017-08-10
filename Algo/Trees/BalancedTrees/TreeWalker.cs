using System;

namespace Algo.Trees.BalancedTrees
{
    public class TreeWalker<T>
    {
        private readonly TreeNode<T> _root;
        private TreeNode<T> _unbalancedSubtree;

        public TreeWalker(TreeNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            _root = root;
        }

        public TreeNode<T> FindUnbalancedSubtree()
        {
            _unbalancedSubtree = null;

            CheckBalance(_root);

            return _unbalancedSubtree;
        }

        private int CheckBalance(TreeNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            int leftDepth = CheckBalance(node.Left);
            int rightDepth = CheckBalance(node.Right);

            if (Math.Abs(leftDepth - rightDepth) > 1)
            {
                _unbalancedSubtree = node;
            }

            return Math.Max(leftDepth, rightDepth) + 1;
        }
    }
}