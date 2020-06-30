using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algo.DynamicProgramming.MatrixChainMultiplication
{
    /// <summary>
    /// For a collection of N matrices dimensions: p[0]...p[N], the optimal multiplication cost
    /// is given by the following recurrent solution. For any 1 &lt;= i &lt; j &lt;= n, multiplication cost
    /// m[i, j] = min { m[i, k] + m[k + 1, j] + p[i - 1]*p[k]*p[j]} for any i &lt;= k &lt; j.
    /// </summary>
    internal sealed class Solver
    {
        public Solution FindSolution(IEnumerable<int> matrixDimensions)
        {
            IReadOnlyList<int> dimensions = matrixDimensions as IReadOnlyList<int> ??
                matrixDimensions?.ToArray() ??
                throw new ArgumentNullException(nameof(matrixDimensions));

            int size = dimensions.Count - 1;
            if (size < 3)
            {
                throw new ArgumentException(nameof(matrixDimensions));
            }

            int[,] costMatrix = new int[size, size],
                   pathMatrix = new int[size, size];

            for (int subTaskSize = 2; subTaskSize <= size; ++subTaskSize)
            {
                for (int i = 0; i <= size - subTaskSize; ++i)
                {
                    int j = i + subTaskSize - 1;
                    Debug.Assert(j <= size);

                    int minIndex = i - 1, 
                        minCost = int.MaxValue;
                    for (int k = i + 1; k <= j; ++k)
                    {
                        int cost = costMatrix[i, k - 1] + 
                                   costMatrix[k, j] +
                                   dimensions[i] * dimensions[k] * dimensions[j + 1];
                        if (cost < minCost)
                        {
                            minCost = cost;
                            minIndex = k;
                        }
                    }

                    costMatrix[i, j] = minCost;
                    pathMatrix[i, j] = minIndex;
                }
            }

            return new Solution(pathMatrix, costMatrix[0, size - 1], dimensions);
        }
    }
}