using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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
            return BreadthFirstSearch().GetEnumerator();
        }

        public IEnumerable<T> BreadthFirstSearch()
        {
            var auxStack = new Stack<BinaryNode>();
            var enumerationQueue = new Queue<T>();
            auxStack.Push(Root);
            while (auxStack.Count > 0)
            {
                var current = auxStack.Pop();

                if (current.HasLeftChild)
                {
                    auxStack.Push(current.Left);
                }

                if (current.HasRightChild)
                {
                    auxStack.Push(current.Right);
                }

                enumerationQueue.Enqueue(current.Value);
            }

            return enumerationQueue;
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
                    //Are we on tail node
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

                    //Item to remove doesn't have right child
                    //so promote left child and update pointers
                    else if (current.Right == null)
                    {
                        if (previous == null)
                        {
                            //We are at Root
                            Root = current.Left;
                        }
                        else if (current.IsLeftOf(previous))
                        {
                            previous.Left = current.Left;
                        }
                        else
                        {
                            previous.Right = current.Left;
                        }
                    }
                    //Item to remove has a right child which doesn't have 
                    //left child
                    else if (!current.Right.HasLeftChild)
                    {
                        if (previous == null)
                        {
                            //We are at Root
                            Root = current.Right;
                        }
                        else if (current.IsLeftOf(previous))
                        {
                            previous.Left = current.Right;
                        }
                        else
                        {
                            previous.Right = current.Right;
                        }
                    }
                    //Item to remove has a right child which has a left child
                    //promote left most child in current's place and update reference pointer
                    else if (current.Right.HasLeftChild)
                    {
                        var parent = current.Right;
                        var tail = current.Right.Left;
                        while (tail.HasLeftChild)
                        {
                            parent = tail.Left;
                            tail = parent.Left;
                        }

                        if (current.IsLeftOf(parent))
                        {
                            current.Left = tail;
                            tail.Left = current.Left;
                            tail.Right = current.Right;
                        }
                        else
                        {
                            current.Right = tail;
                            tail.Left = current.Left;
                            tail.Right = current.Right;
                        }

                        parent.Left = null;
                    }

                    --Count;
                    return true;
                }

                previous = current;
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
