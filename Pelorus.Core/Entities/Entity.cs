namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all objects.
    /// </summary>
    /// <typeparam name="TKey">Type of the key for this entity.</typeparam>
    public abstract class Entity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Entity's primary key.
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Base class for all objects with key type of long.
    /// </summary>
    public abstract class Entity : Entity<long>
    {
    }
}
