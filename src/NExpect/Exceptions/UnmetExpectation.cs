using System;

namespace NExpect.Exceptions
{
    /// <summary>
    /// Mimicks the NUnit AssertionException if it cannot be dynamically located
    /// </summary>
    public class UnmetExpectation : Exception
    {
        public UnmetExpectation(string message) : base(message)
        {
        }
    }
}