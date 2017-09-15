namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .And on .Greater.Than
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGreaterThan<T> 
    {
        /// <summary>
        /// Prepares to test for more than just greatness
        /// </summary>
        IGreaterThanAnd<T> And { get; }
    }
}