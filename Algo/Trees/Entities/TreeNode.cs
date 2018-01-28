using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Trees.Entities
{
    public abstract class TreeNode<TData, TNode>
        where TNode : TreeNode<TData, TNode>
    {
        private readonly TNode[] _children;

        protected TreeNode(int size)
        {
            _children = size > 0 ?
                new TNode[size] :
                throw new ArgumentOutOfRangeException(nameof(size));
        }

        protected TreeNode(TData data, int size) :
            this(size)
        {
            Data = data;
        }

        protected TreeNode(TreeNode<TData, TNode> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Data = node.Data;
            _children = node._children
                .Select(CopyNode)
                .ToArray();
        }

        public TData Data
        {
            get;
            set;
        }

        public TNode Parent
        {
            get;
            set;
        }

        public IReadOnlyList<TNode> Children => _children;

        protected void UpdateChild(int index, TNode node)
        {
            _children[index] = node;
        }

        protected abstract TNode Clone();

        private TNode CopyNode(TNode node)
        {
            if (node == null)
            {
                return null;
            }

            TNode copy = node.Clone();
            copy.Parent = (TNode)this;
            return copy;
        }
    }

    public sealed class TreeNode<TData> : TreeNode<TData, TreeNode<TData>>
    {
        public TreeNode(int size) : base(size)
        {
        }

        public TreeNode(TData data, int size) : base(data, size)
        {
        }

        public TreeNode(TreeNode<TData> node) : base(node)
        {
        }

        protected override TreeNode<TData> Clone() =>
            new TreeNode<TData>(this);
    }
}