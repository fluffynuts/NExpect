namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the negated expectations of other types with the added 
    /// string-specific extensions
    /// </summary>
    public interface IStringNotAfterTo: INotAfterTo<string>
    {
        /// <summary>
        /// Tests (negatively) for a string starting with another
        /// </summary>
        IStringStart Start { get; }

        /// <summary>
        /// Tests (negatively) for a string ending with another
        /// </summary>
        IStringEnd End { get; }

        /// <summary>
        /// Provides the string-specific .To.Not.Be
        /// </summary>
        new IStringBe Be { get; }
    }
}