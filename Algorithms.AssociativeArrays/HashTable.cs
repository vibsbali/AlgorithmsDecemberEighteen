using System;
using System.Collections.Generic;

namespace Algorithms.AssociativeArrays
{
   public class HashTable<TKey, TValue>
      where TKey : IComparable<TKey>
   {
      private readonly int _size;
      private List<Node> _backingStore;

      internal class Node
      {
         internal List<KeyValuePair<TKey, TValue>> KeyValuePairs { get; set; }

         internal Node()
         {
            KeyValuePairs = new List<KeyValuePair<TKey, TValue>>();
         }
      }

      public int Count { get; private set; }

      public HashTable(int size)
      {
         _size = size;
         Instantiate();
      }

      private void Instantiate()
      {
         _backingStore = new List<Node>(_size);
         for (int i = 0; i < _size; i++)
         {
            _backingStore.Add(new Node());
         }

         Count = 0;
      }

      public void Add(TKey key, TValue value)
      {
         int position = GetPosition(key);

         //We check if List at some position of backing store is not instantiated yet
         if (_backingStore[position] == null)
         {
            _backingStore[position] = new Node();
         }
         else
         {
            //check if item is already there
            if (TryGetItem(_backingStore[position], key) != null)
            {
               throw new InvalidOperationException("Cannot add duplicate keys");
            }
         }

         _backingStore[position].KeyValuePairs.Add(new KeyValuePair<TKey, TValue>(key, value));
         ++Count;
      }

      private int GetPosition(TKey key, int size = 0)
      {
         if (size == 0)
         {
            size = _backingStore.Count;
         }
         return key.GetHashCode() % size;
      }

      private KeyValuePair<TKey, TValue>? TryGetItem(Node node, TKey key)
      {
         foreach (var keyValuePair in node.KeyValuePairs)
         {
            if (keyValuePair.Key.CompareTo(key) == 0)
            {
               return keyValuePair;
            }
         }

         return null;
      }

      public TValue TryGetValue(TKey key)
      {
         int position = GetPosition(key);
         var potentialItem = TryGetItem(_backingStore[position], key);

         if (potentialItem != null)
         {
            return potentialItem.Value.Value;
         }

         return default(TValue);
      }

      public void Clear()
      {  
         Instantiate(); 
      }

      public void Update(TKey key, TValue value)
      {
         int position = GetPosition(key);
         var potentialItem = TryGetItem(_backingStore[position], key);

         if (potentialItem != null)
         {
            //KeyValuePairs cannot be updated so remove and update
            _backingStore[position].KeyValuePairs.Remove(potentialItem.Value);
            _backingStore[position].KeyValuePairs.Add(new KeyValuePair<TKey, TValue>(key, value));
         }
      }

      public bool Remove(TKey key)
      {
         var position = key.GetHashCode() % _backingStore.Count;

         if (_backingStore[position] == null)
         {
            throw new InvalidOperationException("Key is not present");
         }

         var potentialItem = TryGetItem(_backingStore[position], key);
         if (potentialItem == null)
         {
            throw new InvalidOperationException("No key present");
         }

         //we can use remove because TryGetItem will return reference to an object
         _backingStore[position].KeyValuePairs.Remove(potentialItem.Value);
         --Count;
         return true;
      }

      private void ResizeUp()
      {
         var temp = new List<Node>(_backingStore.Count * 2);
         for (int i = 0; i < temp.Capacity; i++)
         {
            temp.Add(new Node());
         }
         foreach (var node in _backingStore)
         {
            foreach (var nodeKeyValuePair in node.KeyValuePairs)
            {
               var position = GetPosition(nodeKeyValuePair.Key, temp.Capacity);
               temp[position].KeyValuePairs.Add(new KeyValuePair<TKey, TValue>(nodeKeyValuePair.Key, nodeKeyValuePair.Value));
            }
         }

         //No need to change count as we are not adding or removing any values
         _backingStore = temp;
      }

      private void ResizeDown()
      {
         var temp = new List<Node>(_backingStore.Count / 2);
         for (int i = 0; i < temp.Capacity; i++)
         {
            temp.Add(new Node());
         }
         foreach (var node in _backingStore)
         {
            foreach (var nodeKeyValuePair in node.KeyValuePairs)
            {
               var position = GetPosition(nodeKeyValuePair.Key, temp.Capacity);
               temp[position].KeyValuePairs.Add(new KeyValuePair<TKey, TValue>(nodeKeyValuePair.Key, nodeKeyValuePair.Value));
            }
         }

         //No need to change count as we are not adding or removing any values
         _backingStore = temp;
      }
   }
}
