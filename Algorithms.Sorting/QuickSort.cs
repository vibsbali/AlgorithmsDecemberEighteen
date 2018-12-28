using System;
using System.Linq;

namespace Algorithms.Sorting
{
   public class QuickSort<T>
       where T : IComparable<T>
   {
      public void Sort(T[] array)
      {
         Shuffle(array);

         SortArray(array, 0, array.Length - 1);
      }

      private void SortArray(T[] array, int lo, int hi)
      {
         if (lo >= hi)
         {
            return;
         }

         int swappedPosition = Partition(array, lo, hi);

         SortArray(array, lo, swappedPosition - 1);
         SortArray(array, swappedPosition + 1, hi);
      }

      private int Partition(T[] array, int pivot, int hi)
      {
         var lo = pivot + 1;

         while (true)
         {
            while (array[lo].CompareTo(array[pivot]) < 0 && lo < hi)
            {
               lo++;
            }

            while (array[hi].CompareTo(array[pivot]) >= 0 && hi >= lo)
            {
               hi--;
            }

            if (lo < hi)
            {
               array.Swap(lo, hi);
            }
            else
            {
               array.Swap(hi, pivot);
            }

            if (lo >= hi)
            {
               break;
            }
         }

         return hi;
      }

      private void Shuffle(T[] items)
      {
         Random rand = new Random();
         var range = Enumerable.Range(0, items.Length - 1);
         var result = range.OrderBy(i => rand.Next()).ToList();
         var temp = 0;
         for (int i = 0; i < result.Count(); i++)
         {
            items.Swap(temp, result[i]);
            temp = result[i];
         }
      }
   }
}
