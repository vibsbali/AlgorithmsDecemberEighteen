using System;
using System.Collections;
using System.Collections.Generic;

namespace Algorithms.LinkedLists
{
    public class DoubleLinkedList<T> : ICollection<T>
        where T : IComparable<T>
    {
        internal class Node
        {
            internal Node Next { get; set; }
            internal Node Previous { get; set; }
            public T Value { get; set; }
            public Node(T value)
            {
                Value = value;
            }
        }

        internal Node Head { get; set; }
        internal Node Tail { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var nodeToAdd = new Node(item);
            if (Head == null)
            {
                Head = Tail = nodeToAdd;
            }
            else
            {
                Tail.Next = nodeToAdd;
                nodeToAdd.Previous = Tail;
                Tail = nodeToAdd;
            }

            ++Count;
        }

        public void Clear()
        {
            Head = Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Value.CompareTo(item) == 0)
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            Node previous = null;
            var current = Head;
            while (current != null)
            {
                //We found the item to remove
                if (current.Value.CompareTo(item) == 0)
                {
                    if (previous == null)
                    {
                        if (Head == Tail)
                        {
                            Clear();
                            return true;
                        }

                        Head = current.Next;
                        Head.Previous = null;
                    }
                    else
                    {
                        if (current.Next == null)
                        {
                            //We are at tail
                            Tail = previous;
                            Tail.Next = null;
                        }
                        else
                        {
                            previous.Next = current.Next;
                            current.Next.Previous = previous;
                        }
                    }

                    --Count;
                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        public void AddFront(T item)
        {
            var nodeToAdd = new Node(item);
            if (Head == null)
            {
                Head = Tail = nodeToAdd;
            }
            else
            {
                Head.Previous = nodeToAdd;
                nodeToAdd.Next = Head;
                Head = nodeToAdd;
            }

            ++Count;
        }

        public void AddToBack(T item)
        {
            Add(item);
        }

        public int Count { get; private set; }
        public bool IsReadOnly => false;
    }
}
