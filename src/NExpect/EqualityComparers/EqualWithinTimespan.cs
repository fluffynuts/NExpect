using System;
using System.Collections.Generic;

namespace NExpect.EqualityComparers
{
    /// <summary>
    /// Tests for equality with an allowed drift
    /// </summary>
    public class EqualWithinTimespan : IEqualityComparer<DateTime>
    {
        /// <summary>
        /// How much drift is allowed between two datetimes
        /// and still consider them equal
        /// </summary>
        public TimeSpan AllowedDrift { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allowedDrift"></param>
        public EqualWithinTimespan(TimeSpan allowedDrift)
        {
            AllowedDrift = allowedDrift;
        }

        /// <summary>
        /// Tests if x is within allowed timespan of y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(DateTime x,
            DateTime y)
        {
            var delta = Math.Abs((x - y).TotalMilliseconds);
            var allowed = Math.Abs(AllowedDrift.TotalMilliseconds);
            return delta <= allowed;
        }

        /// <summary>
        /// Just gets the hashcode for the datetime, NOT TAKING
        ///  ALLOWED DELTA INTO ACCOUNT (yet);
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(DateTime obj)
        {
            return obj.GetHashCode(); // should really take the allowed drift

            //  into account, but not sure how yet
        }
    }
}