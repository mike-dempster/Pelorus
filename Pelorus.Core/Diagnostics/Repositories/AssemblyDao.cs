using System;
using System.Runtime.Serialization;

namespace Pelorus.Core.Diagnostics.Repositories
{
    /// <summary>
    /// Data access entity for assembly records.
    /// </summary>
    [DataContract(Namespace = "http://core.pelorus.com/2015/01/Pelorus.Core.Diagnostics")]
    internal class AssemblyDao
    {
        /// <summary>
        /// Primary key of the record.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Short/friendly name of the assembly.
        /// </summary>
        [DataMember]
        public string AssemblyName { get; set; }

        /// <summary>
        /// Full name/strong name of the assembly.
        /// </summary>
        [DataMember]
        public string AssemblyFullName { get; set; }

        /// <summary>
        /// Major version of the assembly.
        /// </summary>
        [DataMember]
        public int VersionMajor { get; set; }

        /// <summary>
        /// Minor version of the assembly.
        /// </summary>
        [DataMember]
        public int VersionMinor { get; set; }

        /// <summary>
        /// Build number of the assembly.
        /// </summary>
        [DataMember]
        public int VersionBuild { get; set; }

        /// <summary>
        /// Revision number of the assembly.
        /// </summary>
        [DataMember]
        public int VersionRevision { get; set; }

        /// <summary>
        /// Date and time that the record was created.
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Identity of the session account that was used to insert the record.
        /// </summary>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date and time that the record was last updated.
        /// </summary>
        [DataMember]
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Identity of the session account that was used for the last update to the record.
        /// </summary>
        [DataMember]
        public string LastUpdatedBy { get; set; }
    }
}
