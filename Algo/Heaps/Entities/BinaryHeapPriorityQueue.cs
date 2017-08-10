using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public override int Add(TValue item)
        {
            int location = base.Add(item);

            Debug.Assert(location >= 0);
            Debug.Assert(location < Count);
            _itemLocations[item] = location;

            return location;
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
            int newLocation;
            if (result < 0)
            {
                newLocation = SiftDown(location);
            }
            else if (result > 0)
            {
                newLocation = SiftUp(location);
            }
            else
            {
                return;
            }

            Debug.Assert(newLocation >= 0);
            Debug.Assert(newLocation < Count);
            Debug.Assert(newLocation != location);

            _itemLocations[item] = newLocation;
        }
    }
}