namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides ".To" after a ".Not"
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IToAfterNot<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Starts a similarity expectation
        /// </summary>
        IBe<T> Be { get; }
        /// <summary>
        /// Starts a containing expectation
        /// </summary>
        IContain<T> Contain { get; }
        /// <summary>
        /// Starts a property expectation
        /// </summary>
        IHave<T> Have { get; }
    }
}