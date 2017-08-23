using System;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;

namespace Algo.Graphs.DepthFirstSearch
{
    public abstract class DfsAlgorithmBase
    {
        private readonly IGraph _graph;
        private readonly VertexTiming[] _times;
        private int _clock;

        protected DfsAlgorithmBase(IGraph graph)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _times = new VertexTiming[_graph.Size];
        }

        public virtual void Run(IEnumerable<int> vertices = null)
        {
            _clock = 0;
            vertices = vertices ?? Enumerable.Range(0, _graph.Size);
            vertices = (vertices as IReadOnlyCollection<int>) ?? vertices.ToArray();

            foreach (int index in vertices)
            {
                _times[index].Reset();
            }

            foreach (int source in vertices)
            {
                Explore(source, true);
            }
        }

        protected IGraph Graph => _graph;

        protected IReadOnlyList<VertexTiming> Times => _times;

        protected virtual void PreVisit(int vertex, bool isNewComponent)
        {
            _times[vertex].StartTime = ++_clock;
        }

        protected virtual void PreExplore(int vertex, int parent)
        {
        }

        protected virtual void PostExplore(int vertex, int parent)
        {
        }

        protected virtual void PostVisit(int vertex, bool isNewComponent)
        {
            _times[vertex].EndTime = ++_clock;
        }

        private void Explore(int vertex, bool isNewComponent)
        {
            if (_times[vertex].StartTime != 0)
            {
                return;
            }

            PreVisit(vertex, isNewComponent);

            foreach (int child in _graph.GetAdjacentVertices(vertex))
            {
                PreExplore(child, vertex);
                Explore(child, false);
                PostExplore(child, vertex);
            }

            PostVisit(vertex, isNewComponent);
        }
    }
}
