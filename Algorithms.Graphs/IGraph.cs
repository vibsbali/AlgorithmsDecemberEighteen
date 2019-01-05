using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public interface IGraph
    {
        int NumberOfVertices { get; }
        int NumberOfEdges { get; }
        bool IsUndirected { get; }
        void AddVertex();
        void AddEdge(int startingVertex, int endingVertex, int weight);
        IEnumerable<int> GetReachableNeighbours(int vertex);
        int GetEdgeWeight(int firstVertex, int secondVertex);
    }
}
