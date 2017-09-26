namespace NExpect.Interfaces
{
    /// <summary>
    /// An Expectation, for a type T
    /// </summary>
    /// <typeparam name="T">Type to expect around</typeparam>
    public interface IExpectation<T>
    {
        /// <summary>
        /// Actual value stored in the expectation
        /// </summary>
        T Actual { get; }

        /// <summary>
        /// To continuation, ie Expect(value).To...
        /// </summary>
        ITo<T> To { get; }
        /// <summary>
        /// Not continuation, ie, Expect(value).Not...
        /// </summary>
        INot<T> Not { get; }
    }

    /// <summary>
    /// Provides a string-specific expectation with a few string-specific features
    /// </summary>
    public interface IStringExpectation: IExpectation<string>
    {
        /// <summary>
        /// Starts a string-specific .To
        /// </summary>
        new IStringTo To { get; }

        /// <summary>
        /// Starts a string-specific .Not
        /// </summary>
        new IStringNot Not { get; }
    }

    /// <summary>
    /// Starts a negated expectation for a string Actual
    /// </summary>
    public interface IStringNot: INot<string>
    {
        /// <summary>
        /// Starts a string-specific .To
        /// </summary>
        new IStringToAfterNot To { get; }

        /// <summary>
        /// Starts shorter grammar negated start test
        /// </summary>
        IStringStart Start { get; }

        /// <summary>
        /// Starts shorter grammar negated end test
        /// </summary>
        IStringEnd End { get; }
    }

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
    }

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
    }

    /// <summary>
    /// Provides an extension point to test if a string starts with an expected
    /// value
    /// </summary>
    public interface IStringStart: ICanAddMatcher<string>
    {
    }

    /// <summary>
    /// Provides an extension point to test if a string ends with an expected
    /// value
    /// </summary>
    public interface IStringEnd: ICanAddMatcher<string>
    {
    }
}