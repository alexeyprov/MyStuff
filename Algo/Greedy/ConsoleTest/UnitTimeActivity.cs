using System;

namespace Algo.Greedy.ConsoleTest
{
    internal sealed class UnitTimeActivity : IEquatable<UnitTimeActivity>
    {
        public UnitTimeActivity(int id, int dueTime, float penalty)
        {
            Id = id;
            DueTime = dueTime > 0 ? dueTime : throw new ArgumentOutOfRangeException(nameof(dueTime));
            Penalty = penalty > 0.0 ? penalty : throw new ArgumentOutOfRangeException(nameof(penalty));
        }

        public int Id { get; }

        public int DueTime { get; }

        public float Penalty { get; }

        public bool Equals(UnitTimeActivity other)
        {
            return Id == other?.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object other)
        {
            return other is UnitTimeActivity activity &&
                   Equals(activity);
        }
    }
}