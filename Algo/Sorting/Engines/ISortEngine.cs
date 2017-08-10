using System;
using System.Collections.Generic;

namespace Algo.Sorting.Engines
{
    public interface ISortEngine<T> where T : IComparable<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> data);
    }
}