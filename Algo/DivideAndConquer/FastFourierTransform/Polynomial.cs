using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algo.DivideAndConquer.FastFourierTransform
{
    public class Polynomial
    {
        private const double RoundingThreshold = 0.000001D;

        public Polynomial(params Complex[] coefficients)
        {
            Coefficients = coefficients;
        }

        public Polynomial(params double[] coefficients)
        {
            Coefficients = coefficients.Select(c => (Complex)c).ToArray();
        }

        public IReadOnlyList<Complex> Coefficients { get; }

        public int Degree => Coefficients.Count - 1;

        public Complex ValueAt(Complex x)
        {
            Complex value = 1D;
            return Coefficients.Aggregate(
                (Complex)0D,
                (c, s) => 
                { 
                    Complex sum = c * value + s; 
                    value *= x; 
                    return sum;
                });
        }

        public Polynomial Scale(Complex multiplier)
        {
            return new Polynomial(
                Coefficients.Select(c => c * multiplier).ToArray());
        }

        public override string ToString()
        {
            return ToString(false);
        }

        public string ToString(bool shouldRound) =>
            string.Join(
                " + ",
                Coefficients
                    .Select((c, i) => FormatMember(c, i, shouldRound))
                    .Where(s => !string.IsNullOrEmpty(s)));

        private static string FormatMember(Complex coefficient, int power, bool shouldRound)
        {
            object roundedCoefficient = coefficient;
            bool useMultiplicationSign = true;

            if (shouldRound && Math.Abs(coefficient.Imaginary) < RoundingThreshold)
            {
                double realCoefficient = coefficient.Real;

                if (Math.Abs(realCoefficient) < RoundingThreshold)
                {
                    return string.Empty;
                }

                roundedCoefficient = realCoefficient;

                if (Math.Abs(realCoefficient - 1D) < RoundingThreshold)
                {
                    useMultiplicationSign = false;
                }
            }

            switch (power)
            {
                case 0:
                    return roundedCoefficient.ToString();
                case 1:
                    return useMultiplicationSign ? $"{roundedCoefficient} * x" : "x";
                default:
                    return useMultiplicationSign ? $"{roundedCoefficient} * x^{power}" : $"x^{power}";
            }
        }
    }
}