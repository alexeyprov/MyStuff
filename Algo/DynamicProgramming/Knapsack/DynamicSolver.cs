using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.Knapsack
{
    internal sealed class DynamicSolver : ISolver
    {
        IEnumerable<Item> ISolver.FindSolution(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            IReadOnlyList<Item> items = problem.Items as IReadOnlyList<Item> ??
                                        problem.Items.ToArray();

            int w = problem.MaxWeight;
            int[,] m = new int[items.Count, w];
            BitArray p = new BitArray(items.Count * w);

            Item firstItem = items[0];
            for (int j = firstItem.Weight - 1; j < w; ++j)
            {
                m[0, j] = firstItem.Value;
                p[j] = true;
            }

            for (int i = 1, stride = w; i < items.Count; ++i, stride += w)
            {
                Item item = items[i];
                int minWeight = item.Weight;

                for (int j = 0, max = minWeight - 1; j < max; ++j)
                {
                    m[i, j] = m[i - 1, j];
                }

                for (int j = minWeight - 1; j < w; ++j)
                {
                    int inclusiveValue = item.Value + 
                        (j >= minWeight ? m[i - 1, j - minWeight] : 0);
                    int exclusiveValue = m[i - 1, j];
                    if (inclusiveValue > exclusiveValue)
                    {
                        m[i, j] = inclusiveValue;
                        p[stride + j] = true;
                    }
                    else
                    {
                        m[i, j] = exclusiveValue;
                    }
                }
            }

            return new SolutionReader(items, p).Items;
        }
    }
}