using System.Collections.Generic;

namespace Algo.Graphs.Entities
{
    public interface IWeighedGraph
    {
        IEnumerable<WeighedDistance> GetAdjacentDistances(int vertex);
    }
}