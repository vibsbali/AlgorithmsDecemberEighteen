using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Sorting
{
   public static class SwapHelper
   {
      public static void Swap<T>(this T[] arrayForSwap, int i, int j)
      {
         var temp = arrayForSwap[i];
         arrayForSwap[i] = arrayForSwap[j];
         arrayForSwap[j] = temp;
      }
   }
}