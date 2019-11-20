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
    }
}