// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Adds the .Deep grammar for collection-count matching
    /// </summary>
    /// <typeparam name="T">Type of object to perform matching against</typeparam>
    public interface ICountMatchDeep<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Provides the .Deep.Equal grammar for collection-count matching
        /// </summary>
        ICountMatchDeepEqual<T> Equal { get; }
    }
}