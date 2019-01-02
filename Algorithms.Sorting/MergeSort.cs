using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
   public class MergeSort<T>
       where T : IComparable<T>
   {

      public void Sort(T[] arrayToSort)
      {
         SplitRecursively(arrayToSort);
      }

      private void SplitRecursively(T[] A)
      {
         if (A.Length > 1)
         {
            var midPoint = A.Length / 2;
            var leftArray = new T[midPoint];
            var rightArray = new T[A.Length - midPoint];
            Array.Copy(A, 0, leftArray, 0, midPoint);
            Array.Copy(A, midPoint, rightArray, 0, A.Length - midPoint);

            SplitRecursively(leftArray);
            SplitRecursively(rightArray);
            Merge(A, leftArray, rightArray);
         }
      }


      private void Merge(T[] A, T[] leftA, T[] rightA)
      {
         var i = 0; var leftIndex = 0; var rightIndex = 0;
         while (leftIndex < leftA.Length && rightIndex < rightA.Length)
         {
            if (leftA[leftIndex].CompareTo(rightA[rightIndex]) <= 0)
            {
               A[i++] = leftA[leftIndex++];
            }
            else
            {
               A[i++] = rightA[rightIndex++];
            }
         }

         while (leftIndex < leftA.Length)
         {
            A[i++] = leftA[leftIndex++];
         }

         while (rightIndex < rightA.Length)
         {
            A[i++] = rightA[rightIndex++];
         }
      }
   }
}
