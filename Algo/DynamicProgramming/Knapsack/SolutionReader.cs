using System;
using System.Collections;
using System.Collections.Generic;

namespace Algo.DynamicProgramming.Knapsack
{
    internal sealed class SolutionReader
    {
        private readonly IReadOnlyList<Item> _items;
        private readonly BitArray _path;

        public SolutionReader(IReadOnlyList<Item> items, BitArray path)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public IEnumerable<Item> Items => EnumerateItems();

        private IEnumerable<Item> EnumerateItems()
        {
            int w = _path.Length / _items.Count;
            for (int i = _items.Count - 1, j = w - 1; i >= 0 && j >= 0; --i)
            {
                if (_path[i * w + j])
                {
                    Item item = _items[i];
                    j -= item.Weight;
                    yield return item;
                }
            }
        }
    }
}