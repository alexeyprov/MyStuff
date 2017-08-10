using System;
using System.Collections.Generic;

namespace Algo.Graphs.Entities
{
    public interface IGraph
    {
        int Size { get; }

        IEnumerable<int> GetAdjacentVertices(int source);

        IGraph Reverse();
    }
}
