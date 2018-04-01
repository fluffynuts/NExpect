namespace NExpect.Interfaces
{
    /// <summary>
    /// Provides the .Intersection grammar for .Intersection.Equal for collection count-matching
    /// </summary>
    /// <typeparam name="T">Type of collection item</typeparam>
    public interface ICountMatchIntersectionEqual<T>: ICountMatch<T>
    {
    }
}