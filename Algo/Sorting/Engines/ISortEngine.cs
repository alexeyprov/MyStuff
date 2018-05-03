using System.Collections.Generic;

namespace Algo.Sorting.Engines
{
    public interface ISortEngine<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> data);
    }
}