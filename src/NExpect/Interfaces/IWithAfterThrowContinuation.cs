using System;

namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".With" grammar extension after .Throw() or .Throw&lt;&gt;
    /// </summary>
    public interface IWithAfterThrowContinuation : ICanAddMatcher<Exception>
    {
        /// <summary>
        /// Provides the ".Message" continuation for testing exception message contents
        /// </summary>
        IExceptionMessageContinuation Message { get; }
    }
}