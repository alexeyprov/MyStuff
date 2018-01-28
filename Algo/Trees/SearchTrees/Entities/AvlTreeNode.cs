using System;
using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees.Entities
{
    public abstract class AvlTreeNode<TData, TNode> : BinaryTreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        protected AvlTreeNode()
        {
        }

        protected AvlTreeNode(TData data) : base(data)
        {
        }

        protected AvlTreeNode(AvlTreeNode<TData, TNode> node) : base(node)
        {
            Balance = node.Balance;
        }

        public NodeBalance Balance { get; set; }

        public void ShiftBalanceRight()
        {
            switch (Balance)
            {
                case NodeBalance.LeftSkewed:
                    Balance = NodeBalance.Even;
                    break;

                case NodeBalance.Even:
                    Balance = NodeBalance.RightSkewed;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Cannot shift balance to the right for node '{Data}'");
            }
        }

        public void ShiftBalanceLeft()
        {
            switch (Balance)
            {
                case NodeBalance.RightSkewed:
                    Balance = NodeBalance.Even;
                    break;

                case NodeBalance.Even:
                    Balance = NodeBalance.LeftSkewed;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Cannot shift balance to the left for node '{Data}'");
            }
        }

        public override string ToString() =>
            $"{base.ToString()} - {Balance}";
    }

    public class AvlTreeNode<TData> : AvlTreeNode<TData, AvlTreeNode<TData>>
    {
        public AvlTreeNode()
        {
        }

        public AvlTreeNode(TData data) : base(data)
        {
        }

        public AvlTreeNode(AvlTreeNode<TData> node) : base(node)
        {
        }

        protected override AvlTreeNode<TData> Clone() =>
            new AvlTreeNode<TData>(this);
    }
}