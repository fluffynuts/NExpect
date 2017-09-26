namespace NExpect.Interfaces
{
    /// <inheritdoc />
    public interface IStringToAfterNot: IToAfterNot<string>
    {
        /// <summary>
        /// Starts a negated .Start expectation
        /// </summary>
        IStringStart Start { get; }

        /// <summary>
        /// Starts a negated .End expectation
        /// </summary>
        IStringEnd End { get; }
    }
}