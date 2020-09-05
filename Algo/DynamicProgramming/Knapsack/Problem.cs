using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.Knapsack
{
    public sealed class Problem
    {
        public Problem() :
            this(CreateRandomProblem())
        {
        }

        public Problem(int maxWeight, params Item[] items) :
            this((maxWeight, items))
        {
        }

        private Problem((int MaxWeight, IReadOnlyCollection<Item> Items) data)
        {
            MaxWeight = data.MaxWeight > 0 ? 
                data.MaxWeight :
                throw new ArgumentOutOfRangeException(nameof(data.MaxWeight));

            Items = data.Items.Any() ? 
                data.Items :
                throw new ArgumentException(nameof(data.Items));
        }

        public int MaxWeight { get; }

        public IEnumerable<Item> Items { get; }

        public void Print()
        {
            string items = string.Join(", ", Items);
            Console.WriteLine($"Max Weight: {MaxWeight}. Items: {items}");
        }

        private static (int MaxWeigth, IReadOnlyCollection<Item> Items) CreateRandomProblem()
        {
            Random random = new Random();
            int maxWeight = random.Next(10, 100);

            int count = random.Next(2, maxWeight / 2);
            IReadOnlyCollection<Item> items = Enumerable.Range(0, count)
                .Select(_ => new Item(random.Next(1, 100), random.Next(1, maxWeight)))
                .ToArray();

            return (maxWeight, items);
        }
    }
}