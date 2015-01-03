namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all data transfer objects.
    /// </summary>
    public abstract class EntityDto<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Entity's primary key.
        /// </summary>
        public TKey Id { get; set; }
    }
}
