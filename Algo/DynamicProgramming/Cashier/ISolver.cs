using System.Collections.Generic;

namespace Cashier
{
    internal interface ISolver
    {
        IEnumerable<int> FindSolution(int goal);
    }
}