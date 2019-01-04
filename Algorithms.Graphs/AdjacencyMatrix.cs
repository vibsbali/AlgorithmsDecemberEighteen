using System;
using System.Collections.Generic;

namespace Algorithms.Graphs
{
    public class AdjacencyMatrix : IGraph
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

        private int[,] _backingStore;
        private readonly Dictionary<Edge, int> _edgesWithWeights;
        public AdjacencyMatrix(int size, bool isUndirected)
        {
            if (size == 0)
            {
                throw new InvalidOperationException();
            }

            NumberOfVertex = size;
            _backingStore = new int[NumberOfVertex,NumberOfVertex];
            _edgesWithWeights = new Dictionary<Edge, int>(NumberOfVertex);
            IsUndirected = isUndirected;
        }

        public int NumberOfVertex { get; private set; }
        public int NumberOfEdges { get; private set; }
        public bool IsUndirected { get; }
        public void AddVertex()
        {
            NumberOfVertex++;
            if (NumberOfVertex > _backingStore.GetUpperBound(0))
            {
                var temp = new int[NumberOfVertex * 2, NumberOfVertex * 2];
                foreach (var edgesWithWeight in _edgesWithWeights)
                {
                    var edge = edgesWithWeight.Key;

                    var startingVertex = edge.StartingVertex;
                    var endingVertex = edge.EndingVertex;

                    temp[startingVertex, endingVertex] = 1;
                }

                _backingStore = temp;
            }

        }

        public void AddEdge(int vertexOne, int vertexTwo, int weight = 0)
        {
            if (vertexOne >= NumberOfVertex || vertexTwo >= NumberOfVertex)
            {
                throw new ArgumentOutOfRangeException();
            }

            _backingStore[vertexOne, vertexTwo] = 1;

            var edge = new Edge(vertexOne, vertexTwo);
            if (_edgesWithWeights.ContainsKey(edge))
            {
                _edgesWithWeights[edge] = weight;
            }

            _edgesWithWeights.Add(edge, weight);
            ++NumberOfEdges;

            if (IsUndirected)
            {
                AddEdge(vertexTwo, vertexOne, weight);
            }
        }

        public IEnumerable<int> GetNeighbours(int vertex)
        {
            if (vertex >= NumberOfVertex)
            {
                throw new ArgumentOutOfRangeException();
            }

            var listOfNeighbours = new List<int>();
            for (int i = 0; i < NumberOfVertex; i++)
            {
                if (_backingStore[vertex, i] == 1)
                {
                    listOfNeighbours.Add(i);
                }        
            }

            return listOfNeighbours;
        }

        public int GetEdgeWeight(int firstVertex, int secondVertex)
        {
            var edge = new Edge(firstVertex, secondVertex);
            if (_edgesWithWeights.ContainsKey(edge))
            {
                return _edgesWithWeights[edge];
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
