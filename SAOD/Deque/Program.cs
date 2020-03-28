using System;

namespace Deque
{
    internal static class Program
    {
        private static void Main()
        {
            var deq = new Deque<int>();
            
            for (var i = 0; i < 10_000; ++i)
            {
                deq.PushBack(i);
            }
            
            for (var i = 0; i < 10_000; ++i)
            {
                if (deq.PopFront() != i)
                {
                    Console.WriteLine("Test 1 failed");
                    
                    return;
                }
            }
            
            Console.WriteLine("Test 1 passed");
            
            for (var i = 0; i < 10_000; ++i)
            {
                deq.PushBack(i);
            }
            
            for (var i = 10_000 - 1; i >= 0; --i)
            {
                if (deq.PopBack() != i)
                {
                    Console.WriteLine("Test 2 failed");
                    
                    return;
                }
            }
            
            Console.WriteLine("Test 2 passed");
        }
    }
}