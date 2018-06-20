namespace NExpect.Interfaces
{
    /// <summary>
    /// String property Not dangle-point
    /// </summary>
    public interface IStringPropertyNot: IPropertyNot<string>
    {
        /// <summary>
        /// Provides the negated .Not.Ending.With() syntax
        /// </summary>
        IStringPropertyEndingContinuation Ending { get; }
        /// <summary>
        /// Provides the negated .Not.Ending.With() syntax
        /// </summary>
        IStringPropertyEndingContinuation Starting { get; }
    }
}