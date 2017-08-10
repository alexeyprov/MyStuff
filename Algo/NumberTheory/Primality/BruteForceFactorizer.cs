namespace Algo.NumberTheory.Primality
{
    public sealed class BruteForceFactorizer
    {
        public IEnumerable<(long factor, long exp)> Factorize(long number)
        {
            if (number <= 0)
            {
                throw new ArgumentException(nameof(number));
            }

            List<(long factor, long exp)> factors = new List<(long, long)>();

            // ...

            return factors;
        }
    }
}