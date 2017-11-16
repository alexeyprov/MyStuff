using System.Collections.Generic;

namespace Algo.Trees.SearchTrees
{
    public interface ISearchTree<T> : ICollection<T>
    {
        T Max();

        T Min();
    }
}
