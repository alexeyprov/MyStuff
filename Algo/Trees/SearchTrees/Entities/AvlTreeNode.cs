using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees.Entities
{
    public class AvlTreeNode<TData, TNode> : BinaryTreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        public AvlTreeNode()
        {
        }

        public AvlTreeNode(TData data) : base(data)
        {
        }

        public NodeBalance Balance { get; set; }
    }

    public class AvlTreeNode<TData> : AvlTreeNode<TData, AvlTreeNode<TData>>
    {
        public AvlTreeNode()
        {
        }

        public AvlTreeNode(TData data) : base(data)
        {
        }
    }
}