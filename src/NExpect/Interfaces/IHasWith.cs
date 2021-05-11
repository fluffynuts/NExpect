namespace NExpect.Interfaces
{
    /// <summary>
    /// Used to continue reflective property assertions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasWith<T>
    {
        /// <summary>
        /// Continues the .Property().With syntax
        /// </summary>
        IWith<T> With { get; }
    }
}