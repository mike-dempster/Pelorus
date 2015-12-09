using Pelorus.Core.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Creates and manages a named distributed exclusive lock.
    /// </summary>
    public class DistributedMutualExclusion : MutualExclusion
    {
        private readonly bool _isNestedScope;
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        /// <summary>
        /// Creates a new instance of the named lock and returns when ownership of the lock is obtained.
        /// </summary>
        /// <param name="name">Name of the lock.</param>
        public DistributedMutualExclusion(string name) : this(name, 0)
        {
        }

        /// <summary>
        /// Creates a new instance of the named lock and returns when ownership of the lock is obtained.
        /// </summary>
        /// <param name="name">Name of the lock.</param>
        /// <param name="timeout">Max length of time (in seconds) to wait to obtain the lock.</param>
        public DistributedMutualExclusion(string name, int timeout) : base(name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this._isNestedScope = this.ExclusionOwned();

            if (this._isNestedScope)
            {
                return;
            }

            using (new TransactionScope(TransactionScopeOption.Suppress)) // Ensure that we do not participate in any other transactions
            {
                string connectionString = this.GetConnectionString();
                this._connection = new SqlConnection(connectionString);

                using (var cmd = this._connection.CreateCommand())
                {
                    cmd.CommandTimeout = timeout;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.EnterLockState;
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "globallyUniqueName",
                        DbType = DbType.String,
                        Value = name
                    });
                    this._connection.Open();
                    this._transaction = this._connection.BeginTransaction();
                    cmd.Transaction = this._transaction;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.ToLower().Contains("timeout expired."))
                        {
                            throw new TimeoutException("Unable to obtain the exclusive lock before the timeout expired.");
                        }

                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Releases the distributed lock.
        /// </summary>
        /// <param name="disposing">Indicates if the method is being called because the instance is being disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if ((this._isNestedScope) || (null == this._connection))
            {
                return;
            }

            if (null != this._transaction)
            {
                this._transaction.Rollback();
            }

            this._connection.Close();
            this._connection.Dispose();
        }

        /// <summary>
        /// Gets the connection string from the configuration data. The method searches for the name of the connection string to use in the following xpaths:
        /// configuration/pelorus.core/distributedMutualExclusion[@connectionStringName]
        /// configuration/appSettings/add[@key=DistributedMutualExclusionConnectionString]
        /// If the connection string name is not found at any of these xpaths, the default name will be used.
        /// </summary>
        /// <returns>Connection string to use to connect to the database.</returns>
        /// <exception cref="ConfigurationErrorsException">This exception is thrown if the connection string cannot be found in the configuration data.</exception>
        private string GetConnectionString()
        {
            string connectionStringName = "MutualExclusions";

            if ((null != CoreConfiguration.DistributedMutualExclusion) && (false == string.IsNullOrWhiteSpace(CoreConfiguration.DistributedMutualExclusion.ConnectionStringName)))
            {
                connectionStringName = CoreConfiguration.DistributedMutualExclusion.ConnectionStringName;
            }
            else
            {
                string appSettingConnectionStringName = ConfigurationManager.AppSettings["DistributedMutualExclusionConnectionStringName"];

                if (false == string.IsNullOrWhiteSpace(appSettingConnectionStringName))
                {
                    connectionStringName = appSettingConnectionStringName;
                }
            }

            var connectionStringObject = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (null == connectionStringObject)
            {
                throw new ConfigurationErrorsException("Connection string does not exist.");
            }

            return connectionStringObject.ConnectionString;
        }
    }
}
