namespace NExpect.Interfaces
{
    /// <summary>
    /// Method to use looking at the number of items, eg in Exactly()
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
        /// Maximum matc (At.Most())
        /// </summary>
        Maximum
    }
}