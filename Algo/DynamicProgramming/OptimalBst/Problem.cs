using System;
using System.Collections.Generic;
using System.Linq;

using Algo.DynamicProgramming.Entities;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal sealed class Problem
    {
        public Problem()
        {
            throw new NotImplementedException();
        }

        public Problem(IEnumerable<float> realKeys, IEnumerable<float> dummyKeys)
        {
            RealKeyProbabilities = new BasedArray<float>(realKeys, 1);
            DummyKeyProbabilities = dummyKeys?.ToArray() ?? 
                                    throw new ArgumentNullException(nameof(dummyKeys));
        }

        public IReadOnlyList<float> RealKeyProbabilities { get; }

        public IReadOnlyList<float> DummyKeyProbabilities { get; }

        public int Size => RealKeyProbabilities.Count;
    }
}
