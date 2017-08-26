using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algo.Heaps.Entities
{
    public class BinaryHeap<TKey, TValue> : IHeap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly IList<Node> _heap;
        private readonly bool _isMaxHeap;
        private readonly IComparer<TKey> _comparer;
        private int _size;

        public BinaryHeap(
            IEnumerable<TValue> data, 
            bool isMaxHeap,
            Func<TValue, TKey> keyFactory, 
            IComparer<TKey> comparer = null)
        {
            if (keyFactory == null)
            {
                throw new ArgumentNullException(nameof(keyFactory));
            }

            _heap = (data ?? throw new ArgumentNullException(nameof(data)))
                .Select(v => new Node 
                                 { 
                                     Key = keyFactory(v),
                                     Value = v
                                 })
                .ToList();
            _isMaxHeap = isMaxHeap;
            _size = _heap.Count;
            _comparer = comparer ?? Comparer<TKey>.Default;

            Heapify(); 
        }

        public BinaryHeap(
            IEnumerable<TKey> keys,
            IEnumerable<TValue> values,
            bool isMaxHeap,
            IComparer<TKey> comparer = null)
        {
            _heap = (keys ?? throw new ArgumentNullException(nameof(keys)))
                .Zip(
                    values ?? throw new ArgumentNullException(nameof(values)),
                    (k, v) => new Node { Key = k, Value = v })
                .ToList();

            _isMaxHeap = isMaxHeap;
            _size = _heap.Count;
            _comparer = comparer ?? Comparer<TKey>.Default;

            Heapify();
        }

        public int Count => _size;

        public virtual TValue Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            return _heap[0].Value;
        }

        public virtual TValue Extract()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            TValue top = _heap[0].Value;
            _heap[0] = _heap[_size-- - 1];
            SiftDown(0);
            return top;
        }

        public virtual void Add(TKey key, TValue value)
        {
            _heap.Add(new Node { Key = key, Value = value });
            SiftUp(_size++);
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            for (int index = 0; index < _size; ++index)
            {
                yield return _heap[index].Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>)this).GetEnumerator();

        protected Node this[int index] => _heap[index];

        protected void SiftDown(int index, bool useQuickSwap = false)
        {
            int left = GetLeftChild(index);
            int winner = index;

            if (left < _size)
            {
                winner = Compare(index, left);
            }

            int right = GetRightChild(index);
            if (right < _size)
            {
                winner = Compare(winner, right);
            }

            if (winner != index)
            {
                if (useQuickSwap)
                {
                    QuickSwap(index, winner);
                }
                else
                {
                    Swap(index, winner);
                }

                SiftDown(winner, useQuickSwap);
            }
        }

        protected void SiftUp(int index)
        {
            if (index <= 0)
            {
                return;
            }

            int parent = GetParent(index);
            if (Compare(index, parent) != parent)
            {
                Swap(index, parent);
                SiftUp(parent);
            }
        }

        /// <summary>
        /// Compares heap elements at two indices and returns the index of parent element.
        /// I.e., this is the index of bigger element in a max heap, and index of the smaller
        /// element in a min heap.
        /// </summary>
        protected int Compare(int index, int other) =>
            Compare(_heap[index].Key, _heap[other].Key) < 0 ?
                index :
                other;

        /// <summary>
        /// Compares two keys to determine a parent element.
        /// </summary>
        /// <param name="key">First key to compare</param>
        /// <param name="other">Second key to compare</param>
        /// <return>
        /// A negative value, if <paramref name="key" /> should be a parent of <paramref name="other" />.
        /// A positive value, if <paramref name="other" /> should be a parent of <paramref name="key" />.
        /// <c>0</c>, if both keys are equivalent.
        /// </return>
        protected int Compare(TKey key, TKey other) => _comparer.Compare(key, other) *
            (_isMaxHeap ? -1 : 1);

        protected virtual void Swap(int index, int other)
        {
            Debug.Assert(index != other);
            QuickSwap(index, other);
        }

        protected sealed class Node
        {
            public TKey Key;

            public TValue Value;
        }

        private static int GetLeftChild(int index) => index * 2 + 1;

        private static int GetRightChild(int index) => index * 2 + 2;

        private static int GetParent(int index) => (index - 1) / 2;

        private void Heapify()
        {
            for (int index = _heap.Count / 2 - 1; index >= 0; --index)
            {
                // called from constructors, use quick swaps, i.e. no virtual calls
                SiftDown(index, true);
            }
        }


        private void QuickSwap(int index, int other)
        {
            Node temp = _heap[index];
            _heap[index] = _heap[other];
            _heap[other] = temp;
        }
    }
}