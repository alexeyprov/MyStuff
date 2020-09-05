using System;

namespace Algo.DynamicProgramming.Knapsack
{
    public struct Item
    {
        public Item(int value, int weight)
        {
            Value = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
            Weight = weight > 0 ? weight : throw new ArgumentOutOfRangeException(nameof(value));
        }

        public int Value { get; }

        public int Weight { get; }

        public override string ToString() =>
            $"({Value}, {Weight})";
    }
}