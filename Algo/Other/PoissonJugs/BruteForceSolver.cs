using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PoissonJugs
{
    internal sealed class BruteForceSolver : BaseSolver
    {
        private readonly Stack<IList<int>> _steps;

        public BruteForceSolver()
        {
            _steps = new Stack<IList<int>>();
        }

        protected override IEnumerable<IList<int>> SeekSolution(IList<int> initialStep)
        {
            _steps.Clear();
            return TryPourings(initialStep) ? _steps.Reverse() : null;    
        }

        private bool TryPourings(IList<int> currentStep)
        {
            IList<int> jugVolumes = Volumes;
            int goal = Goal;

            // validate proposed step
            
            // redundant sanity check (debug only):
            // vessels should have valid amount of liquid
#if DEBUG

            for (int i = 0; i < jugVolumes.Length; ++i)
            {
                if (currentStep[i] < 0 || currentStep[i] > jugVolumes[i])
                {
                    Debug.Fail(string.Format("Invalid state of jug #{0}: {1}", i + 1, currentStep[i]));
                }
            }
#endif

            // amount of liquid shouldn't repeat to avoid infinite recursion
            foreach (IList<int> step in _steps)
            {
                if (Enumerable.SequenceEqual(step, currentStep))
                {
                    return false;
                }
            }

            _steps.Push(currentStep);

            // have we found a solution yet?
            for (int i = jugVolumes.Count - 1; i >= 0 && jugVolumes[i] >= goal; --i)
            {
                if (goal == currentStep[i])
                {
                    return true;
                }
            }

            // now try pourings
            for (int i = 0; i < jugVolumes.Count; ++i)
            {
                // can pour from i?
                if (currentStep[i] > 0)
                {
                    for (int j = 0; j < jugVolumes.Count; ++j)
                    {
                        IList<int> newStep = Pour(currentStep, i, j);
                        if (newStep != null && TryPourings(newStep))
                        {
                            return true;
                        }
                    }
                }
            }

            _steps.Pop();
            return false;
        }
    }
}