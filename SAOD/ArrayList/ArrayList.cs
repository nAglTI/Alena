#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace ArrayList
{
    public class ArrayList<T> : IEnumerable<T> where T : IComparable<T>
    {
        public void PushBack(T value)
        {
            if (Size == _capacity)
            {
                UpSize();
            }

            _array[Size++] = value;
        }

        public T PopBack()
        {
            if (Size == 0)
            {
                ThrowForEmptyList();
            }

            --Size;

            var result = _array[Size];

            if (Size <= _capacity / (DefaultCapacity * 2))
            {
                DownSize();
            }

            return result;
        }

        public void Clear()
        {
            _capacity = DefaultCapacity;
            
            _array = new T[DefaultCapacity];
        }

        public T Back()
        {
            if (Size == 0)
            {
                ThrowForEmptyList();
            }

            return _array[Size - 1];
        }

        public T Front()
        {
            if (Size == 0)
            {
                ThrowForEmptyList();
            }

            return _array[0];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return _array[index];
            }
        }

        public void Insert(int index, T value)
        {
            if (Size == _capacity)
            {
                UpSize();
            }

            CopyArray(index, index + 1, Size - index);

            _array[index] = value;
        }

        public void RemoveAt(int index)
        {
            var nextIndex = index + 1;

            CopyArray(nextIndex, index, Size - nextIndex);

            if (Size <= _capacity / DefaultCapacity / 2)
            {
                DownSize();
            }
        }

        public void ForEach(Action<T> action)
        {
            for (var i = 0; i < Size; ++i)
            {
                action(_array[i]);
            }
        }

        public int Size { get; private set; }

        public int IndexOf(T value) => Find(item => item.CompareTo(value) == 0);

        public int Find(Predicate<T> predicate)
        {
            for (var i = 0; i < Size; ++i)
            {
                if (predicate(_array[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public IEnumerator<T> GetEnumerator() => new Iterator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void UpSize() => ResizeArray(_capacity *= 2);

        private void DownSize()
        {
            if (_capacity == DefaultCapacity)
            {
                return;
            }
            
            ResizeArray(_capacity /= 2);
        }
        
        private void ResizeArray(int newSize)
        {
            var newArray = new T[newSize];

            var size = Math.Min(newSize, Size);

            for (var i = 0; i < size; ++i)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
        }

        private void CopyArray(int from, int to, int length)
        {
            for (var i = 0; i < length; ++i, ++from, ++to)
            {
                _array[to] = _array[from];
            }
        }

        private static void ThrowForEmptyList() => throw new InvalidOperationException("ArrayList is empty");

        private const int DefaultCapacity = 4;

        private T[] _array = new T[DefaultCapacity];

        private int _capacity = DefaultCapacity;
        
        public struct Iterator : IEnumerator<T>
        {
            public Iterator(ArrayList<T> list)
            {
                _list = list;
                
                _currentIndex = 0;
            }

            public bool MoveNext()
            {
                if (_currentIndex + 1 == _list.Size)
                {
                    return false;
                }

                ++_currentIndex;

                return true;
            }

            public void Reset() => _currentIndex = 0;

            public T Current => _list[_currentIndex];

            object? IEnumerator.Current => Current;

            public void Dispose() {}

            private readonly ArrayList<T> _list;

            private int _currentIndex;
        }
    }
}