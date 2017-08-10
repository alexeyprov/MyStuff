using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Algo.DivideAndConquer.FastFourierTransform
{
    public static class PolyMath
    {
        public static Polynomial Multiply(Polynomial left, Polynomial right)
        {
            int power = FindComplexRoot(Math.Max(left.Degree, right.Degree));
            Debug.Assert(power >= left.Degree + 1);
            Debug.Assert(power >= right.Degree + 1);

            Polynomial expandedLeft = Expand(left, power - 1);
            Polynomial expandedRight = Expand(right, power - 1);
            
            Polynomial rotatedLeft = ApplyFourierTransform(expandedLeft, power);
            Polynomial rotatedRight = ApplyFourierTransform(expandedRight, power);

            Polynomial product = new Polynomial(
                rotatedLeft.Coefficients
                    .Zip(
                        rotatedRight.Coefficients,
                        (l, r) => l* r)
                    .ToArray());

            return ApplyFourierTransform(product, -power).Scale(Complex.One / (double)power);
        }

        private static int FindComplexRoot(int degree)
        {
            // Find power = 2^k: 2^(k-1) < 2*degree < 2^k and root^power = 1
            int shift = (int)Math.Ceiling(Math.Log(degree + 1, 2)) + 1;
            return 1 << shift;
        }

        private static Polynomial ApplyFourierTransform(Polynomial polynomial, int power)
        {
            if (polynomial.Degree == 0)
            {
                Debug.Assert(power == 1 || power == -1, $"Power is expected to be {-1, 1}, but the actual value is {power}");
                return polynomial;
            }

            int capacity = (polynomial.Degree + 1) / 2 + 1;
            List<Complex> evenData = new List<Complex>(capacity);
            List<Complex> oddData = new List<Complex>(capacity);
            IReadOnlyList<Complex> coeffs = polynomial.Coefficients;

            for (int i = 0; i < coeffs.Count; ++i)
            {
                if ((i % 2) == 0)
                {
                    evenData.Add(coeffs[i]);
                }
                else
                {
                    oddData.Add(coeffs[i]);
                }
            }

            Polynomial evenPolynomial = ApplyFourierTransform(
                new Polynomial(evenData.ToArray()),
                power / 2);
            Polynomial oddPolynomial = ApplyFourierTransform(
                new Polynomial(oddData.ToArray()),
                power / 2);

            Complex[] resultCoeffs = new Complex[polynomial.Degree + 1];
            int childSize = evenPolynomial.Degree + 1;
            for (int i = 0; i < childSize; ++i)
            {
                Complex multiplier = Complex.FromPolarCoordinates(1, 2 * Math.PI * i / power);
                resultCoeffs[i] = evenPolynomial.Coefficients[i] + multiplier * oddPolynomial.Coefficients[i];
                resultCoeffs[i + childSize] = evenPolynomial.Coefficients[i] - multiplier * oddPolynomial.Coefficients[i];    
            }

            return new Polynomial(resultCoeffs);
        }

        private static Polynomial Expand(Polynomial p, int power) => new Polynomial(
                p.Coefficients
                    .Concat(Enumerable.Repeat(Complex.Zero, power - p.Degree))
                    .ToArray());
    }
}