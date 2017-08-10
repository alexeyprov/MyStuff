using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashier
{
    internal sealed class Problem
    {
        public Problem(IReadOnlyList<int> nominations)
        {
            Random random = new Random();
            Slot[] register = new Slot[nominations.Count];

            int maxSum = 0;

            for (int index = 0; index < nominations.Count; ++index)
            {
                Slot slot = new Slot
                {
                    Nomination = nominations[index],
                    Count = 1 + random.Next(50)
                };

                maxSum += slot.Nomination * slot.Count;

                register[index] = slot;
            }

            int halfSum = maxSum / 2;
            Register = register;
            Goal = halfSum + random.Next(halfSum);
        }

        public Problem(IReadOnlyList<Slot> register, int goal)
        {
            Register = register;
            Goal = goal;
        }

        public IReadOnlyList<Slot> Register
        {
            get;
        }

        public int Goal
        {
            get;
        }

        public override string ToString()
        {
            return string.Format(
                "Goal: {0}; Register: {1}",
                Goal,
                string.Join(
                    " | ",
                    Register.Select(s => $"{s.Count} x {s.Nomination}")));
        }
    }
}
