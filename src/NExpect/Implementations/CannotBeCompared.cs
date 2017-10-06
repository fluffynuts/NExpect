using System;

namespace NExpect.Implementations
{
    internal abstract class CannotBeCompared 
    {
        public override bool Equals(object obj)
        {
            throw new InvalidOperationException(
                "You probably intend to use .Equal(), not .Equals()"
            );
        }

        public override int GetHashCode()
        {
            throw new InvalidOperationException(
                "You probably shouldn't be hashing this."
            );
        }
    }
}