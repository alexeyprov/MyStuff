using System;
using System.Collections.Generic;
using System.Numerics;

namespace Algo.DivideAndConquer.FastFourierTransform
{
    public class EvaluatedPolynomial
    {
        public EvaluatedPolynomial(Complex root, IReadOnlyList<Complex> values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values)); 
            Root = root; 
        }

        public IReadOnlyList<Complex> Values { get; }
        
        public Complex Root { get; }
    }
}