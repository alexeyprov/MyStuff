using System.Collections.Generic;

namespace PoissonJugs
{
    internal interface ISolver
    {
        IEnumerable<IList<int>> FindSolution(IList<int> volumes, int goal);
    }
}