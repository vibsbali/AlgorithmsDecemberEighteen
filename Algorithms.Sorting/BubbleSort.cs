using System;
using System.Collections.Generic;

namespace Algorithms.Sorting
{
   public class BubbleSort<T>
      where T : IComparable<T>
   {
      public void Sort(T[] arrayToSort)
      {
         bool hasMoved;
         var lastIndex = arrayToSort.Length + 1;
         do
         {
            --lastIndex; var i = 1;
            hasMoved = false;
            while (i < lastIndex)
            {
               if (arrayToSort[i].CompareTo(arrayToSort[i - 1]) < 0)
               {
                  arrayToSort.Swap(i, i - 1);
                  hasMoved = true;
               }

               i++;
            }
         } while (hasMoved);
      }
   }
}