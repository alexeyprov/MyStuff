using System;
using System.Collections.Generic;

using Algo.Graphs.Entities;

namespace Algo.Graphs.Paths
{
    public abstract class BfsAlgorithmBase
    {
        private readonly IGraph _graph;
        private readonly Queue<Edge> _queue;
        private readonly ICollection<int> _visited;

        public BfsAlgorithmBase(IGraph graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _queue = new Queue<Edge>();
            _visited = new HashSet<int>();
        }

        public virtual void Run()
        {
            _queue.Clear();
            Enqueue(0);
            
            while (_queue.Count != 0)
            {
                Edge head = _queue.Dequeue();
                if (!_visited.Contains(head.To))
                {
                    Explore(head);
                    _visited.Add(head.To);
                }
            }
        }

        protected virtual void Explore(Edge edge)
        {
            foreach (int child in _graph.GetAdjacentVertices(edge.To))
            {
                Enqueue(child, edge.To);
            }
        }

        protected void Enqueue(int vertex, int parent = -1)
        {
            _queue.Enqueue(new Edge { From = parent, To = vertex });
        }
    }    
}