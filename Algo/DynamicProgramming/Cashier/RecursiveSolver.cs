using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashier
{
    internal sealed class RecursiveSolver : ISolver
    {
        private readonly IReadOnlyList<Slot> _register;

        public RecursiveSolver(IReadOnlyList<Slot> register)
        {
            _register = register;
        }

        IEnumerable<int> ISolver.FindSolution(int goal)
        {
            IList<int> solution = new int[_register.Count];
            return TrySlot(goal, solution, _register.Count - 1) ? solution : null;
        }

        private bool TrySlot(int goal, IList<int> solution, int slotIndex)
        {
            if (slotIndex < 0)
            {
                return false;
            }

            // fast track: compare goal with maximum available sum
            int maxSum = _register.Take(slotIndex + 1)
                .Aggregate(
                    0,
                    (p, c) => p + c.Nomination * c.Count);
            if (goal > maxSum)
            {
                // if goal is unreachable, give up
                return false;
            }
            else if (goal == maxSum)
            {
                // if goal happens to be max sum, report it as the solution
                for (int index = 0; index <= slotIndex; ++index)
                {
                    solution[index] = _register[index].Count;
                }

                return true;
            }

            // determine max count of coins from the slot to use and 
            // the remainder to pass to the recursive iteration
            int nomination = _register[slotIndex].Nomination;
            int availableCount = _register[slotIndex].Count;

            int maxCount = goal / nomination;
            int remainder = goal % nomination;

            if (maxCount <= availableCount)
            {
                if (remainder == 0)
                {
                    solution[slotIndex] = maxCount;
                    return true;
                }
            }
            else
            {
                remainder += (maxCount - availableCount) * nomination;
                maxCount = availableCount;
            }

            for (int count = maxCount; count >= 0; --count, remainder += nomination)
            {
                solution[slotIndex] = count;
                if (TrySlot(remainder, solution, slotIndex - 1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}