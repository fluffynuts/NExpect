namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation for strings pulled out of exceptions
    /// </summary>
    public interface IStringPropertyContinuation 
        : ICanAddMatcher<string>
    {
        /// <summary>
        /// Negates the continuation
        /// </summary>
        IStringPropertyNot Not { get; }

        /// <summary>
        /// Facilitates further expectations against the string
        /// </summary>
        IStringPropertyContinuation And { get; }

        /// <summary>
        /// Facilitates the .Equal.To() syntax
        /// </summary>
        IEqualityContinuation<string> Equal { get; }

        /// <summary>
        /// Facilitates the .Ending.With() syntax
        /// </summary>
        IStringPropertyEndingContinuation Ending { get; }

        /// <summary>
        /// Facilitates the .Starting.With syntax
        /// </summary>
        IStringPropertyStartingContinuation Starting { get; }
    }
}