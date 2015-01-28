using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;

namespace Pelorus.Core.Diagnostics.Repositories
{
    /// <summary>
    /// Data access entity for application log records.
    /// </summary>
    [DataContract(Namespace = "http://core.pelorus.com/2015/01/Pelorus.Core.Diagnostics")]
    [Serializable]
    internal class ApplicationLogDao
    {
        /// <summary>
        /// Primary key of the application log.
        /// </summary>
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// Application log message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Optional help link for the application log.
        /// </summary>
        [DataMember]
        public string HelpLink { get; set; }

        /// <summary>
        /// Name of the trace source that inserted the application log.
        /// </summary>
        [DataMember]
        public string Source { get; set; }

        /// <summary>
        /// Stack trace of the trace event.
        /// </summary>
        [DataMember]
        public string StackTrace { get; set; }

        /// <summary>
        /// Serialized data associated with the application log.
        /// </summary>
        public XmlDocument Data { get; set; }

        /// <summary>
        /// Id of the trace Id dimension that defines the application log.
        /// </summary>
        [DataMember]
        public int TraceId { get; set; }

        /// <summary>
        /// Correlation Id if the application log is correlatated with any other logs.
        /// </summary>
        [DataMember]
        public Nullable<Guid> CorrelationId { get; set; }

        /// <summary>
        /// Index of this application log in the correlated group.
        /// </summary>
        [DataMember]
        public Nullable<short> CorrelationIndex { get; set; }

        /// <summary>
        /// Id of the assembly that was associated with the trace event.
        /// </summary>
        [DataMember]
        public int AssemblyId { get; set; }

        /// <summary>
        /// Name of the machine that was hosting the process that inserted the application log.
        /// </summary>
        [DataMember]
        public string MachineName { get; set; }

        /// <summary>
        /// Name of the app domain of the process that inserted the application log.
        /// </summary>
        [DataMember]
        public string AppDomainName { get; set; }

        /// <summary>
        /// Id of the process that inserted the application log.
        /// </summary>
        [DataMember]
        public int ProcessId { get; set; }

        /// <summary>
        /// Id of the thread that inserted the application log.
        /// </summary>
        [DataMember]
        public string ThreadId { get; set; }

        /// <summary>
        /// Type of the trace event.
        /// </summary>
        [DataMember]
        public TraceEventType TraceEventType { get; set; }

        /// <summary>
        /// Name of the trace listener that inserted the application log.
        /// </summary>
        [DataMember]
        public string TraceListenerName { get; set; }

        /// <summary>
        /// Date and time that the application log was inserted.
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Session account that inserted the application log.
        /// </summary>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date and time that the application log was last updated.
        /// </summary>
        [DataMember]
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Session account that was used to update the application log last.
        /// </summary>
        [DataMember]
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Assembly that was associated with the trace event.
        /// </summary>
        [DataMember]
        public AssemblyDao Assembly { get; set; }
    }
}
