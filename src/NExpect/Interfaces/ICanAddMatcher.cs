// ReSharper disable UnusedTypeParameter
namespace NExpect.Interfaces
{
    /// <summary>
    /// Most general interface for object which should allow the .AddMatcher() syntax
    /// </summary>
    /// <typeparam name="T">Type of the object being carried through the continuation</typeparam>
    public interface ICanAddMatcher<T>
    {
    }
}