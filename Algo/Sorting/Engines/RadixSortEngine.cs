using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Sorting.Engines
{
    public sealed class RadixSortEngine : ISortEngine<int>
    {
        private const int OptimalRadix = -1;
        private readonly int _radix;

        public RadixSortEngine(int radix = OptimalRadix)
        {
            _radix = radix < 2 && radix != OptimalRadix ? 
                throw new ArgumentOutOfRangeException(nameof(radix)) :
                radix;
        }

        IEnumerable<int> ISortEngine<int>.Sort(IEnumerable<int> data)
        {
            data = data as IReadOnlyCollection<int> ??
                   data?.ToArray() ??
                   throw new ArgumentNullException(nameof(data));

            int radix = _radix;
            if (radix == OptimalRadix)
            {
                radix = 1 << Math.Min(32, (int)Math.Log(data.LongCount(), 2));
                Console.WriteLine($"Set optimal radix size to {radix}");
            }

            int max = 0;
            foreach (int item in data)
            {
                if (item < 0)
                {
                    throw new NotSupportedException("Negative numbers are not supported");
                }

                if (item > max)
                {
                    max = item;
                }
            }

            int divisor = 1; 
            int iterations = (int)Math.Log(max, radix) + 1;

            for (int step = 0; step < iterations; ++step)
            {
                ISortEngine<int> subEngine = new HelperEngine(divisor, radix);
                data = subEngine.Sort(data).ToArray();
                divisor *= radix;
            }

            return data;
        }

        private sealed class HelperEngine : CountingSortEngineBase<int>
        {
            private readonly int _divisor;
            private readonly int _modulo;

            public HelperEngine(int divisor, int modulo)
            {
                _divisor = divisor;
                _modulo = modulo;
            }

            protected override int GetItemKey(int item) => (item / _divisor) % _modulo;
        }
    }
}