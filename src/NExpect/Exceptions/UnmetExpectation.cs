using System;

namespace NExpect.Exceptions
{
    /// <summary>
    /// Mimicks the NUnit AssertionException if it cannot be dynamically located
    /// </summary>
    public class UnmetExpectation : Exception
    {
        /// <summary>
        /// Constructs the exception
        /// </summary>
        /// <param name="message">Message to display</param>
        public UnmetExpectation(string message) : base(message)
        {
        }
    }
}