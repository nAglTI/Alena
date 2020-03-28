using System;
using static System.Math;
using static System.Runtime.InteropServices.Marshal;

namespace Deque
{
    public class Deque<T>
    {
        public Deque()
        {
            long objectSize = SizeOf<T>();

            _blockSize = (int) (Max(16 * objectSize, 4096) / objectSize);

            _back = _front = new BlockNode(_blockSize);

            _frontIndex = _backIndex = 0;
        }

        public void PushBack(T value)
        {
            if (_backIndex == _blockSize)
            {
                var newBlock = new BlockNode(_blockSize) {Prev = _back};

                _back.Next = newBlock;

                _back = newBlock;

                _backIndex = 0;
            }

            _back[_backIndex++] = value;

            ++Size;
        }

        public void PushFront(T value)
        {
            if (_frontIndex == 0)
            {
                var newBlock = new BlockNode(_blockSize) {Next = _front};

                _front.Prev = newBlock;

                _front = newBlock;

                _frontIndex = _blockSize;
            }

            _front[--_frontIndex] = value;

            ++Size;
        }

        public T PopFront()
        {
            ThrowForEmptyDeque();

            if (_frontIndex == _blockSize)
            {
                _front = _front.Next;

                _frontIndex = 0;
            }

            --Size;

            return _front[_frontIndex++];
        }

        public T PopBack()
        {
            ThrowForEmptyDeque();

            if (_backIndex == 0)
            {
                _back = _back.Prev;

                _backIndex = _blockSize;
            }

            --Size;

            return _back[--_backIndex];
        }

        public void Clear()
        {
            _back = _front = new BlockNode(_blockSize);

            _frontIndex = _backIndex = 0;

            Size = 0;
        }

        public int Size { get; private set; }

        private void ThrowForEmptyDeque()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException($"{GetType().Name} is empty");
            }
        }

        private BlockNode _front;

        private int _frontIndex;

        private BlockNode _back;

        private int _backIndex;

        private readonly int _blockSize;

        internal class BlockNode
        {
            public BlockNode(int blockSize)
            {
                _arr = new T[blockSize];
            }

            public T this[int index]
            {
                get => _arr[index];
                set => _arr[index] = value;
            }

            public BlockNode Next;

            public BlockNode Prev;

            private readonly T[] _arr;
        }
    }
}