using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.DynamicProgramming.AssemblyLineBalancing
{
    internal sealed class Solution
    {
        public Solution(int size)
        {
            if (size < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            Costs = new int[2, size];
            Paths = (byte[,])
                Array.CreateInstance(typeof(byte), new int[] { 2, size - 1 }, new int[] { 0, 1 });
        }

        public int[,] Costs { get; }
        
        public byte[,] Paths { get; }

        public IEnumerable<(byte line, int cost)> GetOptimalPath()
        {
            int size = Costs.GetLength(1);
            ICollection<(byte line, int cost)> path = new List<(byte, int)>(size);

            byte lastLine;
            int totalCost;
            if (Costs[0, size - 1] < Costs[1, size - 1])
            {
                totalCost = Costs[0, size - 1];
                lastLine = 0;
            }
            else
            {
                totalCost = Costs[1, size - 1];
                lastLine = 1;
            }

            path.Add((lastLine, totalCost));

            for (int stageIndex = size - 2; stageIndex >= 0; --stageIndex)
            {
                byte usedLine = Paths[lastLine, stageIndex + 1];
                path.Add((usedLine, Costs[usedLine, stageIndex]));
                lastLine = usedLine;
            }

            return path.Reverse();
        }
    }
}