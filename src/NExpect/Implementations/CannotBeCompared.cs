using System;

namespace NExpect.Implementations
{
    /// <summary>
    /// Defines a class which cannot be compared to any other
    /// (throws exceptions when you try). This prevents accidental
    /// usage of .Equal() instead of .Equals() in expectations.
    /// </summary>
    public abstract class CannotBeCompared 
    {
        /// <summary>
        /// Throws when invoked. Not to be used to check equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override bool Equals(object obj)
        {
            throw new InvalidOperationException(
                "You probably intend to use .Equal(), not .Equals()"
            );
        }

        /// <summary>
        /// Throws when invoked. Why do you want to hash an expectation,
        /// grasshopper?
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public override int GetHashCode()
        {
            throw new InvalidOperationException(
                "You probably shouldn't be hashing this."
            );
        }
    }
}