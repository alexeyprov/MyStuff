using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class BinarySearchTree<TData, TNode> : ICollection<TData>
        where TData : IComparable<TData>
        where TNode : BinaryTreeNode<TData, TNode>, new()
    {
        private readonly IComparer<TData> _comparer;
        private int _count;
        private TNode _root;

        public BinarySearchTree(IComparer<TData> comparer = null)
        {
            _comparer = comparer ?? Comparer<TData>.Default;
        }

        #region ICollection<TData> Members

        public int Count => _count;

        public bool IsReadOnly => false;

        public virtual void Add(TData item)
        {
            TNode newNode = new TNode
            {
                Data = item
            };

            if (_root == null)
            {
                _root = newNode;
                _count++;
                return;
            }

            TNode parent = null, current = _root;
            bool isLeft = false;
            while (current != null)
            {
                parent = current;
                switch (_comparer.Compare(item, current.Data))
                {
                    case int result when result < 0:
                        current = current.Left;
                        isLeft = true;
                        break;
                    case int result when result > 0:
                        current = current.Right;
                        isLeft = false;
                        break;
                    default:
                        return;
                }
            }

            newNode.Parent = parent;
            if (isLeft)
            {
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }
            _count++;
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        public bool Contains(TData item)
        {
            TNode current = _root;
            while (current != null)
            {
                switch (_comparer.Compare(item, current.Data))
                {
                    case 0:
                        return true;
                    case int result when result < 0:
                        current = current.Left;
                        break;
                    case int result when result > 0:
                        current = current.Right;
                        break;
                }
            }

            return false;
        } 
                
        // CopyTo copies a collection into an Array, starting at a particular
        // index into the array.
        // 
        public void CopyTo(TData[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
                
        public bool Remove(TData item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<TData> Members

        IEnumerator<TData> IEnumerable<TData>.GetEnumerator()
        {
            return IterateInOrder(_root).Select(n => n.Data).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable<TData>)this).GetEnumerator();

        #endregion

        private static IEnumerable<TNode> IterateInOrder(TNode node)
        {
            if (node == null)
            {
                yield break;
            }

            foreach (TNode leftTreeNode in IterateInOrder(node.Left))
            {
                yield return leftTreeNode;
            }

            yield return node;

            foreach (TNode rightTreeNode in IterateInOrder(node.Right))
            {
                yield return rightTreeNode;
            }
        }
    }
}