using System;
using System.Collections.Generic;
using Algorithms.Sorting;
using Algorithms.Trees;

namespace Algorithms.ConsoleRunner
{
   class Program
   {
      static void Main(string[] args)
        {
            //var bst = new BinarySearchTree<int>();

            //bst.Add(100);
            //bst.Add(50);
            //bst.Add(25);
            //bst.Add(75);
            //bst.Add(35);
            //bst.Add(90);
            //bst.Add(200);
            //bst.Add(150);
            //bst.Add(250);
            //bst.Add(225);

            //Console.WriteLine(bst.Contains(225));
            //Console.WriteLine(bst.Contains(400));
            //Console.WriteLine(bst.Contains(-1));

            //bst.Remove(100);

            //PrintTree(bst);

            //bst.Remove(250);

            //PrintTree(bst);

            //var something = new Algorithms.AssociativeArrays.HashTable<int, int>(10);
            //something.Add(10, 10);
            //something.Add(20, 20);
            //something.Add(11, 23);

            //something.Remove(11);

            var sortingAlgorithm = new InsertionSort<int>();
            var arrayToSort = new[]{9, 0, 9, 1, 0, 2, 33, 33, 2, 1, 8, -1, 4, 2, -3, 11, 0, 32};
            sortingAlgorithm.Sort(arrayToSort);

            Print(arrayToSort);



        }

       private static void Print(int[] arrayToSort)
       {
           foreach (var i in arrayToSort)
           {
               Console.WriteLine(i);
           }
       }

       private static void PrintTree(BinarySearchTree<int> bst)
        {
            foreach (var item in bst.SortedEnumerable())
            {
                Console.WriteLine(item);
            }
        }
    }
}
