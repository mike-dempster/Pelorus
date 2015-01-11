using System.Collections.Generic;
using System.Diagnostics;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Represents the data that is written to the trace listener during a trave event.
    /// </summary>
    public class TraceMessageData
    {
        /// <summary>
        /// Name of the trace source.
        /// </summary>
        public string TraceSourceName { get; set; }

        /// <summary>
        /// Event type of the trace event.
        /// </summary>
        public TraceEventType EventType { get; set; }

        /// <summary>
        /// A numeric identifier for the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Body of the trace event.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Footers that followed the main body of the trace event.
        /// </summary>
        public IDictionary<string, string> Footers { get; set; }
    }
}
