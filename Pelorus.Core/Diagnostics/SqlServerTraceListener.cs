using System;
using System.Diagnostics;
using System.Text;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Trace listener that logs message to SQL Server.
    /// </summary>
    public sealed class SqlServerTraceListener : TraceListener
    {
        private readonly StringBuilder _messageBuffer;

        /// <summary>
        /// Indicates if the trace listener is thread safe.  This trace listener implementation is not thread safe.
        /// </summary>
        public override bool IsThreadSafe { get { return false; } }

        /// <summary>
        /// Create a new instance of the listener and initialize the internal state.
        /// </summary>
        public SqlServerTraceListener()
        {
            this._messageBuffer = new StringBuilder();
        }

        /// <summary>
        /// Flushes the output buffer writing the buffer contents to SQL Server.
        /// </summary>
        public override void Flush()
        {
            base.Flush();
        }

        /// <summary>
        /// Writes trace information, a data object, and event information to SQL Server.  If the object is an exception then 
        /// an exception record is created with the exception data.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output.  This is typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            var exception = data as Exception;
        }

        /// <summary>
        /// Writes trace information, an array of data objects, and event information to SQL Server.  An exception record is 
        /// created for any exception objects in the array.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output.  This is typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">An array of objects to emit as data.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
        }

        /// <summary>
        /// Writes trace and event information to SQL Server.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
        }

        /// <summary>
        /// Writes trace information, a message, and event information to SQL Server.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
        }

        /// <summary>
        /// Writes trace information, a formatted array of objects, and event information to SQL Server.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="format">
        /// A format string that contains zero or more format items, which correspond to objects in the args array.
        /// </param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
        }

        /// <summary>
        /// Writes trace information, a message, a related activity identity and event information to SQL Server.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="eventType">
        /// One of the TraceEventType values specifying the type of event that has caused the trace.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">
        /// A message to write.
        /// </param>
        /// <param name="relatedActivityId">A Guid object identifying a related activity.</param>
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
        }

        /// <summary>
        /// Writes the specified message to SQL Server.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            this._messageBuffer.Append(message);
        }

        /// <summary>
        /// Writes a message to the listener followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            this._messageBuffer.AppendLine(message);
        }
    }
}
