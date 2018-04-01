// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Intersection part of .Intersection.Equal for collection count-matching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICountMatchIntersection<T>: ICountMatch<T>
    {
        /// <summary>
        /// Provides the .Intersection.Equal grammar for collection-count matching
        /// </summary>
        ICountMatchIntersectionEqual<T> Equal { get; }
    }
}