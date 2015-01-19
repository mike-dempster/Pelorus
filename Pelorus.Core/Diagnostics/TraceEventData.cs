using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Data associated with a trace event.
    /// </summary>
    public class TraceEventData
    {
        /// <summary>
        /// Correlation Id of the trace event if the event is correlated to other trace events..
        /// </summary>
        public Nullable<Guid> CorrelationId { get; set; }

        /// <summary>
        /// Correlation index of the trace event if the event is correlated to other trace events.
        /// </summary>
        public Nullable<short> CorrelationIndex { get; set; }

        /// <summary>
        /// Trace source that is handling the trace event.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Type of the trace event.
        /// </summary>
        public TraceEventType EventType { get; set; }

        /// <summary>
        /// Trace event identification.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Trace message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Serialized data associated with the trace event.
        /// </summary>
        public XmlDocument Data { get; set; }

        /// <summary>
        /// Call stack indicating where the trace was initiated from.
        /// </summary>
        public string Callstack { get; set; }

        /// <summary>
        /// Date and time that the trace event was generated.
        /// </summary>
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// Logical operation stack.
        /// </summary>
        public Stack LogicalOperationStack { get; set; }

        /// <summary>
        /// Id of the process where the trace event was initiated from.
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// Thread Id where the trace event was initiated from.
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// Time stamp of the time that the trace event was generated.
        /// </summary>
        public long TimeStamp { get; set; }

        /// <summary>
        /// Additional contextual data related to the trace event.
        /// </summary>
        public IDictionary<string, object> Context { get; set; }
    }
}
