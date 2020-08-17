using System;
using System.Collections.Generic;
using System.Linq;

using Algo.DynamicProgramming.Entities;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal sealed class Problem
    {
        public Problem() : this(GetRandomKeys())
        {
        }

        public Problem(IEnumerable<decimal> realKeys, IEnumerable<decimal> dummyKeys) : this((realKeys, dummyKeys))
        {
        }

        private Problem((IEnumerable<decimal> RealKeys, IEnumerable<decimal> DummyKeys) keys)
        {
            RealKeyProbabilities = new BasedArray<decimal>(keys.RealKeys, 1);
            DummyKeyProbabilities = keys.DummyKeys?.ToArray() ?? 
                                    throw new ArgumentNullException(nameof(keys.DummyKeys));
        }

        public IReadOnlyList<decimal> RealKeyProbabilities { get; }

        public IReadOnlyList<decimal> DummyKeyProbabilities { get; }

        public int Size => RealKeyProbabilities.Count;

        public void Print()
        {
            int i,
                size = Size;
            Console.Write("i:     ");
            for (i = 0; i <= size; ++i)
            {
                Console.Write("{0,7}", i);
            }

            Console.Write(Environment.NewLine + "pi:           ");
            for (i = 1; i <= size; ++i)
            {
                Console.Write("{0,7:F3}", RealKeyProbabilities[i]); 
            }

            Console.Write(Environment.NewLine + "qi:    ");
            for (i = 0; i <= size; ++i)
            {
                Console.Write("{0,7:F3}", DummyKeyProbabilities[i]);
            }

            Console.WriteLine();
        }

        private static (IEnumerable<decimal>, IEnumerable<decimal>) GetRandomKeys()
        {
            Random random = new Random();
            int size = random.Next(2, 21);
            decimal[] realKeys = new decimal[size];
            decimal[] dummyKeys = new decimal[size + 1];
            decimal sum = 0.0M;
            for (int i = 0; i < size; ++i)
            {
                realKeys[i] = (decimal)random.NextDouble();
                sum += realKeys[i];

                dummyKeys[i] = (decimal)random.NextDouble();
                sum += dummyKeys[i];
            }

            dummyKeys[size] = (decimal)random.NextDouble();
            sum += dummyKeys[size];

            return (Normalize(realKeys, sum), Normalize(dummyKeys, sum));
        }

        private static IEnumerable<decimal> Normalize(IEnumerable<decimal> data, decimal denominator)
        {
            return data.Select(d => d / denominator);
        }
    }
}
