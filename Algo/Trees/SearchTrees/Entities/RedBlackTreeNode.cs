using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees.Entities
{
    public abstract class RedBlackTreeNode<TData, TNode> : BinaryTreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        protected RedBlackTreeNode()
        {
        }

        protected RedBlackTreeNode(TData data) : base(data)
        {
        }

        protected RedBlackTreeNode(RedBlackTreeNode<TData, TNode> node) : base(node)
        {
            IsRed = node.IsRed;
        }

        public bool IsRed { get; set; }

        public override string ToString() =>
            $"{base.ToString()} - {(IsRed ? "red" : "black")}";
    }

    public class RedBlackTreeNode<TData> : RedBlackTreeNode<TData, RedBlackTreeNode<TData>>
    {
        public RedBlackTreeNode()
        {
        }

        public RedBlackTreeNode(TData data) : base(data)
        {
        }

        public RedBlackTreeNode(RedBlackTreeNode<TData> node) :
            base(node)
        {
        }

        protected override RedBlackTreeNode<TData> Clone() =>
            new RedBlackTreeNode<TData>(this);
    }
}