using System;

namespace AVL_Tree
{
    internal static class Program
    {
        private static void Main()
        {
            var tree = new AvlTree<int>();
            
            var random = new Random(1234);

            for (var i = 0; i < 10_000; ++i)
            {
                tree.Insert(random.Next());
            }
            
            random = new Random(1234);

            for (var i = 0; i < 10_000; ++i)
            {
                if (!tree.Contains(random.Next()))
                {
                    Console.WriteLine("Test failed");
                    
                    return;
                }
            }

            Console.WriteLine("Test passed");

            Console.WriteLine(tree.Size);
            
            random = new Random(1234);

            for (var i = 0; i < 10_000; ++i)
            {
                tree.Remove(random.Next());
            }

            Console.WriteLine(tree.Size == 0 ? "Test passed" : "Test failed");
        }
    }
}