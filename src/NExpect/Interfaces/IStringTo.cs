namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides a string-specific .To
    /// </summary>
    public interface IStringTo: ITo<string>
    {
        /// <summary>
        /// Begins a test for a substring at the start of the actual string
        /// </summary>
        IStringStart Start { get; }

        /// <summary>
        /// Begins a test for a substring at the end of the actual string
        /// </summary>
        IStringEnd End { get; }

        /// <summary>
        /// Starts a string-specific negated expectation
        /// </summary>
        new IStringNotAfterTo Not { get; }

        /// <summary>
        /// Adds the string-specific .Be syntax
        /// </summary>
        new IStringBe Be { get; }

        /// <summary>
        /// Provides the .Contain of .To.Contain.In.Order(...)
        /// </summary>
        IStringContain Contain { get; }
    }
}