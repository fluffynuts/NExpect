namespace NExpect.Interfaces;

/// <summary>
/// Continue on to Intersection Equality Testing for dictionary value
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDictionaryValueIntersection<T>
{
    /// <summary>
    /// Continue on to Intersection Equality Testing for dictionary value
    /// </summary>
    IDictionaryValueEqual<T> Equal { get; }
}