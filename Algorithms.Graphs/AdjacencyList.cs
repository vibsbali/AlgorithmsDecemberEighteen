using System;
using System.Collections.Generic;
using System.Linq;

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

         NumberOfVertices = size;
         IsUndirected = isUndirected;
         _backingList = new Dictionary<int, List<int>>(size);
         for (int i = 0; i < size; i++)
         {
            _backingList.Add(i, new List<int>());
         }

         _edgesWithWeights = new Dictionary<Edge, int>();
      }

      public int NumberOfVertices { get; private set; }
      public int NumberOfEdges { get; private set; }
      public bool IsUndirected { get; }
      public void AddVertex()
      {
         _backingList.Add(NumberOfVertices, new List<int>());
         ++NumberOfVertices;
      }

      internal Dictionary<Edge, int> _edgesWithWeights { get; private set; }

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

         var edge = new Edge(startingVertex, endingVertex);

         if (!_backingList[startingVertex].Contains(endingVertex))
            _backingList[startingVertex].Add(endingVertex);

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

         var listOfEdges = _backingList[vertex].ToList();
         return listOfEdges;
      }

      public int GetEdgeWeight(int firstVertex, int secondVertex)
      {
         if (firstVertex >= NumberOfVertices || secondVertex >= NumberOfVertices)
         {
            throw new ArgumentOutOfRangeException();
         }

         var edge = new Edge(firstVertex, secondVertex);
         if (_edgesWithWeights.ContainsKey(edge))
         {
            return _edgesWithWeights[edge];
         }

         throw new InvalidOperationException("No edge found");
      }
   }
}

