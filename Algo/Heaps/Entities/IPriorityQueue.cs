namespace Algo.Heaps.Entities
{
    public interface IPriorityQueue<TKey, TValue> : IHeap<TValue>
    {
        void UpdateKey(TValue item, TKey oldKey);
    }
}