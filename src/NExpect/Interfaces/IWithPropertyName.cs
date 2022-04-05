namespace NExpect.Interfaces;

/// <summary>
/// Extends IWith&lt;T&gt; to also carry a property name
/// </summary>
/// <typeparam name="T"></typeparam>
internal interface IWithPropertyName<T> : IWith<T>
{
    /// <summary>
    /// The name of the property carried with this instance
    /// </summary>
    string PropertyName { get; }
}