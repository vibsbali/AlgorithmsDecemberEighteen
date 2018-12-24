using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociativeArrays.AssociativeArrays
{
   public class HashTable<TKey, TValue>
        where TKey : IComparable<TKey>
   {
      internal LinkedList<TKey, TValue>[] backingStore;
      public int Count { get; private set; }
      public HashTable(int size)
      {
         backingStore = new backingStore[size];
      }

      public void Add(TKey key, TValue value)
      {
         var position = key.GetHashCode % backingStore.Length;

         if (backingStore[position] == null)
         {
            backingStore[position] = new LinkedList<TKey, TValue>();
         } 
         else
         {
             //check if item is alredy there
             if(backingStore[position].Contains(key)){
                 throw new InvalidOperationException("Cannot add duplicate keys");
             }
         }

         backingStore[position].AddLast(key, value);
         ++Count;
         
         return;
      }

      public TValue Remove(TKey key, TValue value)
      {
          var position = key.GetHashCode() % backingStore.Length;

          if(backingStore[position] == null || backingStore[position].First == null){
              throw new InvalidOperationException("Key is not present");
          } 

          var potentialItem = backingStore[position].Find[key];
          if(potentialItem.key == key && potentialItem.value == value){
              backingStore[position].Remove(key);
          }

          --Count;
          return potentialItem.value;
      }
   }
}
