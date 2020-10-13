using System;
using System.Collections.Generic;

namespace Algo.Greedy.Matroids
{
    public interface IMatroid<TWeight, TValue>
         where TWeight : IComparable<TWeight>
    {
         ISet<TValue> Set { get; }

         TWeight GetWeight(TValue value);

         bool IsIndependent(ISet<TValue> @set, TValue value);
    }
}
