using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Stack<T>
    {
        T[] data;

        public int Count { get; private set; }

        public Stack()
        {
            data = new T[0];
        }

        public void Push(T s)
        {
            Array.Resize(ref data, Count + 1);
            data[Count++] = s;
        }

        public T Peek()
        {
            if (Count > 0)
                return data[Count - 1];
            else
                return default(T);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }
        public T Pop()
        {
            if (Count > 0)
            {
                var t = Peek();
                Array.Resize(ref data, Count - 1);
                --Count;
                return t;
            }
            else
                throw new System.InvalidCastException("Стек пуст");
        }
        public void Clear()
        {
            Count = 0;
            Array.Resize(ref data, Count);
        }
    }
}
