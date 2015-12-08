﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Pelorus.Core.Synchronization
{
    /// <summary>
    /// Creates and manages a named distributed exclusive lock.
    /// </summary>
    public class DistributedExclusiveLock : ExclusiveLock
    {
        private readonly bool _isNestedScope;
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        /// <summary>
        /// Creates a new instance of the named lock and returns when ownership of the lock is obtained.
        /// </summary>
        /// <param name="name">Name of the lock.</param>
        public DistributedExclusiveLock(string name) : base(name)
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

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                var connectionStringObject = ConfigurationManager.ConnectionStrings["DistributedLockConnectionString"];

                if (null == connectionStringObject)
                {
                    throw new Exception("Connection string does not exist.");
                }

                string connectionString = connectionStringObject.ConnectionString;
                this._connection = new SqlConnection(connectionString);

                using (var cmd = this._connection.CreateCommand())
                {
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
                    cmd.ExecuteNonQuery();
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

            try
            {
                using(new TransactionScope(TransactionScopeOption.Suppress))
                using (var cmd = this._connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.ReleaseLockState;
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "globallyUniqueName",
                        DbType = DbType.String,
                        Value = this.Name
                    });

                    if (ConnectionState.Open != this._connection.State)
                    {
                        this._connection.Open();
                    }

                    cmd.Transaction = this._transaction;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (null != this._transaction)
                {
                    this._transaction.Rollback();
                }

                this._connection.Close();
                this._connection.Dispose();
            }
        }
    }
}
