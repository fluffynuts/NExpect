namespace NExpect.Interfaces
{
    /// <summary>
    /// Allows user-chaining of .And (ie, "More expectations plz")
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMore<T>
    {
        /// <summary>
        /// Provides the .And grammar extension
        /// </summary>
        IAnd<T> And { get; }

        /// <summary>
        /// Provides the .With grammar extension
        /// </summary>
        IWith<T> With { get; }

        /// <summary>
        /// Provides the .Of grammar extension
        /// </summary>
        IOf<T> Of { get; }

        /// <summary>
        /// Provides the .By grammar extension
        /// </summary>
        IBy<T> By { get; }

        /// <summary>
        ///  Provides the .Max grammar extension
        /// </summary>
        IMax<T> Max { get; }

        /// <summary>
        /// Provides the .To grammar extension
        /// </summary>
        ITo<T> To { get; }

        /// <summary>
        /// Provides the .Which grammar extension
        /// </summary>
        IWhich<T> Which { get; }

        /// <summary>
        /// Provides the .Without grammar extension
        /// </summary>
        IWithout<T> Without { get; }
    }
}
