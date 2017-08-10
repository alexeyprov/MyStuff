using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using Algo.Graphs.Entities;

namespace Algo.Graphs.DepthFirstSearch
{
    public abstract class SearchTreeBase : DfsAlgorithmBase
    {
        public SearchTreeBase(IGraph graph) : base(graph)
        {
        }

        protected override void PreExplore(int vertex, int parent)
        {
            if (vertex == parent)
            {
                VisitSelfLoop(vertex);
                return;
            }

            VertexTiming sourceTime = Times[parent];
            VertexTiming destTime = Times[vertex];
            
            // parent must be pre-visited
            Debug.Assert(sourceTime.StartTime != 0);
            // parent must not be post-visited yet. otherwise, what are we doing here?
            Debug.Assert(sourceTime.EndTime == 0);
            
            if (destTime.StartTime == 0)
            {
                // not discovered yet - tree edge
                VisitTreeEdge(vertex, parent);
            }
            else 
            {
                // cannot have different vertexes visited at the same time
                Debug.Assert(destTime.StartTime != sourceTime.StartTime);

                if (destTime.StartTime < sourceTime.StartTime)
                {
                     // destination (vertex) was already discovered and it was before
                     // the current node (parent)
                     if (destTime.EndTime == 0)
                     {
                         // at the same time, destination is not post-visited yet
                         VisitBackwardEdge(vertex, parent);
                     }
                     else
                     {
                         VisitCrossEdge(vertex, parent);
                     }
                }
                else
                {
                    // destination was already discovered after source
                    Debug.Assert(destTime.EndTime != 0);
                    VisitForwardEdge(vertex, parent);
                }
            }
        }

        protected virtual void VisitTreeEdge(int vertex, int parent)
        {
        }

        protected virtual void VisitForwardEdge(int vertex, int parent)
        {
        }

        protected virtual void VisitBackwardEdge(int vertex, int parent)
        {
        }

        protected virtual void VisitCrossEdge(int vertex, int parent)
        {
        }

        protected virtual void VisitSelfLoop(int vertex)
        {
        }
    }
}
