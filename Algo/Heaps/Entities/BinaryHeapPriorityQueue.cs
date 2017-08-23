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
            _itemLocations = this
                .Select((v, i) => new { Value = v, Index = i })
                .ToDictionary(x => x.Value, x => x.Index);
        }

        public override void Add(TValue item)
        {
            _itemLocations[item] = Count;
            base.Add(item);
        }

        public override TValue Extract()
        {
            TValue value = base.Extract();
            _itemLocations.Remove(value);
            return value;
        }

        public virtual void UpdateKey(TValue item, TKey oldKey)
        {
            int location = _itemLocations[item];
            TKey newKey = GetKey(item);

            int result = Compare(oldKey, newKey);
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
                _itemLocations[base[index]] = index;
                _itemLocations[base[other]] = other;
            }
        }
    }
}