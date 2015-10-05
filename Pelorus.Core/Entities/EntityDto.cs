namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all data transfer objects.
    /// </summary>
    /// <typeparam name="TKey">Type of the key for this entity.</typeparam>
    public abstract class EntityDto<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Entity's primary key.
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Base class for all DTO entities with a key type of long.
    /// </summary>
    public abstract class EntityDTo : EntityDto<long>
    {
    }
}
