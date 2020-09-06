using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.Knapsack
{
    internal sealed class MemoizationSolver : ISolver
    {
        IEnumerable<Item> ISolver.FindSolution(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            RecursiveSeeker seeker = new RecursiveSeeker(problem);
            return seeker.FindSolution();
        }

        private sealed class RecursiveSeeker
        {
            private readonly int _w;
            private readonly int?[,] _m;
            private readonly BitArray _p;
            private readonly IReadOnlyList<Item> _items;

            public RecursiveSeeker(Problem problem)
            {
                _items = problem.Items as IReadOnlyList<Item> ??
                         problem.Items.ToArray();

                _w = problem.MaxWeight;
                _m = new int?[_items.Count, _w];
                _p = new BitArray(_items.Count * _w);
            }

            public IEnumerable<Item> FindSolution()
            {
                CalculateMaxValue(_items.Count - 1, _w - 1);
                return new SolutionReader(_items, _p).Items;
            }

            private int CalculateMaxValue(int i, int j)
            {
                if (i < 0 || j < 0)
                {
                    return 0;
                }

                int? value = _m[i, j];
                if (value.HasValue)
                {
                    return value.Value;
                }

                Item item = _items[i];
                int exclusiveValue = CalculateMaxValue(i - 1, j),
                    inclusiveValue = int.MinValue;
                if (j >= item.Weight - 1)
                {
                    inclusiveValue = CalculateMaxValue(i - 1, j - item.Weight) + item.Value;
                }

                int result;
                if (inclusiveValue > exclusiveValue)
                {
                    _p[i * _w + j] = true;
                    result = inclusiveValue;
                }
                else
                {
                    result = exclusiveValue;
                }

                _m[i, j] = result;
                return result;
            }
        }
    }
}