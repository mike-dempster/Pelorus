using System;

namespace Pelorus.Core.Entities
{
    /// <summary>
    /// Standard properties for change audited entities.
    /// </summary>
    public interface IAuditedEntity
    {
        /// <summary>
        /// Identifier of the user ro service account that created the record.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Date and time that the record was created.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Identifier of the user or service account that last updated the record.
        /// </summary>
        string LastUpdatedBy { get; set; }

        /// <summary>
        /// Date and time that the record was last updated.
        /// </summary>
        DateTime LastUpdatedOn { get; set; }
    }
}
