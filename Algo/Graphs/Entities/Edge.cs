namespace Algo.Graphs.Entities
{
    public struct Edge
    {
        public int From { get; set; }

        public int To { get; set; }

        public override string ToString() => $"({From}, {To})";
    }
}