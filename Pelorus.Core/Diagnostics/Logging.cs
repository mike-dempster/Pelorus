using System;
using System.Diagnostics;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Common trace methods for emitting diagnostic information.
    /// </summary>
    public sealed class Logging
    {
        private const string LoggingTraceSourceName = "LoggingTraceSource";
        private const string ExceptionLoggingTraceSourceName = "ExceptionLoggingTraceSource";

        /// <summary>
        /// Emit a formatted exception message to the trace listeners.
        /// </summary>
        /// <param name="ex">Exception to format and emit.</param>
        public static void LogException(Exception ex)
        {
            var traceSource = new TraceSource(ExceptionLoggingTraceSourceName);
            traceSource.TraceData(TraceEventType.Error, 1, ex);
        }

        /// <summary>
        /// Emit a message to the logging trace source.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogInformation(string message)
        {
            var traceSource = new TraceSource(LoggingTraceSourceName);
            traceSource.TraceInformation(message);
        }

        /// <summary>
        /// Emit a formatted message to the logging trace source.
        /// </summary>
        /// <param name="format">Format of the message to emit.</param>
        /// <param name="args">Message args referenced by the message format string.</param>
        public static void LogInformation(string format, params object[] args)
        {
            var traceSource = new TraceSource(LoggingTraceSourceName);
            traceSource.TraceInformation(format, args);
        }

        /// <summary>
        /// Emit an event to the logging trace source.
        /// </summary>
        /// <param name="eventType">Type of the event that is being logged.</param>
        /// <param name="id">Id of the event that is being logged.</param>
        public static void LogEvent(TraceEventType eventType, int id)
        {
            var traceSource = new TraceSource(LoggingTraceSourceName);
            traceSource.TraceEvent(eventType, id);
        }

        /// <summary>
        /// Emit an event with a message to the logging trace source.
        /// </summary>
        /// <param name="eventType">Type of the event that is being logged.</param>
        /// <param name="id">Id of the event that is being logged.</param>
        /// <param name="message">Message to be logged with the event.</param>
        public static void LogEvent(TraceEventType eventType, int id, string message)
        {
            var traceSource = new TraceSource(LoggingTraceSourceName);
            traceSource.TraceEvent(eventType, id, message);
        }

        /// <summary>
        /// Emit an event with a formatted message to the logging trace source.
        /// </summary>
        /// <param name="eventType">Type of the event that is being logged.</param>
        /// <param name="id">Id of the event that is being logged.</param>
        /// <param name="format">Format of the message to be logged with the event.</param>
        /// <param name="args">Message args referenced by the message format string.</param>
        public static void LogEvent(TraceEventType eventType, int id, string format, params object[] args)
        {
            var traceSource = new TraceSource(LoggingTraceSourceName);
            traceSource.TraceEvent(eventType, id, format, args);
        }
    }
}
