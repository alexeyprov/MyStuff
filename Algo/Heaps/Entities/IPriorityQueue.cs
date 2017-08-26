namespace Algo.Heaps.Entities
{
    public interface IPriorityQueue<TKey, TValue> : IHeap<TKey, TValue>
    {
        void UpdateKey(TValue value, TKey key);
    }
}