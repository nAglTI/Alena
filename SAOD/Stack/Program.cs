using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            /*MyStack mystack = new MyStack(5);

            System.Console.WriteLine(mystack.Size());
            System.Console.WriteLine(mystack.Top());

            mystack.Push(3);
            mystack.Push(19);

            System.Console.WriteLine(mystack.IsEmpty());
            System.Console.WriteLine(mystack.Size());

            mystack.Push(41);
            mystack.Push(5);

            while (!mystack.IsEmpty())
            {
                System.Console.WriteLine(mystack.Top());
                mystack.Pop();
            }

            mystack.Pop();
            System.Console.WriteLine(mystack.IsEmpty());*/

            var s = new Stack<int>();
            Console.WriteLine(s.IsEmpty());
            //Console.WriteLine(s.Pop());
            s.Push(1);
            s.Push(2);
            s.Push(3);
            Console.WriteLine(s.IsEmpty());
            Console.WriteLine(s.Pop());
            s.Clear();
            Console.WriteLine(s.IsEmpty());
            Console.WriteLine(s.Peek());
            Console.WriteLine(s.Pop());

        }
    }
}
