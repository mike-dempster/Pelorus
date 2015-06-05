using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Base class for trace listeners formats the message data into a strongly typed object and invokes an abstract method 
    /// with the message data for writing to the target media.
    /// </summary>
    public abstract class BaseTraceListener : TraceListener
    {
        /// <summary>
        /// When implemented in a derived class, writes the trace data to the target media.
        /// </summary>
        /// <param name="traceData">Data associated with the trace event.</param>
        protected abstract void LogMessage(TraceEventData traceData);

        /// <summary>
        /// Serializes an object that is received by on of the TraceData methods. This method can be overridden in a derived 
        /// class to allow customization of the serializing process.
        /// </summary>
        /// <param name="data">Data to serialize.</param>
        /// <param name="traceData">
        /// Trace data instance for the current trace event. The method can set any of the trace data properties based on the 
        /// state of the data object.
        /// </param>
        protected virtual void SerializeData(object data, TraceEventData traceData)
        {
            var exception = data as Exception;

            if (null != exception)
            {
                this.SerializeException((Exception) data, traceData);
                return;
            }

            var xmlDocument = new XmlDocument();
            var xmlNavigator = xmlDocument.CreateNavigator();

            using (var writer = xmlNavigator.AppendChild())
            {
                var serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(writer, data);
            }

            traceData.Data = xmlDocument;
        }

        /// <summary>
        /// Writes trace information, a data object and event information to the listener specific output.
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
        /// <param name="data">The trace data to emit.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            var exception = data as Exception;

            if (null == exception)
            {
                var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, null, null);
                this.SerializeData(data, traceData);
                this.LogMessage(traceData);
                return;
            }

            Nullable<Guid> correlationId = null;
            Nullable<short> correlationIndex = null;

            if (null != exception.InnerException)
            {
                correlationId = Guid.NewGuid();
                correlationIndex = 0;
            }

            while (null != exception)
            {
                var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, correlationId, correlationIndex);
                this.SerializeData(exception, traceData);
                this.LogMessage(traceData);
                exception = exception.InnerException;

                if (null != correlationIndex)
                {
                    correlationIndex++;
                }
            }
        }

        /// <summary>
        /// Writes trace information, an array of data objects and event information to the listener specific output.
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
        /// <param name="data">An array of objects to emit as data.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if ((null == data) || (0 == data.Length))
            {
                var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, null, null);
                this.LogMessage(traceData);
                return;
            }

            Nullable<Guid> correlationId = null;
            Nullable<short> correlationIndex = null;
            var exception = data.FirstOrDefault() as Exception;

            // If there is more than 1 element in the collection or if there is only one but it is an exception adn it has
            // at least on inner exception then initialize the correlation properties.
            if ((1 > data.Length) || ((null != exception) && (null != exception.InnerException)))
            {
                correlationId = Guid.NewGuid();
                correlationIndex = 0;
            }

            foreach (var element in data)
            {
                exception = element as Exception;
                var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, correlationId, correlationIndex);

                if (null == exception)
                {
                    this.SerializeData(element, traceData);
                    this.LogMessage(traceData);
                    continue;
                }

                while (null != exception)
                {
                    this.SerializeData(exception, traceData);
                    this.LogMessage(traceData);
                    exception = exception.InnerException;

                    if (null != correlationIndex)
                    {
                        correlationIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// Writes trace and event information to the listener specific output.
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
            var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, null, null);
            this.LogMessage(traceData);
        }

        /// <summary>
        /// Writes trace information, a message, and event information to the listener specific output.
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
            var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, null, null);
            traceData.Message = message;
            this.LogMessage(traceData);
        }

        /// <summary>
        /// Writes trace information, a formatted array of objects and event information to the listener specific output.
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
            string message = string.Format(CultureInfo.InvariantCulture, format, args);
            var traceData = this.CreateTraceEventData(eventCache, source, eventType, id, null, null);
            traceData.Message = message;
            this.LogMessage(traceData);
        }

        /// <summary>
        /// Writes trace information, a message, a related activity identity and event information to the listener specific 
        /// output.
        /// </summary>
        /// <param name="eventCache">
        /// A TraceEventCache object that contains the current process Id, thread Id, and stack trace information.
        /// </param>
        /// <param name="source">
        /// A name used to identify the output, typically the name of the application that generated the trace event.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        /// <param name="relatedActivityId">A System.Guid object identifying a related activity.</param>
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            var traceData = this.CreateTraceEventData(eventCache, source, TraceEventType.Information, id, null, null);
            this.LogMessage(traceData);
        }

        /// <summary>
        /// Serializes an exception object.
        /// </summary>
        /// <param name="data">Exception instance to serialize.</param>
        /// <param name="traceData">Trace event data for the current trace event.</param>
        private void SerializeException(Exception data, TraceEventData traceData)
        {
            // Create the XML document and add the root element.
            var exceptionData = new XmlDocument();
            var rootElement = exceptionData.CreateElement("root");
            exceptionData.AppendChild(rootElement);

            // Add an attribute to the root element for the exception's source.
            var attribute = exceptionData.CreateAttribute("source");
            attribute.Value = data.Source;
            rootElement.Attributes.Append(attribute);

            // Add an attribute to the root element for the exception's help link.
            attribute = exceptionData.CreateAttribute("helpLink");
            attribute.Value = data.HelpLink;
            rootElement.Attributes.Append(attribute);

            // Add an attribute to the root element for the exception's type.
            attribute = exceptionData.CreateAttribute("type");
            var exceptionType = data.GetType();
            attribute.Value = exceptionType.FullName;
            rootElement.Attributes.Append(attribute);

            // Add child elements to the root for each item in the exception's data dictionary.
            foreach (DictionaryEntry entry in data.Data)
            {
                string key = entry.Key.ToString();
                string value = (null == entry.Value) ? "null" : entry.Value.ToString();
                var element = exceptionData.CreateElement(key);
                element.InnerText = value;
                rootElement.AppendChild(element);
            }

            traceData.Message = data.Message;
            traceData.Callstack = data.StackTrace;
            traceData.Data = exceptionData;
            traceData.Context.Add("ExceptionHelpLink", data.HelpLink);
            traceData.Context.Add("ExceptionSource", data.Source);
        }

        /// <summary>
        /// Creates a new instance of a trace event data object.
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
        /// <param name="correlationId">Correlation Id if the event is correlated to other events.</param>
        /// <param name="correlationIndex">
        /// Correlation index of this trace event's position within the correlation if this trace event is correlated to any 
        /// other events.
        /// </param>
        /// <returns></returns>
        private TraceEventData CreateTraceEventData(
            TraceEventCache eventCache,
            string source,
            TraceEventType eventType,
            int id,
            Nullable<Guid> correlationId,
            Nullable<short> correlationIndex)
        {
            int processId = eventCache.ProcessId;

            // If the process Id was not supplied then default to the Id of the current process.
            if (0 == processId)
            {
                processId = Process.GetCurrentProcess().Id;
            }

            string threadId = eventCache.ThreadId;

            // If the thread Id was not supplied then default to the Id of the current thread.
            if (string.IsNullOrWhiteSpace(threadId))
            {
                threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            }

            return new TraceEventData
            {
                Callstack = eventCache.Callstack,
                Context = new Dictionary<string, object>(),
                CorrelationId = correlationId,
                CorrelationIndex = correlationIndex,
                Data = null,
                EventDateTime = eventCache.DateTime,
                EventId = id,
                EventType = eventType,
                LogicalOperationStack = eventCache.LogicalOperationStack,
                Message = null,
                ProcessId = processId,
                Source = source,
                ThreadId = threadId,
                TimeStamp = eventCache.Timestamp
            };
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
        }
    }
}
