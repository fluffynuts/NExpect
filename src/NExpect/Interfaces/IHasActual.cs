namespace NExpect.Interfaces
{
    /// <summary>
    /// Interface to implement to surface the Actual value
    /// </summary>
    /// <typeparam name="T">Type of Actual</typeparam>
    public interface IHasActual<out T>
    {
        T Actual { get; }
    }
}