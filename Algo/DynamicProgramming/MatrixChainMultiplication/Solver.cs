using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algo.DynamicProgramming.MatrixChainMultiplication
{
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