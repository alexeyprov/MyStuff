namespace Algo.Sorting.Engines
{
    public sealed class CountingSortEngine : CountingSortEngineBase<int>
    {
        protected override int GetItemKey(int item) => item;
    }
}