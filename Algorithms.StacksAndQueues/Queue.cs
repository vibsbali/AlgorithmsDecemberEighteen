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
            if (Head > backingStore.Length)
            {
                if (Count < backingStore.Length)
                {
                    Head = 0;
                }
                if (Count > backingStore.Length)
                {
                    
                }
            }
        }

        public T Deque()
        {

        }
    }
}

