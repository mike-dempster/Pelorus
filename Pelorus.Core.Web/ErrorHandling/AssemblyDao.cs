using System;

namespace Pelorus.Core.Web.ErrorHandling
{
    internal class AssemblyDao
    {
        public int Id { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyFullName { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionBuild { get; set; }
        public int VersionRevision { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
