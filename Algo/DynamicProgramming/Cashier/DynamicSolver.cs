using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Cashier
{
    // Define: G - final goal, g - intermediate goal
    // m - total amount of coins
    // n[i] is coin nomination, where i is in [1..m]
    // C(g, i) - minimal amount of coins from the set {1..i} resulting in g
    // Then, C(g, i) = min { C(g - n[i], i - 1) + 1; C(g, i - 1) } is a recursive dependency
    // Boundary conditions:
    // a. For any g in [1..G], any i in [1..m]: C(g, i) = 1, if n[i] = g
    // b. For any i in [1..m], h < 0: C(h, i) = inf
    // Sought solution is C(G, m)

    // E.g. n = (2, 3, 5); G = 7, then:
    // C(7, 3) = min { C(7 - 5, 2) + 1; C(7, 2) }
    // C(2, 2) = min { C(2 - 3, 1) + 1 = inf; C(2, 1) }
    // C(7, 2) = min { C(7 - 3, 1) + 1; C(7; 1) }
    // C(2, 1) = 1
    // C(4, 1) = inf
    // C(7, 1) = inf
    // C(7, 2) = inf
    // C(2, 2) = 1
    // C(7, 3) = min { 1 + 1, inf } = 2

    // The cost matrix (7x3) is (m = max value = inf):
    // m m m
    // 1 1 1
    // m 1 1
    // m m m
    // m 2 1
    // m m m
    // m m 2
    internal sealed class DynamicSolver : ISolver
    {
        private readonly IReadOnlyList<int> _coins;
        private readonly IReadOnlyList<Slot> _register;

        public DynamicSolver(IReadOnlyList<Slot> register)
        {
            _coins = register
                .SelectMany(s => Enumerable.Repeat(s.Nomination, s.Count))
                .ToArray();

            _register = register;
        }

        IEnumerable<int> ISolver.FindSolution(int goal)
        {
            int[,] costMatrix = new int[goal, _coins.Count];

            BitArray pathMatrix = FillCostMatrix(costMatrix, goal);

#if TROUBLESHOOTING
            SaveCostMatrix(costMatrix);
            SavePathMatrix(pathMatrix);
#endif

            return costMatrix[goal - 1, _coins.Count - 1] == int.MaxValue ? 
                null : 
                ReadSolution(pathMatrix);
        }

        private BitArray FillCostMatrix(int[,] costMatrix, int goal)
        {
            BitArray pathMatrix = new BitArray(goal * _coins.Count);

            // fill the boundary conditions
            for (int g = 1; g <= goal; ++g)
            {   
                for (int i = 0; i < _coins.Count; ++i)
                {
                    if (_coins[i] == g)
                    {
                        costMatrix[g - 1, i] = 1;
                        pathMatrix[(g - 1) * _coins.Count + i] = true;
                    }
                    else
                    {
                        costMatrix[g - 1, i] = int.MaxValue;
                    }
                }
            }
            
            // fill the matrix by columns, starting with the second one
            for (int i = 1; i < _coins.Count; ++i)
            {
                int currentCoin = _coins[i];
                for (int g = 1; g < currentCoin; ++g)
                {
                    costMatrix[g - 1, i] = costMatrix[g - 1, i - 1];    
                }
                
                for (int g = currentCoin + 1; g <= goal; ++g)
                {
                    int inclusiveCost = costMatrix[g - currentCoin - 1, i - 1];
                    if (inclusiveCost != int.MaxValue)
                    {
                        inclusiveCost++;
                    }

                    int exclusiveCost = costMatrix[g - 1, i - 1];
                    if (inclusiveCost < exclusiveCost)
                    {
                        pathMatrix[(g - 1) * _coins.Count + i] = true;
                        costMatrix[g - 1, i] = inclusiveCost;
                    }
                    else
                    {
                        costMatrix[g - 1, i] = exclusiveCost;
                    }
                }
            }

            return pathMatrix;
        }

        private IEnumerable<int> ReadSolution(BitArray pathMatrix)
        {
            IDictionary<int, int> solution = new SortedDictionary<int, int>();

            int goal = pathMatrix.Length / _coins.Count;
            int coinIndex;
            for (coinIndex = _coins.Count - 1; coinIndex >= 0 && goal > 0; --coinIndex)
            {
                int pathIndex = (goal - 1) * _coins.Count + coinIndex;
                int nomination = _coins[coinIndex];
                int count = 0;

                bool isKnownNomination = solution.TryGetValue(nomination, out count);

                if (pathMatrix[pathIndex])
                {
                    solution[nomination] = count + 1;
                    goal -= nomination;
                }
                else if (!isKnownNomination)
                {
                    solution[nomination] = 0;
                }
            }

            // process the remainder (not used coins)
            for (; coinIndex >= 0; --coinIndex)
            {
                int nomination = _coins[coinIndex];
                if (!solution.ContainsKey(nomination))
                {
                    solution[nomination] = 0;
                }
            }

            return solution.Values;
        }

        private void SaveCostMatrix(int[,] costMatrix)
        {
            int rowCount = costMatrix.GetLength(0);
            int columnCount = costMatrix.GetLength(1);

            using (TextWriter writer = new StreamWriter("CostMatrix.csv"))
            {
                for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
                {
                    for (int columnIndex = 0; columnIndex < columnCount - 1; ++columnIndex)
                    {
                        int value = costMatrix[rowIndex, columnIndex];
                        if (value != int.MaxValue)
                        {
                            writer.Write(value);
                        }
                        writer.Write(',');
                    }

                    int lastValue = costMatrix[rowIndex, columnCount - 1];
                    if (lastValue != int.MaxValue)
                    {
                        writer.Write(lastValue);
                    }
                    
                    writer.Write('\n');
                }
            }
        }

        private void SavePathMatrix(BitArray pathMatrix)
        {
            using (TextWriter writer = new StreamWriter("PathMatrix.csv"))
            {
                int counter = 0;
                int rowLength = _coins.Count;

                foreach (bool flag in pathMatrix)
                {
                    writer.Write(flag ? '1' : '0');
                    writer.Write((counter++ % rowLength) == (rowLength - 1) ? '\n' : ',');
                }
            }
        }
    }
}
