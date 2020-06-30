using System;
using System.Diagnostics;

namespace Algo.DynamicProgramming.AssemblyLineBalancing
{
    /// <summary>
    /// With assembly line i in [0, 1] and stage j in [0, n-1],
    /// stage costs A[i, j] and &quot;transfer to&quot; costs T[i, j], the recurrent solution is:
    /// C[i, j] = min { C[i, j - 1], C[1 - i, j - 1] + T[i, j] } + A[i, j]
    /// </summary>
    internal sealed class Solver
    {
        public Solution FindSolution(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            int size = problem.Size;
            Solution solution = new Solution(size + 1);

            solution.Costs[0, 0] = problem.EntryCosts[0] + problem.StageCosts[0, 0];
            solution.Costs[1, 0] = problem.EntryCosts[1] + problem.StageCosts[1, 0];

            for (int stageIndex = 1; stageIndex < size; ++stageIndex)
            {
                MoveToStage(problem, solution, 0, stageIndex);
                MoveToStage(problem, solution, 1, stageIndex);
            }

            solution.Costs[0, size] = solution.Costs[0, size - 1] + problem.ExitCosts[0];
            solution.Costs[1, size] = solution.Costs[1, size - 1] + problem.ExitCosts[1];
            solution.Paths[0, size] = 0;
            solution.Paths[1, size] = 1;

            return solution;
        }

        private void MoveToStage(Problem problem, Solution solution, byte lineIndex, int stageIndex)
        {
            Debug.Assert(stageIndex >= 1);

            int predecessor = stageIndex - 1;
            byte otherLineIndex = (byte)(1 - lineIndex);
            int ownCost = solution.Costs[lineIndex, predecessor];
            int transferCost = solution.Costs[otherLineIndex, predecessor] + 
                               problem.TransferCosts[lineIndex, stageIndex];

            solution.Costs[lineIndex, stageIndex] = problem.StageCosts[lineIndex, stageIndex];
            if (ownCost <= transferCost)
            {
                solution.Costs[lineIndex, stageIndex] += ownCost;
                solution.Paths[lineIndex, stageIndex] = lineIndex; 
            }
            else
            {
                solution.Costs[lineIndex, stageIndex] += transferCost;
                solution.Paths[lineIndex, stageIndex] = otherLineIndex;
            }
        }
    }
}