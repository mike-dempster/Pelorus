namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all DAO entities.
    /// </summary>
    public abstract class EntityDao<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Entity's primary key.
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// Base class for all DAO entities with a key type of int.
    /// </summary>
    public abstract class EntityDao : EntityDao<int>
    {
    }
}
