using System.Collections.Generic;

namespace Algo.Graphs.Entities
{
    public interface IWeighedGraph : IGraph
    {
        IEnumerable<WeighedDistance> GetDistances(int vertex);
    }
}