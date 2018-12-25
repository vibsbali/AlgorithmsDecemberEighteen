using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
   public class InsertionSort<T>
        where T : IComparable<T>
    {
        public void Sort(T[] arrayToSort)
        {
            for (int i = 1; i < arrayToSort.Length; i++)
            {
                if (arrayToSort[i].CompareTo(arrayToSort[i - 1]) < 0)
                {
                    var temp = arrayToSort[i];
                    var insertionIdx = i - 1;
                    while (insertionIdx > 0)
                    {
                        if (arrayToSort[insertionIdx - 1].CompareTo(temp) <= 0)
                        {
                            break;
                        }
                        else
                        {
                            insertionIdx--;
                        }
                    }

                    Array.Copy(arrayToSort, insertionIdx, arrayToSort, insertionIdx + 1, i - insertionIdx);
                    arrayToSort[insertionIdx] = temp;
                }
            }
        }
    }
}

