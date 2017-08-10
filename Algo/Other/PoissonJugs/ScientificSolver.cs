using System.Collections.Generic;

namespace PoissonJugs
{
    internal sealed class ScientificSolver : BaseSolver
    {
        private readonly Queue<IList<int>> _steps;

        public ScientificSolver()
        {
            _steps = new Queue<IList<int>>();
        }

        protected override IEnumerable<IList<int>> SeekSolution(IList<int> initialStep)
        {
            int count = initialStep.Count;

            _steps.Clear();
            _steps.Enqueue(initialStep);

            IList<int> step = initialStep;
            int goal = Goal;
            IList<int> jugVolumes = Volumes;

            while (step[count - 1] != goal)
            {
                if (step[count - 2] == jugVolumes[count - 2])
                {
                    // when the second biggest jar is full, pour it into the biggest one
                    step = ShiftRight(step);
                }
                else
                {
                    // keep pouring from the biggest jar, until the second biggest jar is full
                    step = ShiftLeft(step);
                }
            }

            return _steps;
        }

        private IList<int> ShiftRight(IList<int> step)
        {
            for (int index = Volumes.Count - 1; index > 0; --index)
            {
                step = Pour(step, index - 1, index);
                _steps.Enqueue(step);
            }

            return step;
        }

        private IList<int> ShiftLeft(IList<int> step)
        {
            int count = Volumes.Count;

            step = Pour(step, count - 1, 0);
            _steps.Enqueue(step);

            for (int index = 1; index <= count - 2; ++index)
            {
                step = Pour(step, index - 1, index);
                _steps.Enqueue(step);
            }

            return step;
        }
    }
}