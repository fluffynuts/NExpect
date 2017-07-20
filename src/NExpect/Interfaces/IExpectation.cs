namespace NExpect.Interfaces
{
    /// <summary>
    /// An Expectation, for a type T
    /// </summary>
    /// <typeparam name="T">Type to expect around</typeparam>
    public interface IExpectation<T>
    {
        /// <summary>
        /// Actual value stored in the expectation
        /// </summary>
        T Actual { get; }

        /// <summary>
        /// To continuation, ie Expect(value).To...
        /// </summary>
        ITo<T> To { get; }
        /// <summary>
        /// Not continuation, ie, Expect(value).Not...
        /// </summary>
        INot<T> Not { get; }
    }

}