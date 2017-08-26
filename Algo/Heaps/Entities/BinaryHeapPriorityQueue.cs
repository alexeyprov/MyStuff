using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Heaps.Entities
{
    public class BinaryHeapPriorityQueue<TKey, TValue> : 
        BinaryHeap<TKey, TValue>,
        IPriorityQueue<TKey, TValue>
            where TKey : IComparable<TKey>
    {
        private readonly IDictionary<TValue, int> _itemLocations;

        public BinaryHeapPriorityQueue(
            IEnumerable<TValue> data, 
            bool isMaxHeap,
            Func<TValue, TKey> keyFactory,
            IComparer<TKey> comparer = null) :
                base(data, isMaxHeap, keyFactory, comparer)
        {
            _itemLocations = GetLocations();
        }

        public BinaryHeapPriorityQueue(
            IEnumerable<TKey> keys,
            IEnumerable<TValue> values,
            bool isMaxHeap,
            IComparer<TKey> comparer = null) :
                base(keys, values, isMaxHeap, comparer)
        {
            _itemLocations = GetLocations();
        }

        public override void Add(TKey key, TValue value)
        {
            _itemLocations[value] = Count;
            base.Add(key, value);
        }

        public override TValue Extract()
        {
            TValue value = base.Extract();
            _itemLocations.Remove(value);
            return value;
        }

        public virtual void UpdateKey(TValue value, TKey key)
        {
            int location = _itemLocations[value];
            TKey oldKey = base[location].Key;

            int result = Compare(oldKey, key);
            if (result < 0)
            {
                SiftDown(location);
            }
            else if (result > 0)
            {
                SiftUp(location);
            }
        }

        protected override void Swap(int index, int other)
        {
            base.Swap(index, other);

            if (_itemLocations != null)
            {
                _itemLocations[base[index].Value] = index;
                _itemLocations[base[other].Value] = other;
            }
        }

        private IDictionary<TValue, int> GetLocations() =>
            this.Select((v, i) => new { Value = v, Index = i })
                .ToDictionary(x => x.Value, x => x.Index);
    }
}