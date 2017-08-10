namespace Algo.Trees.BalancedTrees
{
    public class TreeNode<T>
    {
        public TreeNode()
        {
        }

        public TreeNode(T data)
        {
            Data = data;
        }

        public T Data
        {
            get;
            set;
        }

        public TreeNode<T> Left
        {
            get;
            set;
        }

        public TreeNode<T> Right
        {
            get;
            set;
        }
    }
}