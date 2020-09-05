using System.Collections.Generic;

namespace Algo.DynamicProgramming.Knapsack
{
    public interface ISolver
    {
        IEnumerable<Item> FindSolution(Problem problem);
    }
}