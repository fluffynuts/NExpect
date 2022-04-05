namespace NExpect.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPropertyAnd<T> : IAnd<T>
{
    /// <summary>
    /// The .With continuation for property matching
    /// </summary>
    new IPropertyWith<T> With { get; }
}