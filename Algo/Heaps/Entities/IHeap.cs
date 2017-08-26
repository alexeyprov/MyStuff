using System.Collections.Generic;

namespace Algo.Heaps.Entities
{
    public interface IHeap<TKey, TValue> : IReadOnlyCollection<TValue>
    {
        void Add(TKey key, TValue value);
        
        TValue Peek();

        TValue Extract();
    }
}