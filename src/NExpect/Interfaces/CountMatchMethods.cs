namespace NExpect.Interfaces
{
    /// <summary>
    /// CountMatchMethod to use looking at the number of items, eg in Exactly()
    /// </summary>
    public enum CountMatchMethods
    {
        /// <summary>
        /// Exact match
        /// </summary>
        Exactly,
        /// <summary>
        /// Minimum match (At.Least)
        /// </summary>
        Minimum,
        /// <summary>
        /// Maximum match (At.Most())
        /// </summary>
        Maximum,
        /// <summary>
        /// Match any
        /// </summary>
        Any,
        /// <summary>
        /// Match all
        /// </summary>
        All
    }
}