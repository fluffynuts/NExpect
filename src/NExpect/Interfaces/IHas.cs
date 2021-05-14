namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Has extension point
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHas<T>
        : ICanAddMatcher<T>
    {
        /// <summary>
        /// Provides the .Has.A extension point
        /// </summary>
        IA<T> A { get; }
        /// <summary>
        /// Provides the .Has.An extension point
        /// </summary>
        IAn<T> An { get; }
        /// <summary>
        /// Provides the .Has.Max extension point
        /// </summary>
        IMax<T> Max { get; }
    }
}