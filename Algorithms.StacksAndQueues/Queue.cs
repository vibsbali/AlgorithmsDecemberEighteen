using System;
using System.Data;

namespace Algorithms.StacksAndQueues
{
    public class Queue<T>
    {
        private T[] backingStore;

        public Queue()
            : this(4)
        {
        }

        public Queue(int size)
        {
            if (size < 4)
            {
                size = 4;
            }

            backingStore = new T[size];
            Head = 0;
            Tail = 0;
        }

        internal int Count { get; private set; }

        internal int Head { get; set; } 
        internal int Tail { get; set; }

        public void Enqueue(T item)
        {
            backingStore[Head++] = item;
            ++Count;
            //Is head getting beyond the size of backing store
            if (Head > backingStore.Length)
            {
                //check that we still have some space in the array
                if (Count < backingStore.Length)
                {
                    //going to set head to 0 and we are going to have a wrapped head
                    // 0 1 2 3 4 5
                    // H . . . . T
                    Head = 0;
                }
                else if(Count > backingStore.Length)
                {
                    //Tail has to be 0 otherwise we should not be in position of setting tail to 0
                    Assert(Tail == 0, "Tail is not at zero but our count is greater than backing store");

                    var temp = new T[backingStore.Length * 2];
                    Array.Copy(backingStore, 0, temp, 0, Count - 1);
                }
            }
            //Must be wrapped
            else if (Head == Tail)
            {
                if (Count > backingStore.Length)
                {
                    //we have depleted the backing store
                    //Scenario 1
                    // 0 1 2 3 4 5
                    // . . T . . . 
                    // . . H . . .
                    // Count is 6
                    // Length is 6
                    var temp = new T[backingStore.Length * 2];
                    Array.Copy(backingStore, Tail, temp, 0, backingStore.Length - Tail);
                    Array.Copy(backingStore, 0, temp, backingStore.Length - Tail, Head);
                    Tail = 0;
                    Head = Count;
                    // 0 1 2 3 4 5 6 . . . . 12
                    // T . . . . . H
                }
                else
                {
                    throw new ApplicationException("Missed a scenario");
                }
            }
            else
            {
                Assert(false, "Head is neither zero nor equal to tail");
            }
        }

        private void Assert(bool expression, string message)
        {
            if (expression == false)
            {
                throw new ApplicationException($"Assert failed {message}");
            }
        }

        public T Deque()
        {

        }
    }
}

