namespace Algo.NumberTheory.Primality
{
    public sealed class EratospheneSieve
    {
        private static readonly ICollection<long> _primes;

        static EratospheneSieve()
        {
            _primes = new HashSet<long>();
        }

        public long FindDivisor(long number)
        {
            return 1;
        }
    }
}