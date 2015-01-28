using Pelorus.Core.Data;
using Pelorus.Core.Diagnostics.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Pelorus.Core.Diagnostics.Repositories
{
    /// <summary>
    /// Simple repository for interfacing with the application log data store.
    /// </summary>
    internal class ApplicationLogRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Creates a new instance of the repository and initializes the internal state.
        /// </summary>
        /// <param name="connectionString">Connection string to use for connecting to the data store.</param>
        public ApplicationLogRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// Gets an application log by Id.
        /// </summary>
        /// <param name="applicationLogId">Id of the application log to get.</param>
        /// <returns>Application log with the given Id or null if the record does not exist.</returns>
        public ApplicationLogDao GetById(long applicationLogId)
        {
            var messageIdParameter = new SqlParameter
            {
                ParameterName = "messageId",
                DbType = DbType.Int64,
                Value = applicationLogId
            };
            var resultCollection = this.ExecuteSelect(SqlQueries.GetApplicationLogById, messageIdParameter);
            var result = resultCollection.SingleOrDefault();

            return result;
        }

        /// <summary>
        /// Gets a collection of application logs by correlation Id.
        /// </summary>
        /// <param name="correlationId">Correlation Id of the application logs to get.</param>
        /// <returns>Collection of application logs with the given correlation Id.</returns>
        public IEnumerable<ApplicationLogDao> GetByCorrelationId(Guid correlationId)
        {
            var correlationIdParameter = new SqlParameter
            {
                ParameterName = "correlationId",
                DbType = DbType.Guid,
                Value = correlationId
            };
            var results = this.ExecuteSelect(SqlQueries.GetApplicationLogsByCorrelationId, correlationIdParameter);

            return results;
        }

        /// <summary>
        /// Gets a collection of application logs that were created on or after the given date.
        /// </summary>
        /// <param name="oldestCreatedOn">Eariliest date of the application logs to get.</param>
        /// <returns>Collection of application logs that were created on or after the given date.</returns>
        public IEnumerable<ApplicationLogDao> GetSinceDate(DateTime oldestCreatedOn)
        {
            var oldestCreatedOnParameter = new SqlParameter
            {
                ParameterName = "oldestCreatedOn",
                DbType = DbType.DateTime,
                Value = oldestCreatedOn
            };
            var results = this.ExecuteSelect(SqlQueries.GetApplicationLogsSinceDate, oldestCreatedOnParameter);

            return results;
        }

        /// <summary>
        /// Gets a collection of application logs by event type.
        /// </summary>
        /// <param name="eventType">Event type of the application logs to get.</param>
        /// <returns>Collection of application logs with the given event type.</returns>
        public IEnumerable<ApplicationLogDao> GetByEventType(TraceEventType eventType)
        {
            var eventTypeParameter = new SqlParameter
            {
                ParameterName = "eventType",
                DbType = DbType.Int32,
                Value = (int)eventType
            };
            var results = this.ExecuteSelect(SqlQueries.GetApplicationLogsByEventType, eventTypeParameter);

            return results;
        }

        /// <summary>
        /// Gets a collection of application logs with the given event type that were created on or after the given date.
        /// </summary>
        /// <param name="eventType">Event type of the application logs to get.</param>
        /// <param name="oldestCreatedOn">Earliest date of the application logs to get.</param>
        /// <returns>Collection of application logs with the given event type that were created on or after the given date.</returns>
        public IEnumerable<ApplicationLogDao> GetByEventTypeSinceDate(TraceEventType eventType, DateTime oldestCreatedOn)
        {
            var queryParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "eventType",
                    DbType = DbType.Int32,
                    Value = (int)eventType
                },
                new SqlParameter
                {
                    ParameterName = "oldestCreatedOn",
                    DbType = DbType.DateTime,
                    Value = oldestCreatedOn
                }
            };
            var results = this.ExecuteSelect(SqlQueries.GetApplicationLogsByEventTypeSinceDate, queryParameters);

            return results;
        }

        /// <summary>
        /// Creates a new application log.
        /// </summary>
        /// <param name="applicationLog">New application log to create in the database.</param>
        /// <returns>Id of the new application log record.</returns>
        public long Create(ApplicationLogDao applicationLog)
        {
            using (var connection = new SqlConnection(this._connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = SqlQueries.InsertApplicationLog;
                var assembly = Assembly.GetExecutingAssembly();
                var assemblyName = assembly.GetName();
                string traceDataXmlString = null;

                if (null != applicationLog.Data)
                {
                    traceDataXmlString = applicationLog.Data.InnerXml;
                }

                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inMessage",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(applicationLog.Message)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inHelpLink",
                    DbType = DbType.String,
                    Size = 2083,
                    Value = this.ValueOrDbNull(applicationLog.HelpLink)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inSource",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(applicationLog.Source)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inStackTrace",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(applicationLog.StackTrace)
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
                    Value = this.ValueOrDbNull(applicationLog.TraceId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inCorrelationId",
                    DbType = DbType.Guid,
                    Value = this.ValueOrDbNull(applicationLog.CorrelationId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inCorrelationIndex",
                    DbType = DbType.Int32,
                    Value = this.ValueOrDbNull(applicationLog.CorrelationIndex)
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
                    Value = this.ValueOrDbNull(applicationLog.ProcessId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inThreadId",
                    DbType = DbType.String,
                    Size = 255,
                    Value = this.ValueOrDbNull(applicationLog.ThreadId)
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceEventType",
                    DbType = DbType.Int32,
                    Value = (int)applicationLog.TraceEventType
                });
                command.Parameters.Add(new SqlParameter
                {
                    ParameterName = "inTraceListenerName",
                    DbType = DbType.String,
                    Value = this.ValueOrDbNull(applicationLog.TraceListenerName)
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

        /// <summary>
        /// Executes a select statement and maps the results to DAO objects.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <param name="parameter">Parameter for the SQL statement.</param>
        /// <returns>Results that were returned from the SQL statement.</returns>
        private IEnumerable<ApplicationLogDao> ExecuteSelect(string sqlStatement, DbParameter parameter)
        {
            return this.ExecuteSelect(sqlStatement, new[] { parameter });
        }

        /// <summary>
        /// Executes a select statement and maps the results to DAO objects.
        /// </summary>
        /// <param name="sqlStatement">SQL statement to execute.</param>
        /// <param name="parameters">Parameters for the SQL statement.</param>
        /// <returns>Results that were returned from the SQL statement.</returns>
        private IEnumerable<ApplicationLogDao> ExecuteSelect(string sqlStatement, IEnumerable<DbParameter> parameters)
        {
            var logs = new Collection<ApplicationLogDao>();

            using (var connection = new SqlConnection(this._connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = sqlStatement;
                command.Parameters.AddRange(parameters.ToArray());

                if (ConnectionState.Open != connection.State)
                {
                    connection.Open();
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var applicationLog = this.MapRecordToDao(reader);
                        logs.Add(applicationLog);
                    }
                }
            }

            return logs;
        }

        /// <summary>
        /// Maps a record returned from the database to a DAO.
        /// </summary>
        /// <param name="record">Record with the data to map to a DAO.</param>
        /// <returns>DAO mapped from the data in the record.</returns>
        private ApplicationLogDao MapRecordToDao(IDataRecord record)
        {
            return new ApplicationLogDao
            {
                AppDomainName = record.GetString("AppDomainName"),
                Assembly = new AssemblyDao
                {
                    AssemblyFullName = record.GetString("AssemblyFullName"),
                    AssemblyName = record.GetString("AssemblyName"),
                    CreatedBy = record.GetString("AssemblyCreatedBy"),
                    CreatedOn = record.GetDateTime("AssemblyCreatedOn"),
                    Id = record.GetInt32("AssemblyId"),
                    LastUpdatedBy = record.GetString("AssemblyLastUpdatedBy"),
                    LastUpdatedOn = record.GetDateTime("AssemblyLastUpdatedOn"),
                    VersionBuild = record.GetInt32("VersionBuild"),
                    VersionMajor = record.GetInt32("VersionMajor"),
                    VersionMinor = record.GetInt32("VersionMinor"),
                    VersionRevision = record.GetInt32("VersionRevision")
                },
                AssemblyId = record.GetInt32("AssemblyId"),
                CorrelationId = record.GetNullableGuid("CorrelationId"),
                CorrelationIndex = record.GetNullableInt16("CorrelationIndex"),
                CreatedBy = record.GetString("CreatedBy"),
                CreatedOn = record.GetDateTime("CreatedOn"),
                Data = record.GetNullableXml("Data"),
                HelpLink = record.GetNullableString("HelpLink"),
                Id = record.GetInt64("Id"),
                LastUpdatedBy = record.GetString("LastUpdatedBy"),
                LastUpdatedOn = record.GetDateTime("LastUpdatedOn"),
                MachineName = record.GetString("MachineName"),
                Message = record.GetNullableString("Message"),
                ProcessId = record.GetInt32("ProcessId"),
                Source = record.GetString("Source"),
                StackTrace = record.GetNullableString("StackTrace"),
                ThreadId = record.GetString("ThreadId"),
                TraceEventType = (TraceEventType)record.GetInt32("TraceEventType"),
                TraceId = record.GetInt32("TraceId"),
                TraceListenerName = record.GetNullableString("TraceListenerName")
            };
        }
    }
}
