using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Graphs
{
    public class AdjacencyList : IGraph
    {
        internal struct Edge
        {
            internal int StartingVertex;
            internal int EndingVertex;

            public Edge(int startingVertex, int endingVertex)
            {
                StartingVertex = startingVertex;
                EndingVertex = endingVertex;
            }
        }

        internal Dictionary<int, List<int>> _backingList;

        public AdjacencyList(int size, bool isUndirected)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            NumberOfVertex = size;
            IsUndirected = isUndirected;
            EdgesWithWeights = new Dictionary<Edge, int>();
        }

        public int NumberOfVertex { get; private set; }
        public int NumberOfEdges { get; private set; }
        public bool IsUndirected { get; private set; }
        public void AddVertex()
        {
            throw new NotImplementedException();
        }

        internal Dictionary<Edge, int> EdgesWithWeights { get; private set; }

        public void AddEdge(int vertexOne, int vertexTwo, int weight)
        {
            if (vertexOne >= NumberOfVertex || vertexTwo >= NumberOfVertex)
            {
                throw new ArgumentOutOfRangeException();
            }

            var edge = new Edge(vertexOne, vertexTwo);
            if (_backingList.ContainsKey(vertexOne))
            {
                _backingList[vertexOne].Add(vertexTwo);
            }
            else
            {
                _backingList.Add(vertexOne, new List<int> { vertexTwo});
            }

            if (EdgesWithWeights.ContainsKey(edge))
            {
                EdgesWithWeights[edge] = weight;
            }
            else
            {
                EdgesWithWeights.Add(edge, weight);
            }

            ++NumberOfEdges;
        }

        public IEnumerable<int> GetNeighbours(int vertex)
        {
            throw new NotImplementedException();
        }

        public int GetEdgeWeight(int firstVertex, int secondVertex)
        {
            if (vertexOne >= NumberOfVertex || vertexTwo >= NumberOfVertex)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}

