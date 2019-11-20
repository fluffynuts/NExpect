// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the extension point for .Value() matchers
    /// </summary>
    public interface IDictionaryValueWith<T> : ICanAddMatcher<T>
    {
        /// <summary>
        /// Allows the syntax to progress to deep equality testing
        /// </summary>
        IDictionaryValue<T> Value { get; }
        
    }
} 