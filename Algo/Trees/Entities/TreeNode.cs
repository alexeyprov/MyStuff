using System;
using System.Collections.Generic;

namespace Algo.Trees.Entities
{
    public class TreeNode<TData, TNode>
        where TNode : TreeNode<TData, TNode>
    {
        private readonly TNode[] _children;    

        public TreeNode(int size)
        {
            _children = size > 0 ?
                new TNode[size] :
                throw new ArgumentOutOfRangeException(nameof(size));
        }

        public TreeNode(TData data, int size) :
            this(size)
        {
            Data = data;
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
    }

    public sealed class TreeNode<TData> : TreeNode<TData, TreeNode<TData>>
    {
        public TreeNode(int size) : base(size)
        {
        }

        public TreeNode(TData data, int size) : base(data, size)
        {
        }
    }
}