// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the ".Contain" grammar extension
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContain<T> : ICanAddMatcher<T>, IHasActual<T>
    {
        /// <summary>
        /// Prepares for setting an expected count to test for, eg:
        /// At.Least, At.Most
        /// </summary>
        IContainAt<T> At { get; }
    }
}