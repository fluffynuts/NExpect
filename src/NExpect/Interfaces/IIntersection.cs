namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Intersection grammar extension
    /// </summary>
    /// <typeparam name="T">Type of thec ontinuation</typeparam>
    public interface IIntersection<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IIntersectionEqual<T> Equal { get; }
    }
}