#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public void PushBack(T value)
        {
            var node = new Node
            {
                Value = value,
                Prev = _last,
                Next = null
            };

            _last.Next = node;
            
            _last = node;

            ++Size;
        }
        
        public void PushFront(T value)
        {
            var node = new Node
            {
                Value = value,
                Prev = null,
                Next = _first
            };

            _first.Prev = node;
            
            _first = node;

            ++Size;
        }

        public void PopBack()
        {
            if (Size == 0)
            {
                ThrowForEmptyList();
            }
            
            _last = _last.Prev;
            
            --Size;
        }

        public void PopFront()
        {
            if (Size == 0)
            {
                ThrowForEmptyList();
            }
            
            _first = _first.Next;

            --Size;
        }

        public T Back() => _last.Value;

        public T Front() => _first.Value;

        public void Clear()
        {
            _first = null;
            _last = null;
        }
        
        public Iterator Begin() => new Iterator(this);
        
        public Iterator RBegin() => new Iterator(this, true);

        public void Insert(Iterator iterator, T value)
        {
            var node = iterator.CurrentNode;
            
            if (node == _first)
            {
                PushFront(value);
            }
            else
            {
                var newNode = new Node
                {
                    Value = value
                };

                node.Prev.Next = newNode;

                newNode.Next = node;
                newNode.Prev = node.Prev;

                node.Prev = newNode;
            }
        }
        
        public void Remove(Iterator iterator)
        {
            var node = iterator.CurrentNode;

            if (node == _first)
            {
                PopFront();
            }
            else if (node == _last)
            {
                PopBack();
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            --Size;
        }

        public Iterator Find(T value)
        { 
            var iterator = new Iterator(this);

            if (!iterator.IsValid())
            {
                return iterator;
            }
            
            while (iterator.HasNext())
            {
                iterator.Next();

                if (iterator.Get().CompareTo(value) == 0)
                {
                    return iterator;
                }
            }
            
            iterator.Next();

            return iterator;
        }
        
        public Iterator RFind(T value)
        { 
            var iterator = new Iterator(this);

            if (!iterator.IsValid())
            {
                return iterator;
            }
            
            while (iterator.HasPrev())
            {
                iterator.Prev();

                if (iterator.Get().CompareTo(value) == 0)
                {
                    return iterator;
                }
            }
            
            iterator.Prev();

            return iterator;
        }
        
        public IEnumerator<T> GetEnumerator() => Begin();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void ForEach(Action<T> action)
        {
            var iter = Begin();
            
            while (iter.HasNext())
            {
                action(iter.Get());
                
                iter.Next();
            }

            if (iter.IsValid())
            {
                action(iter.Get());
            }
        }

        private static void ThrowForEmptyList()
        {
            throw new InvalidOperationException("List is empty");
        }
        
        private Node _first;

        private Node _last;
        
        public int Size { get; private set; }
        
        public struct Iterator : IEnumerator<T>
        {
            public Iterator(LinkedList<T> list, bool reverse = false)
            {
                _list = list;
                
                CurrentNode = reverse ? list._last : list._first;

                _reverse = reverse;
            }

            public void Next() => CurrentNode = CurrentNode.Next;
        
            public void Prev() => CurrentNode = CurrentNode.Prev;

            public bool HasNext() => CurrentNode.Next != null;
        
            public bool HasPrev() => CurrentNode.Prev != null;

            public bool IsValid() => CurrentNode != null;

            public T Get()
            {
                if (CurrentNode == null)
                {
                    throw new InvalidOperationException("Iterator is invalid");
                }

                return CurrentNode.Value;
            }

            public bool MoveNext()
            {
                if (!HasNext())
                {
                    return false;
                }
            
                Next();

                return true;
            }

            public void Reset() => CurrentNode = _reverse ? _list._last : _list._first;

            public void Dispose() {}

            public T Current => CurrentNode.Value;

            object? IEnumerator.Current => Current;

            public Node CurrentNode;
            
            private readonly LinkedList<T> _list;
            
            private readonly bool _reverse;
        }

        public class Node
        {
            public T Value;
        
            public Node Prev;

            public Node Next;
        }
    }
}