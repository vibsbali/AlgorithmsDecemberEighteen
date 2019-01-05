using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Graphs
{
   public class DijkstraSearch : IGraphSearch
   {
      public IGraph Graph { get; }

      public DijkstraSearch(IGraph graphToSearch)
      {
         Graph = graphToSearch;
         _parentMap = new Dictionary<int, int>();
      }

      private Dictionary<int, int> _parentMap;
      public bool AreConnected(int start, int goal)
      {
         if (start >= Graph.NumberOfVertices || goal >= Graph.NumberOfVertices || goal == start)
         {
            throw new ArgumentException();
         }

         var listOfVisited = new List<int>();
         var priorityQueue = new List<Tuple<int, int>>();
         Start = start;
         Goal = goal;
         
         priorityQueue.Add(start, 0);
         
         while (priorityQueue.Count > 0)
         {
            var current = priorityQueue.Deque();

            //check if the neighbour is goal
            if (current.Item1 == goal)
            {
               return true;
            }

            if (listOfVisited.Contains(current.Item1))
            {
               continue;
            }

            listOfVisited.Add(current.Item1);
            foreach (var neighbour in Graph.GetReachableNeighbours(current.Item1).Where(n => !listOfVisited.Contains(n)))
            {
               var weight = Graph.GetEdgeWeight(current.Item1, neighbour);
               _parentMap.AddOrUpdate(neighbour, current.Item1);

               //Cannot return if we find neighbour because it needs to be added to priority queue
               priorityQueue.Add(neighbour, current.Item2 + weight);
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
