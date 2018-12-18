using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Algorithms.Trees
{
   public class BinarySearchTree<T> : ICollection<T>
      where T : IComparable<T>
   {
      internal class BinaryNode
      {
         internal BinaryNode Left { get; set; }
         internal BinaryNode Right { get; set; }
         internal T Value { get; set; }

         internal BinaryNode(T value)
         {
            Value = value;
         }

         public bool HasLeftChild => Left != null;
         public bool HasRightChild => Right != null;


         public bool IsLeftOf(BinaryNode other)
         {
            return this.Value.CompareTo(other.Value) < 0;
         } 
      }

      internal BinaryNode Root { get; set; }
      public IEnumerator<T> GetEnumerator()
      {
         throw new NotImplementedException();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public void Add(T item)
      {
         var nodeToAdd = new BinaryNode(item);
         if (Root == null)
         {
            Root = nodeToAdd;
         }
         else
         {
            var current = Root;
            while (true)
            {
               if (nodeToAdd.Value.CompareTo(current.Value) < 0)
               {
                  if (current.HasLeftChild)
                  {
                     current = current.Left;
                  }
                  else
                  {
                     current.Left = nodeToAdd;
                     break;
                  }
               }
               else
               {
                  if (current.HasRightChild)
                  {
                     current = current.Right;
                  }
                  else
                  {
                     current.Right = nodeToAdd;
                     break;
                  }
               }
            }
         }

         ++Count;
      }

      public void Clear()
      {
         Root = null;
         Count = 0;
      }

      public bool Contains(T item)
      {
         var current = Root;
         while (current != null)
         {
            if (item.CompareTo(current.Value) == 0)
            {
               return true;
            }

            if (item.CompareTo(current.Value) < 0)
            {
               current = current.Left;
            }
            else
            {
               current = current.Right;
            }
         }

         return false;
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
         throw new NotImplementedException();
      }

      public bool Remove(T item)
      {
         var current = Root;
         BinaryNode previous = null;

         while (current != null)
         {
            //We have found the item
            if (current.Value.CompareTo(item) == 0)
            {
               
               if (!current.HasLeftChild && !current.HasRightChild)
               {
                  if (previous == null)
                  {
                     //We are at Root
                     Clear();
                     return true;
                  }

                  if (current.IsLeftOf(previous))
                  {
                     previous.Left = null;
                  }
                  else
                  {
                     previous.Right = null;
                  }
               }

               else if (current.Right == null)
               {
                  if (current.IsLeftOf(previous))
                  {
                     previous.Left = current.Left;
                  }
                  else
                  {
                     previous.Right = current.Right;
                  }
               }
               else
               {

               }

            }
         }
      }

      public IEnumerable<T> SortedEnumerable()
      {
         if (Count == 0)
         {
            return System.Linq.Enumerable.Empty<T>();
         }

         //We start by pushing root to Stack and set go left to true
         Stack<BinaryNode> auxStack = new Stack<BinaryNode>();
         auxStack.Push(Root);

         var enumerationQueue = new Queue<T>();
         bool goLeft = Root.HasLeftChild;

         while (auxStack.Count > 0)
         {
            //We keep going left until we hit a dead end
            while (goLeft && auxStack.Peek().HasLeftChild)
            {
               var current = auxStack.Peek().Left;
               auxStack.Push(current);
            }

            //as soon as we hit dead end we set goleft to false
            goLeft = false;
            var itemToAdd = auxStack.Pop();
            enumerationQueue.Enqueue(itemToAdd.Value);

            //if we have the right child we add to stack and set the go left again
            if (itemToAdd.HasRightChild)
            {
               auxStack.Push(itemToAdd.Right);
               goLeft = true;
            }
         }

         return enumerationQueue;
      }

      public int Count { get; private set; }
      public bool IsReadOnly => false;
   }
}
