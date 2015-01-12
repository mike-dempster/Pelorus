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
