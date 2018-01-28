namespace Algo.Trees.Entities
{
    public abstract class BinaryTreeNode<TData, TNode> : TreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        protected BinaryTreeNode() :
            base(2)
        {
        }

        protected BinaryTreeNode(TData data) :
            base(data, 2)
        {
        }

        protected BinaryTreeNode(BinaryTreeNode<TData, TNode> node) : base(node)
        {
        }

        public TNode Left
        {
            get => Children[0];
            set => UpdateChild(0, value);
        }

        public TNode Right
        {
            get => Children[1];
            set => UpdateChild(1, value);
        }

        public static void Link(TNode parent, TNode child, bool isLeftChild)
        {
            if (parent != null)
            {
                if (isLeftChild)
                {
                    parent.Left = child;
                }
                else
                {
                    parent.Right = child;
                }
            }

            if (child != null)
            {
                child.Parent = parent;
            }
        }

        public TNode GetSuccessor()
        {
            TNode current = Right;

            if (current == null)
            {
                return null;
            }

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        public TNode GetPredecessor()
        {
            TNode current = Left;

            if (current.Right == null)
            {
                return null;
            }

            while (current.Right != null)
            {
                current = current.Right;
            }

            return current;
        }

        public override string ToString() =>
            $"[{Data}]";
    }

    public class BinaryTreeNode<TData> : BinaryTreeNode<TData, BinaryTreeNode<TData>>
    {
        public BinaryTreeNode() :
            base()
        {
        }

        public BinaryTreeNode(TData data) :
            base(data)
        {
        }

        public BinaryTreeNode(BinaryTreeNode<TData> node) : base(node)
        {
        }

        protected override BinaryTreeNode<TData> Clone() =>
            new BinaryTreeNode<TData>(this);
    }
}