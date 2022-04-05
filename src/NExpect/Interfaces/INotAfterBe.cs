// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces;

/// <summary>
/// Continuation of Not after Be, ie ...Not.Be...
/// </summary>
/// <typeparam name="T">Underlying type of the continuation</typeparam>
public interface INotAfterBe<T>: IBe<T>
{
}