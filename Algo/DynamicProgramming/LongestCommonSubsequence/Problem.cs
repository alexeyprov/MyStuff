using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestCommonSubsequence
{
    public sealed class Problem<T>
    {
        public Problem(IEnumerable<T> firstSequence, IEnumerable<T> secondSequence)
        {
            FirstSequence = CheckSequence(firstSequence, nameof(firstSequence));
            SecondSequence = CheckSequence(secondSequence, nameof(secondSequence));
        }

        public IReadOnlyList<T> FirstSequence { get; }

        public IReadOnlyList<T> SecondSequence { get; }

        public static Problem<T> CreateRandom(Func<double, T> elementFactory, int maxLength)
        {
            if (elementFactory == null)
            {
                throw new ArgumentNullException(nameof(elementFactory));
            }

            if (maxLength <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            Random random = new Random();
            return new Problem<T>(
                GetRandomSequence(random, maxLength, elementFactory),
                GetRandomSequence(random, maxLength, elementFactory));
        }

        private static IEnumerable<T> GetRandomSequence(Random random, int maxLength, Func<double, T> elementFactory)
        {
            return Enumerable
                .Range(0, random.Next(1, maxLength + 1))
                .Select(_ => elementFactory(random.NextDouble()))
                .ToArray();
        }

        private IReadOnlyList<T> CheckSequence(IEnumerable<T> sequence, string parameterName)
        {
            IReadOnlyList<T> list = sequence as IReadOnlyList<T> ??
                                    sequence?.ToArray() ??
                                    throw new ArgumentNullException(parameterName);
            return list.Count > 0 ? list : throw new ArgumentException(parameterName);
        }
    }
}
