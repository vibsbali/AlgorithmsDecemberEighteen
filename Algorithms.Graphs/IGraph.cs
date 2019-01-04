using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graphs
{
    public interface IGraph
    {
        int NumberOfVertex { get; }
        int NumberOfEdges { get; }
        bool IsUndirected { get; }
        void AddVertex();
        void AddEdge(int vertexOne, int vertexTwo, int weight);
        IEnumerable<int> GetNeighbours(int vertex);
        int GetEdgeWeight(int firstVertex, int secondVertex);
    }
}
