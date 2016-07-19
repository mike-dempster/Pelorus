using System;

namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Base class for all entities with standard audit columns.
    /// </summary>
    /// <typeparam name="TKey">Type of the key for this entity.</typeparam>
    public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditedEntity
        where TKey : struct
    {
        /// <summary>
        /// Identifier of the user ro service account that created the record.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date and time that the record was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Identifier of the user or service account that last updated the record.
        /// </summary>
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Date and time that the record was last updated.
        /// </summary>
        public DateTime LastUpdatedOn { get; set; }
    }

    /// <summary>
    /// Base class for all entities with standard audit columns.
    /// </summary>
    public abstract class AuditedEntity : AuditedEntity<long>
    {
    }
}
