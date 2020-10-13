using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Greedy.Matroids
{
    public static class MatroidExtensions
    {
        public static ISet<TValue> FindOptimum<TWeight, TValue>(this IMatroid<TWeight, TValue> matroid)
            where TWeight : IComparable<TWeight>
        {
            if (matroid == null)
            {
                throw new ArgumentNullException(nameof(matroid));
            }

            IDictionary<TWeight, TValue> weightedValues = new SortedDictionary<TWeight, TValue>();
            foreach (TValue value in matroid.Set)
            {
                weightedValues[matroid.GetWeight(value)] = value;
            }

            ISet<TValue> result = new HashSet<TValue>();
            foreach (TWeight weight in weightedValues.Keys.Reverse())
            {
                TValue value = weightedValues[weight];
                if (matroid.IsIndependent(result, value))
                {
                    result.Add(value);
                }
            }

            return result;
        }
    }
}
