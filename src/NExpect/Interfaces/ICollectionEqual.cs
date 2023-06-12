// ReSharper disable InheritdocConsiderUsage

namespace NExpect.Interfaces;

/// <summary>
/// Provides the ".Equal" grammar continuation for collections
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICollectionEqual<T> 
    : ICanAddCollectionMatcher<T>
{
}