using System;
using System.ComponentModel.DataAnnotations;

namespace Algorithms.StacksAndQueues
{
    public class Stack<T>
    {
        private T[] backingStore;

        public Stack()
            : this(4)
        {
        }

        public Stack(int size)
        {
            if (size < 4)
            {
                size = 4;
            }
            
            backingStore = new T[size];
        }

        internal int Count { get; private set; }

        public void Push(T item)
        {
            backingStore[Count++] = item;
            if (Count > backingStore.Length)
            {
                IncreaseSize();
            }
        }

        private void IncreaseSize()
        {
            var temp = new T[backingStore.Length * 2];
            Array.Copy(backingStore, 0, temp, 0, Count);
            backingStore = temp;
        }

        public T Pop()
        {
            if (Count > 0)
            {
                var itemToReturn = backingStore[--Count];
                if (Count > 4 && Count < backingStore.Length / 3)
                {
                    DecreaseSize();
                }

                return itemToReturn;
            }

            throw new InvalidOperationException();
        }

        private void DecreaseSize()
        {
            var temp = new T[backingStore.Length / 2];
            Array.Copy(backingStore, 0, temp, 0, Count);
            backingStore = temp;
        }

        public bool TryPop(out T value)
        {
            if (Count > 0)
            {
                value = Pop();
                return true;
            }

            value = default(T);
            return false;
        }

        public T Peek()
        {
            if (Count > 0)
            {
                var index = Count - 1;
                return backingStore[index];
            }
            throw new InvalidOperationException();
        }
    }
}
