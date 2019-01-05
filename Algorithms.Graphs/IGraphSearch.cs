using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graphs
{
   public interface IGraphSearch
   {
      IGraph Graph { get; }
      bool AreConnected(int start, int goal);
      List<int> GetPath();
   }

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

   public class DepthFirstSearch : IGraphSearch
   {
      public IGraph Graph { get; }
      public bool AreConnected(int start, int goal)
      {
         if (start >= Graph.NumberOfVertices || goal >= Graph.NumberOfVertices || start == goal)
         {
            throw new ArgumentOutOfRangeException();
         }

         Start = start;
         Goal = goal;

         var stack = new Stack<int>();
         var listOfVisited = new List<int>();

         stack.Push(start);
         while (stack.Count > 0)
         {
            var curr = stack.Pop();
          
            if (listOfVisited.Contains(curr))
            {
               continue;
            }

            listOfVisited.Add(curr);
            var neighbours = Graph.GetReachableNeighbours(curr);
            foreach (var neighbour in neighbours.Where(n => !listOfVisited.Contains(n)))
            {
               stack.Push(neighbour);
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

      public DepthFirstSearch(IGraph graph)
      {
         if (graph == null)
         {
            throw new ArgumentNullException();
         }

         Graph = graph;
         _parentMap = new Dictionary<int, int>(Graph.NumberOfVertices);
      }

      private Dictionary<int, int> _parentMap;

      public List<int> GetPath(int start, int goal)
      {
         if (start >= Graph.NumberOfVertices || goal >= Graph.NumberOfVertices || start == goal)
         {
            throw new ArgumentOutOfRangeException();
         }

         var stack = new Stack<int>();
         var listOfVisited = new List<int>();
         int curr = 0;

         stack.Push(start);
         while (stack.Count > 0)
         {
            curr = stack.Pop();
            if (curr == goal)
            {
               break;
            }

            if (listOfVisited.Contains(curr))
            {
               continue;
            }

            listOfVisited.Add(curr);
            var neighbours = Graph.GetReachableNeighbours(curr);
            foreach (var neighbour in neighbours.Where(n => !listOfVisited.Contains(n)))
            {
               stack.Push(neighbour);
               _parentMap.AddOrUpdate(neighbour, curr);

               //Optimisation only
               if (neighbour == goal)
               {
                  //Clear the stack so that the loop exits
                  stack.Clear();
                  curr = goal;
                  break;
               }
            }
         }

         var path = new Stack<int>();
         if (curr == goal)
         {
            path.Push(curr);
            while (curr != start)
            {
               curr = _parentMap[curr];
               path.Push(curr);
            }

            return path.ToList();
         }

         return new List<int>();
      }
   }
   public static class ExtensionMethods
   {
      public static void AddOrUpdate(this Dictionary<int, int> dictionary, int neighbour, int parent)
      {
         if (dictionary.ContainsKey(neighbour))
         {
            dictionary.Remove(neighbour);
         }

         dictionary.Add(neighbour, parent);
      }
   }
}
