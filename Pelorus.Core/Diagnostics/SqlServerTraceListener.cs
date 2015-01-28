using Pelorus.Core.Diagnostics.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
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
        protected override void LogMessage(TraceEventData traceData)
        {
            object helpLink;

            traceData.Context.TryGetValue("exceptionHelpLink", out helpLink);

            this.InsertMessage(
                message: traceData.Message,
                helpLink: helpLink as string,
                source: traceData.Source,
                stackTrace: traceData.Callstack,
                traceData: traceData.Data,
                traceEventType: traceData.EventType,
                traceId: traceData.EventId,
                correlationId: traceData.CorrelationId,
                correlationIndex: traceData.CorrelationIndex,
                processId: traceData.ProcessId,
                threadId: traceData.ThreadId);
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
            Nullable<Guid> correlationId,
            Nullable<short> correlationIndex,
            int processId,
            string threadId)
        {
            string connectionString = this.GetConnectionString();
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var repository = new ApplicationLogRepository(connectionString);
            var applicationLog = new ApplicationLogDao
            {
                AppDomainName = AppDomain.CurrentDomain.FriendlyName,
                Assembly = new AssemblyDao
                {
                    AssemblyFullName = assemblyName.FullName,
                    AssemblyName = assemblyName.Name,
                    VersionBuild = assemblyName.Version.Build,
                    VersionMajor = assemblyName.Version.Major,
                    VersionMinor = assemblyName.Version.Minor,
                    VersionRevision = assemblyName.Version.Revision
                },
                CorrelationId = correlationId,
                CorrelationIndex = correlationIndex,
                Data = traceData,
                HelpLink = helpLink,
                MachineName = Environment.MachineName,
                Message = message,
                ProcessId = processId,
                Source = source,
                StackTrace = stackTrace,
                ThreadId = threadId,
                TraceEventType = traceEventType,
                TraceId = traceId,
                TraceListenerName = this.Name
            };
            long applicationLogId = repository.Create(applicationLog);

            return applicationLogId;
        }
    }
}
