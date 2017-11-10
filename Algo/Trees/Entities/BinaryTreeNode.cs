namespace Algo.Trees.Entities
{
    public class BinaryTreeNode<TData, TNode> : TreeNode<TData, TNode>
        where TNode : BinaryTreeNode<TData, TNode>
    {
        public BinaryTreeNode() :
            base(2)
        {
        }

        public BinaryTreeNode(TData data) :
            base(data, 2)
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
    }
}