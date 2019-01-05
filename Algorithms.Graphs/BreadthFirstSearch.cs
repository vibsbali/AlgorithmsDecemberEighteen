using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
   public class BreadthFirstSearch : IGraphSearch
   {
      public IGraph Graph { get; }

      public BreadthFirstSearch(IGraph graph)
      {
         if (graph == null)
         {
            throw new ArgumentNullException();
         }

         Graph = graph;
         _parentMap = new Dictionary<int, int>(Graph.NumberOfVertices);
      }

      private Dictionary<int, int> _parentMap;

      public bool AreConnected(int start, int goal)
      {
         if (start >= Graph.NumberOfVertices || goal >= Graph.NumberOfVertices || start == goal)
         {
            throw new ArgumentOutOfRangeException();
         }

         Start = start;
         Goal = goal;

         var queue = new Queue<int>();
         var listOfVisited = new List<int>();

         queue.Enqueue(start);
         while (queue.Count > 0)
         {
            var curr = queue.Dequeue();

            if (listOfVisited.Contains(curr))
            {
               return true;
            }

            listOfVisited.Add(curr);
            var neighbours = Graph.GetReachableNeighbours(curr);
            foreach (var neighbour in neighbours.Where(n => !listOfVisited.Contains(n)))
            {
               queue.Enqueue(neighbour);
               _parentMap.AddOrUpdate(neighbour, curr);

               if (neighbour == goal)
               {
                  return true;
               }
            }
         }

         return false;
      }

      public int Goal { get; set; }

      public int Start { get; set; }

      public List<int> GetPath()
      {
         var path = new Stack<int>();
         var current = Goal;

         path.Push(current);
         while (current != Start)
         {
            current = _parentMap[current];
            path.Push(current);
         }

         return path.ToList();
      }
   }
}