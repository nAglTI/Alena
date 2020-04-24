using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using KTrie;

namespace StringCounter
{
    internal static class Program
    {
        private static void Main()
        {
            Hash();
            //Trie();
        }

        private static void Hash()
        {
            var lines = File.ReadAllText("stuff/big.txt").Split(' ', '\n');

            var checkLines = File.ReadAllText("stuff/check.txt").Split('\n');
            
            var hash = new Dictionary<string, uint>();

            var watch = Stopwatch.StartNew();

            foreach (var line in lines)
            {
                if (!hash.ContainsKey(line))
                {
                    hash[line] = 0;
                }
                
                hash[line] += 1;
            }

            foreach (var line in checkLines)
            {
                Console.WriteLine(hash[line]);
            }
            
            watch.Stop();

            Console.WriteLine();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        // private static void Trie()
        // {
        //     var lines = File.ReadAllText("stuff/big.txt").Split(' ', '\n');
        //
        //     var checkLines = File.ReadAllText("stuff/check.txt").Split('\n');
        //     
        //     var trie = new StringTrie<uint>();
        //
        //     var watch = Stopwatch.StartNew();
        //
        //     foreach (var line in lines)
        //     {
        //         if (!trie.ContainsKey(line))
        //         {
        //             trie[line] = 0;
        //         }
        //         
        //         trie[line] += 1;
        //     }
        //
        //     foreach (var line in checkLines)
        //     {
        //         Console.WriteLine(trie[line]);
        //     }
        //     
        //     watch.Stop();
        //
        //     Console.WriteLine();
        //     Console.WriteLine(watch.ElapsedMilliseconds);
        // }
    }
}