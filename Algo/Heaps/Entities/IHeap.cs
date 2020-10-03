using System.Collections.Generic;

namespace Algo.Heaps.Entities
{
    public interface IHeap<TKey, TValue> : IReadOnlyCollection<TValue>
    {
        void Add(TKey key, TValue value);
        
        (TKey, TValue) Peek();

        (TKey, TValue) Extract();
    }
}