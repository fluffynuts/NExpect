using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers
{
    /// <summary>
    /// Provides an equality comparer to pass into
    /// deep equality tests to allow for minor date/time
    /// drifts, eg when data goes into, and then comes
    /// back out of a database
    /// </summary>
    public class ApproximateDateTimeEqualityComparer: IEqualityComparer<DateTime>
    {
        private readonly double _allowableDriftMs;

        /// <summary>
        /// Construct the approximate comparer allowing 1s of drift
        /// </summary>
        public ApproximateDateTimeEqualityComparer() : this(1000)
        {
        }

        /// <summary>
        /// Construct the approximate comparer allowing the
        /// configured number of ms drift
        /// </summary>
        /// <param name="allowedDriftMilliseconds"></param>
        public ApproximateDateTimeEqualityComparer(int allowedDriftMilliseconds) : this(
            TimeSpan.FromMilliseconds(allowedDriftMilliseconds)
        )
        {
        }

        /// <summary>
        /// Construct the approximate comparer allowing the
        /// configured timespan drift
        /// </summary>
        /// <param name="allowableDrift"></param>
        public ApproximateDateTimeEqualityComparer(TimeSpan allowableDrift)
        {
            _allowableDriftMs = allowableDrift.TotalMilliseconds;
        }

        /// <inheritdoc />
        public bool Equals(DateTime x, DateTime y)
        {
            var delta = Math.Abs((x - y).TotalMilliseconds);
            return delta < _allowableDriftMs;
        }

        /// <inheritdoc />
        public int GetHashCode(DateTime obj)
        {
            return obj.GetHashCode();
        }
    }
}