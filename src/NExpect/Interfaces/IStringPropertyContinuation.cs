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
        /// 
        /// </summary>
        IStringPropertyContinuation And { get; }

        /// <summary>
        /// 
        /// </summary>
        IEqualityContinuation<string> Equal { get; }

    }
}