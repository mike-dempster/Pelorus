using System;

namespace Pelorus.Core.Web.ErrorHandling
{
    internal class ApplicationLogDao
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string HelpLink { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string Data { get; set; }
        public int TraceId { get; set; }
        public Nullable<Guid> CorrelationId { get; set; }
        public Nullable<short> CorrelationIndex { get; set; }
        public int AssemblyId { get; set; }
        public string MachineName { get; set; }
        public string AppDomainName { get; set; }
        public int ProcessId { get; set; }
        public string ThreadId { get; set; }
        public int TraceEventType { get; set; }
        public string TraceListenerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
