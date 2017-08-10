using System;

namespace Algo.DivideAndConquer.FastFourierTransform
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Polynomial left = new Polynomial(1D, 3D, 0D, 4D);  // 1 + 3x + 4x^3
            Polynomial right = new Polynomial(0D, 5D, 2D, 1D); // 5x + 2x^2 + x^3

            // Expected result: 5x + 17x^2 + 7x^3 + 23x^4 + 8x^5 + 4x^6
            Polynomial result = PolyMath.Multiply(left, right);
            Console.WriteLine($"({left.ToString(true)}) x ({right.ToString(true)}) = {result.ToString(true)}");
        }
    }
}
