// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Not" negation, to come before a ".To"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPropertyNot<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Provides the ".To" after ".Not"
        /// </summary>
        IToAfterNot<T> To { get; }
    }
}