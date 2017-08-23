using System.Collections.Generic;

namespace Algo.Heaps.Entities
{
    public interface IHeap<T> : IReadOnlyCollection<T>
    {
        void Add(T value);
        
        T Peek();

        T Extract();
    }
}