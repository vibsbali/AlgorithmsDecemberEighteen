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
            Head = Tail = 0;
        }

        internal int Head { get; set; }
        internal int Tail { get; set; }

        public void Enqueue(T item)
        {
            backingStore[Head++] = item;
        }

        public T Deque()
        {

        }
    }
}

