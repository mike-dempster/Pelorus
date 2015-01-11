using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Xml;

namespace Pelorus.Core.Diagnostics
{
    /// <summary>
    /// Trace listener that logs message to SQL Server.
    /// </summary>
    public sealed class SqlServerTraceListener : BaseTraceListener
    {
        private bool logged = false;
        private readonly IDictionary<string, object> _traceSessionData;

        /// <summary>
        /// Indicates if the trace listener is thread safe.  This trace listener implementation is not thread safe.
        /// </summary>
        public override bool IsThreadSafe { get { return false; } }

        /// <summary>
        /// Create a new instance of the listener and initialize the internal state.
        /// </summary>
        public SqlServerTraceListener()
        {
            this._traceSessionData = new Dictionary<string, object>();
            this.TraceOutputOptions = TraceOptions.Callstack |
                                      TraceOptions.DateTime |
                                      TraceOptions.LogicalOperationStack |
                                      TraceOptions.ProcessId |
                                      TraceOptions.ThreadId;
        }

        /// <summary>
        /// Returns a collection of custom attributes that the trace listener supports.
        /// </summary>
        /// <returns>Collection of supported configuration attributes.</returns>
        protected override string[] GetSupportedAttributes()
        {
            return new[]
            {
                "connectionStringName"
            };
        }

        /// <summary>
        /// Write the trace message to the database.
        /// </summary>
        /// <param name="traceData">Trace message data to write to the database.</param>
        public override void LogMessage(TraceMessageData traceData)
        {
            // If the message has been logged by an overridden method then do not attempt to log it again.
            if (true == this.logged)
            {
                this.logged = false;
                return;
            }

            string helpLink = this.GetFooterOrNull(traceData, "HelpUrl");
            string stackTrace = this.GetFooterOrNull(traceData, "Callstack");
            var traceEventData = this.GetFooterXmlOrNull(traceData, "TraceData");
            string processIdString = this.GetFooterOrNull(traceData, "ProcessId");
            int processId = int.Parse(processIdString ?? "0");
            string threadId = this.GetFooterOrNull(traceData, "ThreadId");
            var correlationId = this.GetTraceSourceDataOrNull("correlationId") as Guid?;
            var correlationIndex = this.GetTraceSourceDataOrNull("correlationIndex") as short?;

            // If the process Id was not supplied then default to the Id of the current process.
            if (0 == processId)
            {
                processId = Process.GetCurrentProcess().Id;
            }

            // If the thread Id was not supplied then default to the Id of the current thread.
            if (string.IsNullOrWhiteSpace(threadId))
            {
                threadId = Thread.CurrentThread.ManagedThreadId.ToString();
            }

            this.InsertMessage(
                message: traceData.Message,
                helpLink: helpLink,
                source: traceData.TraceSourceName,
                stackTrace: stackTrace,
                traceData: traceEventData,
                traceEventType: traceData.EventType,
                traceId: traceData.Id,
                correlationId: correlationId,
                correlationIndex: correlationIndex,
                processId: processId,
                threadId: threadId);
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

            if (null == exception)
            {
                base.TraceData(eventCache, source, eventType, id, data);
                return;
            }

            var correlationId = this.GetTraceSourceDataOrNull("correlationId") as Guid?;
            var correlationIndex = this.GetTraceSourceDataOrNull("correlationIndex") as short?;

            if (null != exception.InnerException)
            {
                if (null == correlationId)
                {
                    correlationId = Guid.NewGuid();
                    this.SetTraceSourceData("correlationId", correlationId);
                }

                if (null == correlationIndex)
                {
                    correlationIndex = -1;
                }
            }

            while (null != exception)
            {
                if (null != correlationIndex)
                {
                    correlationIndex++;
                    this.SetTraceSourceData("correlationIndex", correlationIndex);
                }

                var exceptionData = this.SerializeExceptionData(exception);
                this.InsertMessage(
                    message: exception.Message,
                    helpLink: exception.HelpLink,
                    source: source,
                    stackTrace: exception.StackTrace,
                    traceData: exceptionData,
                    traceEventType: eventType,
                    traceId: id,
                    correlationId: correlationId,
                    correlationIndex: correlationIndex,
                    processId: eventCache.ProcessId,
                    threadId: eventCache.ThreadId);

                exception = exception.InnerException;
            }

            this.logged = true;
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
            var correlationId = this.GetTraceSourceDataOrNull("correlationId") as Guid?;
            var correlationIndex = this.GetTraceSourceDataOrNull("correlationIndex") as short?;

            if (null == correlationId)
            {
                this.SetTraceSourceData("correlationId", Guid.NewGuid());
            }

            if (null == correlationIndex)
            {
                correlationIndex = -1;
            }

            foreach (var element in data)
            {
                correlationIndex++;
                this.SetTraceSourceData("correlationIndex", correlationIndex);
                this.TraceData(eventCache, source, eventType, id, element);
            }
        }

        /// <summary>
        /// Get a value in the trace source data dictionary if it exists and if it does not exist return null.
        /// </summary>
        /// <param name="key">Key of the data to get from the dictionary.</param>
        /// <returns>Trace source data or null if the data does not exist in the dictionary.</returns>
        private object GetTraceSourceDataOrNull(string key)
        {
            bool containsKey = this._traceSessionData.ContainsKey(key);

            if (false == containsKey)
            {
                return null;
            }

            var value = this._traceSessionData[key];

            return value;
        }

        /// <summary>
        /// Save the given value in the trace source dictionary.  If the value alreay exists it will be updated otherwise 
        /// the key will be added and set to the given value.
        /// </summary>
        /// <param name="key">Key of the data to set.</param>
        /// <param name="value">Data to be saved to the dictionary.</param>
        private void SetTraceSourceData(string key, object value)
        {
            bool containsKey = this._traceSessionData.ContainsKey(key);

            if (false == containsKey)
            {
                this._traceSessionData.Add(key, value);
                return;
            }

            this._traceSessionData[key] = value;
        }

        /// <summary>
        /// Get a footer from the trace message data.
        /// </summary>
        /// <param name="traceMessageData">Trace message data to get the footer from.</param>
        /// <param name="footerName">Name of the footer to get.</param>
        /// <returns>Footer value or null if the footer is not found.</returns>
        public string GetFooterOrNull(TraceMessageData traceMessageData, string footerName)
        {
            string value;
            bool result = traceMessageData.Footers.TryGetValue(footerName, out value);

            if (result)
            {
                return value;
            }

            return null;
        }

        /// <summary>
        /// Get a footer from the trace message data.
        /// </summary>
        /// <param name="traceMessageData">Trace message data to get the footer from.</param>
        /// <param name="footerName">Name of the footer to get.</param>
        /// <returns>Footer value or null if the footer is not found.</returns>
        public XmlDocument GetFooterXmlOrNull(TraceMessageData traceMessageData, string footerName)
        {
            string value;
            bool result = traceMessageData.Footers.TryGetValue(footerName, out value);

            if (result)
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(value);

                return xmlDocument;
            }

            return null;
        }

        /// <summary>
        /// Serializes the exception data into XML format for storage.
        /// </summary>
        /// <param name="ex">Exception whose data is to be serialized.</param>
        /// <returns>XML document of the exception's data or null if the exception did not have any data.</returns>
        private XmlDocument SerializeExceptionData(Exception ex)
        {
            if (0 == ex.Data.Count)
            {
                return null;
            }

            var dataRoot = new XmlDocument();
            var exceptionType = ex.GetType();
            string rootElementName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", exceptionType.Namespace, exceptionType.Name);
            var rootCollection = dataRoot.CreateElement(rootElementName);
            dataRoot.AppendChild(rootCollection);

            foreach (DictionaryEntry entry in ex.Data)
            {
                string key = entry.Key.ToString();
                string value = (null == entry.Value) ? "null" : entry.Value.ToString();
                var element = dataRoot.CreateElement(key);
                element.InnerText = value;
                rootCollection.AppendChild(element);
            }

            return dataRoot;
        }

        /// <summary>
        /// Get the connection string to the SQL Server database.
        /// </summary>
        /// <returns>Connection string name to the database to use for tracing.</returns>
        private string GetConnectionString()
        {
            string connectionStringName = "Pelorus.Core.Tracing";
            bool connectionStringConfigured = this.Attributes.ContainsKey("connectionStringName");

            if (connectionStringConfigured)
            {
                connectionStringName = this.Attributes["connectionStringName"];
            }

            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (null == connectionString)
            {
                return null;
            }

            return connectionString.ConnectionString;
        }

        /// <summary>
        /// Executes the stored procedure to insert a trace message into SQL.
        /// </summary>
        /// <param name="message">Trace message to insert into the database.</param>
        /// <param name="helpLink">Help link to insert into the database.</param>
        /// <param name="source">Source of the trace message to insert into the database.</param>
        /// <param name="stackTrace">Stack trace of the trace message to insert into the database.</param>
        /// <param name="traceData">Data associated with the trace event.</param>
        /// <param name="traceEventType">Type of the trace event.</param>
        /// <param name="traceId">Id of the trace message to insert into the database.</param>
        /// <param name="correlationId">Optional correlation Id of the trace message to insert into the database.</param>
        /// <param name="correlationIndex">Optional correlation index of the trace message to insert into the database.</param>
        /// <param name="processId">Process Id to insert into the database.</param>
        /// <param name="threadId">Thread Id to insert into the database.</param>
        /// <returns>Primary key Id of the record that was inserted into that database.</returns>
        private long InsertMessage(
            string message,
            string helpLink,
            string source,
            string stackTrace,
            XmlDocument traceData,
            TraceEventType traceEventType,
            int traceId,
            Guid? correlationId,
            short? correlationIndex,
            int processId,
            string threadId)
        {
            string connectionString = this.GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Pelorus.Core].[sp_InsertMessage]";
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyName = assembly.GetName();
                string traceDataXmlString = null;

                if (null != traceData)
                {
                    traceDataXmlString = traceData.InnerXml;
                }

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inMessage",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(message)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inHelpLink",
                    DbType = DbType.String,
                    Size = 2083,
                    Value = this.ValueOrDbNull(helpLink)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inSource",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(source)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inStackTrace",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(stackTrace)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceData",
                    DbType = DbType.Xml,
                    Value = this.ValueOrDbNull(traceDataXmlString)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceId",
                    DbType = DbType.Int32,
                    Value = this.ValueOrDbNull(traceId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inCorrelationId",
                    DbType = DbType.Guid,
                    Value = this.ValueOrDbNull(correlationId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inCorrelationIndex",
                    DbType = DbType.Int32,
                    Value = this.ValueOrDbNull(correlationIndex)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyName",
                    DbType = DbType.String,
                    Value = assemblyName.Name
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyFullName",
                    DbType = DbType.String,
                    Value = assemblyName.FullName
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyVersionMajor",
                    DbType = DbType.Int32,
                    Value = assemblyName.Version.Major
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyVersionMinor",
                    DbType = DbType.Int32,
                    Value = assemblyName.Version.Minor
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyVersionBuild",
                    DbType = DbType.Int32,
                    Value = assemblyName.Version.Build
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAssemblyVersionRevision",
                    DbType = DbType.Int32,
                    Value = assemblyName.Version.Revision
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inMachineName",
                    DbType = DbType.String,
                    Value = Environment.MachineName
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inAppDomainName",
                    DbType = DbType.String,
                    Value = AppDomain.CurrentDomain.FriendlyName
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inProcessId",
                    DbType = DbType.Int32,
                    Value = this.ValueOrDbNull(processId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inThreadId",
                    DbType = DbType.String,
                    Size = 255,
                    Value = this.ValueOrDbNull(threadId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceEventType",
                    DbType = DbType.Int32,
                    Value = (int)traceEventType
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceListenerName",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(this.Name)
                });
                var recordId = new SqlParameter
                {
                    ParameterName = "outMessageLogId",
                    DbType = DbType.Int64,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(recordId);

                if (ConnectionState.Open != connection.State)
                {
                    connection.Open();
                }

                command.ExecuteNonQuery();
                long messageId = (long)recordId.Value;

                return messageId;
            }
        }

        /// <summary>
        /// Return the given value if it is not null otherwise return DBNull.Value.
        /// </summary>
        /// <param name="value">Value to return if not null.</param>
        /// <returns>Given value or DBNull.Value if the given value is null.</returns>
        private object ValueOrDbNull(object value)
        {
            if (null == value)
            {
                return DBNull.Value;
            }

            return value;
        }
    }
}
