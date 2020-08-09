using System;

using Algo.DynamicProgramming.Entities;

namespace Algo.DynamicProgramming.OptimalBst
{
    internal sealed class Solver
    {
        public Solution FindSolution(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            int n = problem.Size;
            FrugalMatrix<float> expectations = new FrugalMatrix<float>(n, 1),
                                weights = new FrugalMatrix<float>(n, 1);
            FrugalMatrix<int> roots = new FrugalMatrix<int>(n);

            for (int i = 0; i <= n; ++i)
            {
                float q = problem.DummyKeyProbabilities[i];
                expectations[i + 1, i] = q;
                weights[i + 1, i] = q;
            }

            for (int t = 0; t < n; ++t)
            {
                // filling "main + t" diagonal
                for (int i = 1, max = n - t; i <= max; ++i)
                {
                    int j = i + t;

                    // adding up weights
                    weights[i, j] = weights[i, j - 1] + 
                                    problem.DummyKeyProbabilities[j] +
                                    problem.RealKeyProbabilities[j];

                    // calculating bracket with the top at (i, j = i + t)
                    float min = float.MaxValue;
                    int pos = 0; 
                    for (int r = i; r <= j; ++r)
                    {
                        float sum = expectations[i, r - 1] + expectations[r + 1, j];
                        if (sum < min)
                        {
                            min = sum;
                            pos = r;
                        }
                    }

                    expectations[i, j] = min + weights[i, j];
                    roots[i, j] = pos;
                }
            }

            return new Solution(roots, expectations[1, n]);
        }
    }
}