namespace Algo.Graphs.DepthFirstSearch
{
    public struct VertexTiming
    {
        public int StartTime; // { get; set; }

        public int EndTime; // { get; set; }

        public void Reset()
        {
            StartTime = 0;
            EndTime = 0;
        }
    }
}