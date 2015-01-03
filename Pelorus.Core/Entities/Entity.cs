namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all business objects.
    /// </summary>
    public abstract class Entity<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Entity's primary key.
        /// </summary>
        public TKey Id { get; set; }
    }
}
