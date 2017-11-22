using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees.Entities
{
    public class RedBlackTreeNode<TData, TNode> : BinaryTreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        public RedBlackTreeNode()
        {
        }

        public RedBlackTreeNode(TData data) : base(data)
        {
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
    }
}