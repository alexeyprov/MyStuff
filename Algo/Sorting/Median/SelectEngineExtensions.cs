using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Sorting.Median
{
    public static class SelectEngineExtensions
    {
        public static T FindMedian<T>(this ISelectEngine<T> engine, IEnumerable<T> data)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            IReadOnlyCollection<T> collection = data as IReadOnlyCollection<T> ??
                data?.ToArray() ??
                throw new ArgumentNullException(nameof(data));

            return engine.FindByRank(data, (collection.Count - 1) / 2);
        }
    }
}