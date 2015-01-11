using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Base class for trace listeners formats the message data into a strongly typed object and invokes an abstract method 
    /// with the message data for writing to the target media.
    /// </summary>
    public abstract class BaseTraceListener : TraceListener
    {
        /// <summary>
        /// Trace message data that is being built up.
        /// </summary>
        private TraceMessageData traceMessageData = null;

        /// <summary>
        /// Bookmarks the next section that is expected to be received by the Write methods.
        /// </summary>
        private TraceMessageSections messageSection = TraceMessageSections.Header;

        /// <summary>
        /// When implemented in a derived class, writes the trace data to the target media.
        /// </summary>
        /// <param name="traceData"></param>
        public abstract void LogMessage(TraceMessageData traceData);

        /// <summary>
        /// Used to flush the buffer and close any open resources.
        /// </summary>
        public override void Close()
        {
            this.Flush();
        }

        /// <summary>
        /// Closes the trace listener causing the buffer to be flushed.
        /// </summary>
        /// <param name="disposing">Indicates if this method is being called becasue the instance is being disposed.</param>
        protected override void Dispose(bool disposing)
        {
            this.Close();
        }

        /// <summary>
        /// Invoke the LogTrace method and reset the internal state to prepare for subsequent trace events.
        /// </summary>
        public override void Flush()
        {
            this.LogMessage(this.traceMessageData);
            this.traceMessageData = null;
            this.messageSection = TraceMessageSections.Header;
        }

        /// <summary>
        /// Writes a message to the trace message data.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            switch (this.messageSection)
            {
                case TraceMessageSections.Header:
                    this.traceMessageData = this.CreateNewTraceMessageData(message);
                    this.messageSection = TraceMessageSections.Body;
                    break;

                case TraceMessageSections.Body:
                    this.traceMessageData.Message = message;
                    this.messageSection = TraceMessageSections.Footers;
                    break;

                case TraceMessageSections.Footers:
                    var splitMessage = message.Split('=');
                    var footerName = splitMessage.FirstOrDefault();
                    var footerData = message.ConcatAndTrimStart(footerName, "=");
                    this.traceMessageData.Footers.Add(footerName, footerData);
                    break;

                default:
                    this.messageSection = TraceMessageSections.Header;
                    this.traceMessageData = null;
                    throw new Exception("The trace event did not complete successfully.");
            }
        }

        /// <summary>
        /// Writes a message to the trace message data followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            string formattedMessage = string.Format(CultureInfo.InvariantCulture, "{0}\n", message);
            this.Write(formattedMessage);
        }

        /// <summary>
        /// Creates a new trace message data instance, parses the header message, and initializes the trace message properties.
        /// </summary>
        /// <param name="headerMessage">Header message to parse for the trace message header data.</param>
        /// <returns>New trace message data instance.</returns>
        private TraceMessageData CreateNewTraceMessageData(string headerMessage)
        {
            string traceSourceName = this.MatchOrNull(headerMessage, @"(?<TraceSourceName>.+(?=\s.*\:\s{0,}[0-9]{1,}\s{0,}\:\s{0,}$))");
            string eventTypeName = this.MatchOrNull(headerMessage, @"(?<TraceEventType>[^\s.]+?(?=\:\s{0,}[0-9]{1,}\s{0,}\:\s{0,}$))");
            string idString = this.MatchOrNull(headerMessage, @"(?<id>[^\s]{0,}[0-9][^\s]{0,}(?!.$))");
            var traceMessage = new TraceMessageData
            {
                TraceSourceName = traceSourceName,
                EventType = Enums.Parse<TraceEventType>(eventTypeName),
                Id = int.Parse(idString),
                Message = string.Empty,
                Footers = new Dictionary<string, string>()
            };

            return traceMessage;
        }

        /// <summary>
        /// Attempts to match a regular expression pattern in the given string and if found returns the match otherwise 
        /// returns null.
        /// </summary>
        /// <param name="input">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>Match found in the input string or null if no match was found.</returns>
        private string MatchOrNull(string input, string pattern)
        {
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }
    }
}
