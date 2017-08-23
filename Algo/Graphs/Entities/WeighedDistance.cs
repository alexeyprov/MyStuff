using System;

namespace Algo.Graphs.Entities
{
    public struct WeighedDistance
    {
        public WeighedDistance(int vertex, int weight)
        {
            Vertex = vertex >= 0 ? vertex : throw new ArgumentException(nameof(vertex));
            Weight = weight != 0 ? weight : throw new ArgumentException(nameof(weight));
        }

        public int Vertex { get; }

        public int Weight { get; }

        public override string ToString() =>
            $"{Vertex} ({Weight})";
    }
}