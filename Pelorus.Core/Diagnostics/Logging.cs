using Pelorus.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Common trace methods for emitting diagnostic information.
    /// </summary>
    public sealed class Logging
    {
        /// <summary>
        /// Emit a formatted exception message to the trace listeners.
        /// </summary>
        /// <param name="ex">Exception to format and emit.</param>
        public static void LogException(Exception ex)
        {
            Trace(e => e.TraceData(TraceEventType.Error, 1, ex));
        }

        /// <summary>
        /// Emit a message to the logging trace source.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogInformation(string message)
        {
            Trace(e => e.TraceInformation(message));
        }

        /// <summary>
        /// Emit a formatted message to the logging trace source.
        /// </summary>
        /// <param name="format">Format of the message to emit.</param>
        /// <param name="args">Message args referenced by the message format string.</param>
        public static void LogInformation(string format, params object[] args)
        {
            Trace(e => e.TraceInformation(format, args));
        }

        /// <summary>
        /// Emit an event to the logging trace source.
        /// </summary>
        /// <param name="eventType">Type of the event that is being logged.</param>
        /// <param name="id">Id of the event that is being logged.</param>
        public static void LogEvent(TraceEventType eventType, int id)
        {
            Trace(e => e.TraceEvent(eventType, id));
        }

        /// <summary>
        /// Emit an event with a message to the logging trace source.
        /// </summary>
        /// <param name="eventType">Type of the event that is being logged.</param>
        /// <param name="id">Id of the event that is being logged.</param>
        /// <param name="message">Message to be logged with the event.</param>
        public static void LogEvent(TraceEventType eventType, int id, string message)
        {
            Trace(e => e.TraceEvent(eventType, id, message));
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
            Trace(e => e.TraceEvent(eventType, id, format, args));
        }

        /// <summary>
        /// Get a collection of trace sources from the configuration data.
        /// </summary>
        /// <returns>Collection of trace sources or null if the required data is missing fomr the configuration.</returns>
        private static IEnumerable<TraceSource> GetTraceSources()
        {
            try
            {
                var traceSourceNames = CoreConfiguration.Diagnostics.TraceSources.OfType<AddTraceSourceConfigurationElement>()
                                                        .Select(e => e.Name);
                var traceSources = traceSourceNames.Select(e => new TraceSource(e))
                                                   .ToList();

                return traceSources;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Execute the trace operation on all of the trace sources.
        /// </summary>
        /// <param name="traceExpression">Trace operation to perform on the trace sources.</param>
        private static void Trace(Action<TraceSource> traceExpression)
        {
            var allTraceSources = GetTraceSources();

            if (null == allTraceSources)
            {
                return;
            }

            foreach (var traceSource in allTraceSources)
            {
                try
                {
                    traceExpression(traceSource);
                }
                catch
                {
                    // Do not allow exceptions originating from the trace listeners to bubble up to the application.
                }
            }
        }
    }
}
