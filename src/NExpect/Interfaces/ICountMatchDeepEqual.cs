// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Deep grammar for .Deep.Equal for collection count-matching
    /// </summary>
    /// <typeparam name="T">Type of collection item</typeparam>
    public interface ICountMatchDeepEqual<T>: ICanAddMatcher<T>, ICountMatch
    {
    }
}