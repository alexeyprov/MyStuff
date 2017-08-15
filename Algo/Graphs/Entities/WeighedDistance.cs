namespace Algo.Graphs.Entities
{
    public struct WeighedDistance
    {
        public int Vertex { get; set; }

        public int Weight { get; set; }

        public override string ToString() =>
            $"{Vertex} ({Weight})";
    }
}