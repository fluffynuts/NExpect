// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides .And members
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAnd<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Provides the .And.A extension point
        /// </summary>
        IA<T> A { get; }
        /// <summary>
        /// Provides the .And.An extension point
        /// </summary>
        IAn<T> An { get; }
        /// <summary>
        /// Provides the .And.Have extension point
        /// </summary>
        IHave<T> Have { get; }
        /// <summary>
        /// Provides the .And.Not extension point
        /// </summary>
        IPropertyNot<T> Not { get; }
        /// <summary>
        /// Provides the .And.To extension point
        /// </summary>
        ITo<T> To { get; }
    }
}