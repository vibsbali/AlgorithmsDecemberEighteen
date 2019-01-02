using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Algorithms.Trees
{
    public class Tries
    {
        internal class Node
        {
            internal Node[] Next { get; set; }
            internal string Value { get; set; } = default(string);

            public Node(int size)
            {
                Next = new Node[size];
            }
        }

        private const int _size = 256;
        public int Count { get; private set; }
        private Node Root { get; } = new Node(_size);

        public void Add(string value)
        {
            //check value is not null or whitespace

            var valueAsCharArray = value.ToLower().ToCharArray();
            var current = Root;

            for (var index = 0; index < valueAsCharArray.Length; index++)
            {
                var character = valueAsCharArray[index];
                if (current.Next[character] == null)
                {
                    current.Next[character] = new Node(_size);
                }

                if (index == valueAsCharArray.Length - 1)
                {
                    current.Next[character].Value = value;
                }
                else
                {
                    current = current.Next[character];
                }
            }

            ++Count;
        }
        public bool Find(string value)
        {
            //check value is not null or whitespace

            return FindWithNode(value) != null;
        }

        private Node FindWithNode(string value)
        {
            var valueAsCharArray = value.ToLower().ToCharArray();
            var current = Root;
            for (var index = 0; index < valueAsCharArray.Length; index++)
            {
                var character = valueAsCharArray[index];
                if (current.Next[character] == null)
                {
                    return null;
                }

                if (index == valueAsCharArray.Length - 1)
                {
                    if (current.Value == value)
                        return current;
                    else
                        return null;
                }

                current = current.Next[character];
            }

            return null;
        }
       

        public bool Remove(string value)
        {
            var nodeToDelete = FindWithNode(value);
            if (nodeToDelete != null)
            {
                nodeToDelete.Value = default(string);
                --Count;
                return true;
            }

            return false;
        }
    }
}
