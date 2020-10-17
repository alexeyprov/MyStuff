using System;
using System.Collections.Generic;

using Algo.Greedy.Matroids;

namespace Algo.Greedy.ConsoleTest
{
    internal sealed class UnitTimeActivityMatroid : IMatroid<float, UnitTimeActivity>
    {
        private readonly ISet<UnitTimeActivity> _set;

        public UnitTimeActivityMatroid()
        {
            _set = new HashSet<UnitTimeActivity>();
        }

        public UnitTimeActivityMatroid(params UnitTimeActivity[] activities)
        {
            _set = new HashSet<UnitTimeActivity>(activities);
        }

        ISet<UnitTimeActivity> IMatroid<float, UnitTimeActivity>.Set => _set;

        float IMatroid<float, UnitTimeActivity>.GetWeight(UnitTimeActivity value)
        {
            return value?.Penalty ?? throw new ArgumentNullException(nameof(value));
        }

        bool IMatroid<float, UnitTimeActivity>.IsIndependent(ISet<UnitTimeActivity> @set, UnitTimeActivity value)
        {
            if (@set == null)
            {
                throw new ArgumentNullException(nameof(@set));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.DueTime > @set.Count;
        }
    }
}