namespace NExpect.Interfaces
{
    /// <summary>
    /// Continue on to Deep Equality Testing for dictionary value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDictionaryValue<T>
    {
        /// <summary>
        /// Continue on to Deep Equality Testing for dictionary value
        /// </summary>
        IDictionaryValueDeep<T> Deep { get; }

        /// <summary>
        /// Continue on to Intersection Equality Testing for the dictionary value
        /// </summary>
        IDictionaryValueIntersection<T> Intersection { get; }
    }
}