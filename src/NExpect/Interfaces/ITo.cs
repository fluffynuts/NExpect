// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the initial ".To" grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITo<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Negates the expectation
        /// </summary>
        INotAfterTo<T> Not { get; }
        /// <summary>
        /// Starts an expectation some kind of similarity
        /// </summary>
        IBe<T> Be { get; }
        /// <summary>
        /// Starts an expectation for some property
        /// </summary>
        IHave<T> Have { get; }
    }
}