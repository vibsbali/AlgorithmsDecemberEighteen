using System;
using System.Collections;
using System.Collections.Generic;


namespace Algorithms.ArrayLists
{
    public class ArrayList<T> : IList<T>
        where T : IComparable<T>
    {
        public ArrayList()
            : this(4)
        {
        }

        public ArrayList(int size)
        {
            if (size < 4)
            {
                size = 4;
            }

            OriginalSize = size;
            BackingStore = new T[size];
        }

        internal int OriginalSize { get; }

        internal T[] BackingStore { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return BackingStore[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            BackingStore[Count++] = item;
            if (Count == BackingStore.Length)
            {
                IncreaseBackingStore();
            }
        }

        private void IncreaseBackingStore()
        {
            var currentLength = BackingStore.Length;
            var temp = new T[currentLength * 2];

            Array.Copy(BackingStore, 0, temp, 0, currentLength);
            BackingStore = temp;
        }

        public void Clear()
        {
            BackingStore = new T[OriginalSize];
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }
        

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex++] = BackingStore[i];
            }
        }

        public bool Remove(T item)
        {
            var indexOfItem = IndexOf(item);
            if (indexOfItem == -1)
            {
                return false;
            }

            RemoveAt(indexOfItem);
            return true;
        }

        private void DecreaseBackingStore()
        {
            var temp = new T[BackingStore.Length / 2];
            Array.Copy(BackingStore, 0, temp, 0, Count - 1);
            BackingStore = temp;
        }

        public int Count { get; private set; }
        public bool IsReadOnly => false;
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (BackingStore[i].CompareTo(item) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > Count - 1)
            {
                throw new IndexOutOfRangeException("Index must be within the bounds of the List");
            }

            Array.Copy(BackingStore, index, BackingStore, index + 1, Count - index);
            BackingStore[index] = item;
            ++Count;

            if (Count == BackingStore.Length)
            {
                IncreaseBackingStore();    
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
            {
                throw new IndexOutOfRangeException();
            }

            var indexOfItem = index;
            Array.Copy(BackingStore, indexOfItem + 1, BackingStore, indexOfItem, Count - indexOfItem - 1);
            BackingStore[Count--] = default(T);

            //If Count is greater than 4 and 
            if (BackingStore.Length > OriginalSize && BackingStore.Length / 3 > Count)
            {
                DecreaseBackingStore();
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return BackingStore[index];
            }

            set => Insert(index, value);
        }
    }
}
