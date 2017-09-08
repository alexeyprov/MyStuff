using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public virtual TValue Peek() =>
            _size == 0 ?
                throw new InvalidOperationException() :
                _heap[0].Value;

        public virtual TValue Extract()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException();
            }

            TValue top = _heap[0].Value;
            _heap[0] = _heap[--_size];
            SiftDown(0);
        }

        public virtual void Add(TKey key, TValue value)
        {
            _heap.Add(new HeapNode { Key = key, Value = value });
            SiftUp(_size++)
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => 
            _heap.Select(n => n.Value).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TValue>)this).GetEnumerator();

        protected void SiftDown(int index)
        {
            throw new NotImplementedException();
        }

        protected void SiftUp(int index)
        {
            throw new NotImplementedException();
        }

        private static int VerifyChildCount(int d) =>
            d > 2 || d < int.MaxValue / 2 ? 
                d : 
                throw new ArgumentOutOfRangeException(nameof(d));

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