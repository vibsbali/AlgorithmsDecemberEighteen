using System;
using Algorithms.Trees;

namespace Algorithms.ConsoleRunner
{
   class Program
   {
      static void Main(string[] args)
      {
         var bst = new BinarySearchTree<int>();

         bst.Add(100);
         bst.Add(50);
         bst.Add(25);
         bst.Add(75);
         bst.Add(35);
         bst.Add(90);
         bst.Add(200);
         bst.Add(150);
         bst.Add(250);
         bst.Add(225);

         Console.WriteLine(bst.Contains(225));
         Console.WriteLine(bst.Contains(400));
         Console.WriteLine(bst.Contains(-1));

         foreach (var item in bst.SortedEnumerable())
         {
            Console.WriteLine(item);
         }

      }
   }
}
