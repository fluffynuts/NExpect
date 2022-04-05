namespace NExpect.Interfaces;

/// <summary>
/// Continue on to Deep Equality Testing for dictionary value
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDictionaryValueDeep<T>
{
    /// <summary>
    /// Continue on to Deep Equality Testing for dictionary value
    /// </summary>
    IDictionaryValueEqual<T> Equal { get; }
}