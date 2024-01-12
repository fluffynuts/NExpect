using System;
using System.Collections.Generic;

namespace NExpect.Implementations
{
    internal class MaxDeltaComparer
        : IEqualityComparer<decimal>
    {
        private readonly decimal _within;

        public MaxDeltaComparer(decimal within)
        {
            _within = Math.Abs(within);
        }

        public bool Equals(decimal x, decimal y)
        {
            return Math.Abs(x - y) < _within;
        }

        public int GetHashCode(decimal obj)
        {
            throw new NotImplementedException();
        }
    }
}