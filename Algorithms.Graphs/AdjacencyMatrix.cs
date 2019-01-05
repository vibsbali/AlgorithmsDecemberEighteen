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

         NumberOfVertices = size;
         _backingStore = new int[NumberOfVertices, NumberOfVertices];
         _edgesWithWeights = new Dictionary<Edge, int>(NumberOfVertices);
         IsUndirected = isUndirected;
      }

      public int NumberOfVertices { get; private set; }
      public int NumberOfEdges { get; private set; }
      public bool IsUndirected { get; }
      public void AddVertex()
      {
         if (NumberOfVertices >= _backingStore.GetUpperBound(0))
         {
            var temp = new int[NumberOfVertices * 2, NumberOfVertices * 2];
            for (int i = 0; i < NumberOfVertices; i++)
            {
               for (int j = 0; j < NumberOfVertices; j++)
               {
                  temp[i, j] = _backingStore[i, j];
               }
            }

            _backingStore = temp;
         }

         NumberOfVertices++;
      }

      public void AddEdge(int startingVertex, int endingVertex, int weight = 0)
      {
         AddEdgeInternal(startingVertex, endingVertex, weight);

         if (IsUndirected)
         {
            AddEdgeInternal(endingVertex, startingVertex, weight);
         }
      }

      private void AddEdgeInternal(int startingVertex, int endingVertex, int weight)
      {
         if (startingVertex >= NumberOfVertices || endingVertex >= NumberOfVertices)
         {
            throw new ArgumentOutOfRangeException();
         }

         _backingStore[startingVertex, endingVertex] = 1;

         var edge = new Edge(startingVertex, endingVertex);
         if (_edgesWithWeights.ContainsKey(edge))
         {
            _edgesWithWeights[edge] = weight;
         }
         else
         {
            _edgesWithWeights.Add(edge, weight);
         }

         ++NumberOfEdges;
      }

      public IEnumerable<int> GetReachableNeighbours(int vertex)
      {
         if (vertex >= NumberOfVertices)
         {
            throw new ArgumentOutOfRangeException();
         }

         var listOfNeighbours = new List<int>();
         for (int i = 0; i < NumberOfVertices; i++)
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
