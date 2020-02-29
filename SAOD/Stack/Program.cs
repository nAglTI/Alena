using System;
using System.Diagnostics;

namespace SAOD
{
    internal static class Program
    {
        public static void Main()
        {
            // number_1();
            // number_2();
            // number_3_1();
            // number_3_2();
            // number_4();
        }

        private static void number_1()
        {
            var stack = new Stack<string>();

            Console.WriteLine(stack.Size);
            // Console.WriteLine(Stack.Top());

            stack.Push("first");
            stack.Push("second");

            Console.WriteLine(stack.IsEmpty());
            Console.WriteLine(stack.Size);

            stack.Push("third");
            stack.Push("fourth");

            while (!stack.IsEmpty())
            {
                Console.WriteLine(stack.Top());
                stack.Pop();
            }

            // Stack.Pop();
            Console.WriteLine(stack.IsEmpty());
        }

        private static void number_2()
        {
            const int n = 1_000_000_000;

            var stack = new Stack<int>();

            var watch = Stopwatch.StartNew();

            for (var i = 0; i != n; ++i)
            {
                stack.Push(i);
            }

            for (var i = 0; i != n; ++i)
            {
                stack.Pop();
            }

            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        private static long Fac(long value) => value < 2 ? 1 : value * Fac(--value);

        private static long StackFac(long value)
        {
            var stack = new Stack<long>();
            
            while (value != 2)
            {
                stack.Push(value--);
            }

            long result = 0;
            
            while (!stack.IsEmpty())
            {
                result += stack.Top();
                stack.Pop();
            }

            return result;
        }

        private static void number_3_1()
        {
            Console.WriteLine(Fac(1_000_000));
        }
        
        private static void number_3_2()
        {
            Console.WriteLine(StackFac(1_000_000));
        }

        private static int Priority(char c) =>
            c switch
            {
                '^' => 3,
                '*' => 2,
                '/' => 2,
                '+' => 1,
                '-' => 1,
                '(' => 0,
                ')' => 0,
                _ => throw new ArgumentException("Unsupported symbol")
            };

        private static void number_4()
        {
            const string sample = "12*((2+2)^3-6)/2";

            var result = string.Empty;
            
            var stack = new Stack<char>();

            for (var i = 0; i < sample.Length; i++)
            {
                if (char.IsNumber(sample[i]))
                {
                    if (i != 0 && !char.IsNumber(sample[i - 1]))
                    {
                        result += ' ';
                    }
                        
                    result += sample[i];
                }
                else
                    switch (sample[i])
                    {
                        case '(':
                            stack.Push(sample[i]);
                            break;
                        case ')':
                        {
                            while (stack.Top() != '(' && Priority(stack.Top()) >= Priority(sample[i]))
                            {
                                result += $" {stack.Top()}";
                                stack.Pop();
                            }

                            stack.Pop();
                            break;
                        }
                        default:
                        {
                            while (!stack.IsEmpty() && Priority(stack.Top()) >= Priority(sample[i]))
                            {
                                result += $" {stack.Top()}";
                                stack.Pop();
                            }

                            stack.Push(sample[i]);
                            break;
                        }
                    }
            }

            while (!stack.IsEmpty())
            {
                result += $" {stack.Top()}";
                stack.Pop();
            }

            Console.WriteLine(result);
        }
    }
}