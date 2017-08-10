namespace Algo.Graphs.TestConsole
{
    internal static class Program
    {
        private static void Main()
        {
            DfsTests dfsTests = new DfsTests();
            dfsTests.SearchForCycles();
            dfsTests.Linearize();
            dfsTests.Decompose();
        }
    }
}
