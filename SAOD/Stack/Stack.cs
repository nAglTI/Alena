using System;

namespace SAOD
{
    public class Stack<T>
    {
        public Stack()
        {
            _blockCapacity = 4;
            
            _currentBlock = new Block<T>(_blockCapacity, null);
        }

        public void Push(T value)
        {
            if (_blockSize == _blockCapacity)
            {
                _blockCapacity *= 2;
                
                _currentBlock = _savedBlock ?? new Block<T>(_blockCapacity, _currentBlock);

                _savedBlock = null;
                
                _blockSize = 0;
            }
            
            _currentBlock.Array[_blockSize++] = value;
            
            ++Size;
        }

        public void Pop()
        {
            if (Size == 0)
            {
                ThrowEmptyStackException();
            }

            --_blockSize;
            --Size;

            if (_blockSize != 0)
            {
                return;
            }

            _savedBlock = _currentBlock;
            
            _currentBlock = _currentBlock.Prev;

            _blockCapacity /= 2;

            _blockSize = _blockCapacity;
        }

        public T Top()
        {
            if (Size == 0)
            {
                ThrowEmptyStackException();
            }
            
            return _currentBlock.Array[_blockSize - 1];
        }

        public bool IsEmpty() => Size == 0;

        private static void ThrowEmptyStackException()
        {
            throw new InvalidOperationException("Stack is empty");
        }

        public int Size { get; private set; }

        private int _blockCapacity;
        private int _blockSize;

        private Block<T> _currentBlock;
        private Block<T> _savedBlock;
    }

    internal class Block<T>
    {
        public Block(int capacity, Block<T> prev)
        {
            Prev = prev;
            
            Array = new T[capacity];
        }
        
        public Block<T> Prev { get; }

        public T[] Array { get; }
    }
}