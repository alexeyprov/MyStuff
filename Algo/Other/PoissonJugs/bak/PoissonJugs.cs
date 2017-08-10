using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PoissonJugs
{
	static class Program
	{
		private const int SOUGHT_VOLUME = 6;
		
		private static readonly int[] _jugVolumes;

		private static IList<int[]> _steps;

		static Program()
		{
			_steps = new List<int[]>();
			_jugVolumes = new int[] { 12, 8, 5 }; // ordered descending
			
		}

		private static void Main()
		{
			if (TryPourings(new int[] { _jugVolumes[0], 0, 0 }))
			{
				foreach (int[] step in _steps)
				{
					Console.WriteLine(String.Join(", ", step));
				}
			}
			else
			{
				Console.WriteLine("Unable to find solution.");
			}
		}

		private static bool TryPourings(int[] currentStep)
		{
			// validate proposed step
			
			// redundant sanity check (debug only):
			// vessels should have valid amount of liquid
#if DEBUG
			for (int i = 0; i < _jugVolumes.Length; ++i)
			{
				if (currentStep[i] < 0 || currentStep[i] > _jugVolumes[i])
				{
					Debug.Fail(String.Format("Invalid state of jug #{0}: {1}", i + 1, currentStep[i]));
				}
			}
#endif

			// amount of liquid shouldn't repeat to avoid infinite recursion
			foreach (int[] step in _steps)
			{
				if (Enumerable.SequenceEqual(step, currentStep))
				{
					return false;
				}
			}

			_steps.Add(currentStep);

			// have we found a solution yet?
			for (int i = 0; i < _jugVolumes.Length && _jugVolumes[i] >= SOUGHT_VOLUME; ++i)
			{
				if (SOUGHT_VOLUME == currentStep[i])
				{
					return true;
				}
			}

			// now try pourings
			for (int i = 0; i < _jugVolumes.Length; ++i)
			{
				// can pour from i?
				if (currentStep[i] > 0)
				{
					for (int j = 0; j < _jugVolumes.Length; ++j)
					{
						// can pour into j?
						if (i != j && currentStep[j] < _jugVolumes[j])
						{
							int amount = Math.Min(currentStep[i], _jugVolumes[j] - currentStep[j]);
				
							Debug.Assert(amount > 0, "Trying to pour negative volume.");

							int[] newStep = (int[]) currentStep.Clone();
							newStep[i] -= amount;
							newStep[j] += amount;

							if (TryPourings(newStep))
							{
								return true;
							}
						}
					}
				}
			}

			_steps.RemoveAt(_steps.Count - 1);
			return false;
		}
	}
}                 