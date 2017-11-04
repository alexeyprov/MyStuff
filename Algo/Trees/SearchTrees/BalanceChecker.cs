using System;

using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class BalanceChecker<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        private readonly TNode _root;
        private TNode _unbalancedSubtree;

        public BalanceChecker(TNode root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public TNode FindUnbalancedSubtree()
        {
            _unbalancedSubtree = null;

            CheckDepth(_root);

            return _unbalancedSubtree;
        }

        private int CheckDepth(TNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int leftDepth = CheckDepth(node.Left);
            int rightDepth = CheckDepth(node.Right);

            if (Math.Abs(leftDepth - rightDepth) > 1)
            {
                _unbalancedSubtree = node;
            }

            return Math.Max(leftDepth, rightDepth) + 1;
        }
    }
}