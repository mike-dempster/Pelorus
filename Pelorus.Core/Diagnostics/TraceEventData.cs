using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Pelorus.Core.Diagnostics
{
    public class TraceEventData
    {
        public Nullable<Guid> CorrelationId { get; set; }
        public Nullable<short> CorrelationIndex { get; set; }
        public string Source { get; set; }
        public TraceEventType EventType { get; set; }
        public int EventId { get; set; }
        public string Message { get; set; }
        public XmlDocument Data { get; set; }
        public string Callstack { get; set; }
        public DateTime EventDateTime { get; set; }
        public Stack LogicalOperationStack { get; set; }
        public int ProcessId { get; set; }
        public string ThreadId { get; set; }
        public long TimeStamp { get; set; }
        public IDictionary<string, object> Context { get; set; }
    }
}
