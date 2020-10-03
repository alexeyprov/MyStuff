using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Algo.Common;

namespace Algo.Heaps.Entities
{
    public class DHeap<TKey, TValue> : IHeap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly int _d;
        private readonly bool _isMaxHeap;
        private readonly IComparer<TKey> _comparer;

        private IList<HeapNode<TKey, TValue>> _heap;
        private int _size;

        public DHeap(
            int d,
            IEnumerable<TValue> values, 
            IEnumerable<TKey> keys,
            bool isMaxHeap,
            IComparer<TKey> comparer = null)
        {
            _d = VerifyChildCount(d);
            _isMaxHeap = isMaxHeap;
            _comparer = comparer ?? Comparer<TKey>.Default;

            _heap = (values ?? throw new ArgumentNullException(nameof(values)))
                .Zip(
                    (keys ?? throw new ArgumentNullException(nameof(keys))),
                    (v, k) => new HeapNode<TKey, TValue> { Key = k, Value = v })
                .ToList();
            _size = _heap.Count;

            Heapify();
        }

        public DHeap(
            int d,
            IEnumerable<TValue> values,
            bool isMaxHeap,
            Func<TValue, TKey> keyFactory, 
            IComparer<TKey> comparer = null)
        {
            if (keyFactory == null)
            {
                throw new ArgumentNullException(nameof(keyFactory));
            }

            _d = VerifyChildCount(d);
            _isMaxHeap = isMaxHeap;
            _comparer = comparer ?? Comparer<TKey>.Default;

            _heap = (values ?? throw new ArgumentNullException(nameof(values)))
                .Select(v => new HeapNode<TKey, TValue> { Key = keyFactory(v), Value = v })
                .ToList();
            _size = _heap.Count;

            Heapify();
        }

        public int Count => _size;

        public virtual (TKey, TValue) Peek() =>
            _size == 0 ?
                throw new InvalidOperationException() :
                (_heap[0].Key, _heap[0].Value);

        public virtual (TKey, TValue) Extract()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            HeapNode<TKey, TValue> top = _heap[0];
            _heap[0] = _heap[--_size];
            SiftDown(0);
            return (top.Key, top.Value);
        }

        public virtual void Add(TKey key, TValue value)
        {
            _heap.Add(new HeapNode<TKey, TValue> { Key = key, Value = value });
            SiftUp(_size++);
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => 
            _heap.Select(n => n.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>)this).GetEnumerator();

        protected void SiftDown(int index, bool useQuickSwap = false)
        {
            int winner = index;
            int firstChild = GetFirstChild(index);
            int lastChild = Math.Min(firstChild, _size - 1);
            for (int child = firstChild; child < lastChild; ++child)
            {
                winner = Compare(winner, child);
            }

            if (winner != index)
            {
                if (useQuickSwap)
                {
                    _heap.Swap(index, winner);
                }
                else
                {
                    Swap(index, winner);
                }

                SiftDown(winner, useQuickSwap);
            }
        }

        protected void SiftUp(int index, bool useQuickSwap = false)
        {
            if (index == 0)
            {
                return;
            }

            int parent = GetParent(index);
            if (Compare(index, parent) != parent)
            {
                if (useQuickSwap)
                {
                    _heap.Swap(index, parent);
                }
                else
                {
                    Swap(index, parent);
                }

                SiftUp(parent);
            }
        }

        protected virtual void Swap(int index, int other)
        {
            Debug.Assert(index != other);
            _heap.Swap(index, other);
        }

        /// <summary>
        /// Compares heap elements at two indices and returns the index of parent element.
        /// I.e., this is the index of bigger element in a max heap, and index of the smaller
        /// element in a min heap.
        /// </summary>
        protected int Compare(int index, int other)
        {
            int orderFactor = _isMaxHeap ? -1 : 1;
            int compareResult = _comparer.Compare(_heap[index].Key, _heap[other].Key);
            return orderFactor * compareResult < 0 ? index : other;
        }

        private static int VerifyChildCount(int d) =>
            d > 2 || d < int.MaxValue / 2 ? 
                d : 
                throw new ArgumentOutOfRangeException(nameof(d));

        private int GetParent(int index) => index == 0 ? 0 : (index - 1) / _d;

        private int GetFirstChild(int index) => index * _d + 1;

        private void Heapify()
        {
            // for any k >= 1, complete tree with k levels has 1 + d + ... + d^(k-1) = f(d, k) nodes
            // consider a complete tree with n = f(d, k) + m nodes, where 0 <= m < d^k
            // then the tree has m + d^(k-1) - m/d = m*(d-1)/d + d^k/d = [(n-f(d, k))*(d-1) + d^k] / d = 
            // [n*(d-1) + d^k + f(d, k) - f(d, k)*d] / d = [n*(d-1) + 1] / d leaves

            for (int index = (_size * (_d - 1) + 1) / _d - 1; index >=0; --index)
            {
                SiftDown(index);
            }
        }
    }
}