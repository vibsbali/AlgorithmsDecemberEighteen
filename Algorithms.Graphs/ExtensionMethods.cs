using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graphs
{
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

      public static Tuple<int, int> Deque(this List<Tuple<int, int>> sortedDictionary)
      {
         var itemToReturn = sortedDictionary.OrderBy(item => item.Item2).First();
         sortedDictionary.Remove(itemToReturn);

         return itemToReturn;
      }

      public static void Add(this List<Tuple<int, int>> sortedDictionary, int vertex, int weight)
      {
         sortedDictionary.Add(new Tuple<int, int>(vertex, weight));
      }
   }
}