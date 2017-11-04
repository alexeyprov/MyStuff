using System;
using System.Collections;
using System.Collections.Generic;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}