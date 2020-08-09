using System;

using Algo.DynamicProgramming.Entities;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal sealed class Solution
    {
        public Solution(FrugalMatrix<int> roots, float expectedValue)
        {
            Roots = roots ?? throw new ArgumentNullException(nameof(roots));
            ExpectedValue = expectedValue > 0.0 ? 
                expectedValue : 
                throw new ArgumentOutOfRangeException(nameof(expectedValue));    
        }

        public FrugalMatrix<int> Roots { get; }

        public float ExpectedValue { get; }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}