using System.Collections.Generic;

namespace Algo.Sorting.Median
{
    public interface ISelectEngine<T>
    {
        T FindByRank(IEnumerable<T> data, int rank);
    }
}