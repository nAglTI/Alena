using System;

namespace ArrayList
{
    internal static class Program
    {
        private static void Main()
        {
            var list = new ArrayList<int>();

            const int n = 1_000_000_000;

            for (var i = 0; i < n; ++i)
            {
                list.PushBack(i);
            }

            Console.WriteLine(list.Size);
            
            Console.WriteLine(Utils.BinarySearch(list, 5_000));
            Console.WriteLine(Utils.BinarySearch(list, 1_000_000_000));
            Console.WriteLine(Utils.BinarySearch(list, 0));

            for (var i = 0; i < n; ++i)
            {
                list.PopBack();
            }

            Console.WriteLine(list.Size);
            
            list.PushBack(4);
            list.PushBack(6);
            list.PushBack(6);
            list.PushBack(6);
            list.PushBack(9);
            list.PushBack(60);

            Console.WriteLine(Utils.BinarySearch(list, 6) == 1); // Первое вхождение

            // list.Back();
        }
    }
}