using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Trees.Entities;

namespace Algo.Trees.SearchTrees
{
    public class BinarySearchTree<TData, TNode> : ICollection<TData>
        where TData : IComparable<TData>
        where TNode : BinaryTreeNode<TData, TNode>, new()
    {
        #region Private Fields

        private readonly IComparer<TData> _comparer;
        private int _count;

        #endregion

        #region Constructors

        public BinarySearchTree(IComparer<TData> comparer = null)
        {
            _comparer = comparer ?? Comparer<TData>.Default;
        }

        #endregion

        #region ICollection<TData> Members

        public int Count => _count;

        public bool IsReadOnly => false;

        public virtual void Add(TData item)
        {
            TNode newNode = new TNode
            {
                Data = item
            };

            if (AddNode(newNode))
            {
                _count++;
            }
        }

        public void Clear()
        {
            Root = null;
            _count = 0;
        }

        public bool Contains(TData item) => FindNode(item).node != null;
                
        // CopyTo copies a collection into an Array, starting at a particular
        // index into the array.
        // 
        public void CopyTo(TData[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
                
        public bool Remove(TData item)
        {
            (TNode node, TNode parent) = FindNode(item);
            if (node == null)
            {
                return false;
            }

            RemoveNode(node, parent);
            return true;
        }

        #endregion

        #region IEnumerable<TData> Members

        IEnumerator<TData> IEnumerable<TData>.GetEnumerator()
        {
            return IterateInOrder(Root).Select(n => n.Data).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator() =>
            ((IEnumerable<TData>)this).GetEnumerator();

        #endregion

        #region Protected Interface

        protected TNode Root { get; set; }

        protected virtual bool AddNode(TNode newNode)
        {
            TData item = newNode.Data;
            (TNode node, TNode parent) = FindNode(item);
            if (node != null)
            {
                return false;
            }

            newNode.Parent = parent;
            if (parent == null)
            {
                Root = newNode;
            }
            else
            {
                if (_comparer.Compare(item, parent.Data) < 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }

            return true;
        }

        protected virtual TNode RemoveNode(TNode node, TNode parent)
        {
            TNode leftChild = node.Left,
                  rightChild = node.Right,
                  deleted = node,
                  replacement = null;

            if (leftChild == null)
            {
                replacement = rightChild;
            }
            else if (rightChild == null)
            {   
                replacement = leftChild;
            }
            else
            {
                TNode successor = node.GetSuccessor();
                Debug.Assert(successor != null);
                Debug.Assert(successor.Left == null);

                deleted = successor;
                replacement = successor.Right;
            }
            
            BinaryTreeNode<TData, TNode>.Link(
                deleted.Parent, 
                replacement, 
                deleted.Parent?.Left == deleted);

            if (deleted != node)
            {
                node.Data = deleted.Data;
            }
            else if (node == Root)
            {
                Root = replacement;
            }

            _count--;
            return deleted;
        }

        protected int Compare(TNode left, TNode right) =>
            _comparer.Compare(left.Data, right.Data);

        #endregion

        #region Implementation

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

        private (TNode node, TNode parent) FindNode(TData item)
        {
            TNode current = Root, parent = null;
            while (current != null)
            {
                switch (_comparer.Compare(item, current.Data))
                {
                    case 0:
                        return (current, parent);
                    case int result when result < 0:
                        parent = current;
                        current = current.Left;
                        break;
                    case int result when result > 0:
                        parent = current;
                        current = current.Right;
                        break;
                }
            }

            return (null, parent);
        }

        #endregion
    }
}