using System;

namespace Algo.DivideAndConquer.Ex212
{
    internal static class Program
    {
        private static int _counter;

        static Program()
        {
            _counter = 0;
        }

        private static void Main(string[] args)
        {
            int n = int.Parse(args[0]);

            RecursiveStep(n);

            Console.WriteLine($"{_counter} steps for the starting value of {n}");
        }

        private static void RecursiveStep(int n)
        {
            _counter++;

            if (n == 1)
            {
                return;
            }

            RecursiveStep(n / 2);
            RecursiveStep(n / 2);
        }
    }
}
