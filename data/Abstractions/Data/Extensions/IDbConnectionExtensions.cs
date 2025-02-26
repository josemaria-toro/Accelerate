using Accelerate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace 
Accelerate.Data.Extensions
{
    /// <summary>
    /// Extension methods for IDbConnection interface.
    /// </summary>
    public static class IDbConnectionExtensions
    {
        /// <summary>
        /// Create and configure a data command.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        /// <param name="queryTimeout">
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </param>
        private static IDbCommand CreateCommand(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters, Int32 queryTimeout)
        {
            if (dbConnection == null)
            {
                throw new ArgumentException("The connection object cannot be null", nameof(dbConnection));
            }

            if (dbConnection.State != ConnectionState.Open)
            {
                throw new ArgumentException("The connection object has an invalid state", nameof(dbConnection));
            }

            if (String.IsNullOrEmpty(queryString))
            {
                throw new ArgumentException("The text of query cannot be empty", nameof(queryString));
            }

            var dbCommand = dbConnection.CreateCommand();

            dbCommand.CommandText = queryString;
            dbCommand.CommandTimeout = queryTimeout;
            dbCommand.CommandType = CommandType.Text;

            if (queryParameters != null)
            {
                foreach (var queryParameter in queryParameters)
                {
                    try
                    {
                        var dbParameter = dbCommand.CreateParameter();

                        dbParameter.Direction = ParameterDirection.Input;
                        dbParameter.ParameterName = queryParameter.Key;
                        dbParameter.Value = queryParameter.Value;

                        dbCommand.Parameters.Add(dbParameter);
                    }
                    catch (Exception ex)
                    {
                        throw new DataException($"Error creating and configuring data parameter '{queryParameter.Key}'", ex);
                    }
                }
            }

            return dbCommand;
        }
        /// <summary>
        /// Execute a sql query to data deletion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        public static Int32 Delete(this IDbConnection dbConnection, String queryString)
        {
            return Delete(dbConnection, queryString, null);
        }
        /// <summary>
        /// Execute a sql query to data deletion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        public static Int32 Delete(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters)
        {
            return Delete(dbConnection, queryString, queryParameters, 30);
        }
        /// <summary>
        /// Execute a sql query to data deletion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        /// <param name="queryTimeout">
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </param>
        public static Int32 Delete(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters, Int32 queryTimeout)
        {
            var affectedRows = 0;

            using (var dbCommand = CreateCommand(dbConnection, queryString, queryParameters, queryTimeout))
            {
                affectedRows = dbCommand.Execute();
            }

            return affectedRows;
        }
        /// <summary>
        /// Execute a sql query to data insertion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        public static Int32 Insert(this IDbConnection dbConnection, String queryString)
        {
            return Insert(dbConnection, queryString, null);
        }
        /// <summary>
        /// Execute a sql query to data insertion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        public static Int32 Insert(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters)
        {
            return Insert(dbConnection, queryString, queryParameters, 30);
        }
        /// <summary>
        /// Execute a sql query to data insertion.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        /// <param name="queryTimeout">
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </param>
        public static Int32 Insert(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters, Int32 queryTimeout)
        {
            var affectedRows = 0;

            using (var dbCommand = CreateCommand(dbConnection, queryString, queryParameters, queryTimeout))
            {
                affectedRows = dbCommand.Execute();
            }

            return affectedRows;
        }
        /// <summary>
        /// Execute a sql query to data selection.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        public static IEnumerable<TEntity> Select<TEntity>(this IDbConnection dbConnection, String queryString) where TEntity : class, IEntity, new()
        {
            return Select<TEntity>(dbConnection, queryString, null);
        }
        /// <summary>
        /// Execute a sql query to data selection.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        public static IEnumerable<TEntity> Select<TEntity>(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters) where TEntity : class, IEntity, new()
        {
            return Select<TEntity>(dbConnection, queryString, queryParameters, 30);
        }
        /// <summary>
        /// Execute a sql query to data selection.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        /// <param name="queryTimeout">
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </param>
        public static IEnumerable<TEntity> Select<TEntity>(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters, Int32 queryTimeout) where TEntity : class, IEntity, new()
        {
            var entities = default(IEnumerable<TEntity>);

            using (var dbCommand = CreateCommand(dbConnection, queryString, queryParameters, queryTimeout))
            {
                entities = dbCommand.Execute<TEntity>();
            }

            return entities;
        }
        /// <summary>
        /// Execute a sql query to update data.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        public static Int32 Update(this IDbConnection dbConnection, String queryString)
        {
            return Update(dbConnection, queryString, null);
        }
        /// <summary>
        /// Execute a sql query to update data.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        public static Int32 Update(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters)
        {
            return Update(dbConnection, queryString, queryParameters, 30);
        }
        /// <summary>
        /// Execute a sql query to update data.
        /// </summary>
        /// <param name="dbConnection">
        /// Database connection.
        /// </param>
        /// <param name="queryString">
        /// Raw text of query.
        /// </param>
        /// <param name="queryParameters">
        /// Parameters of query.
        /// </param>
        /// <param name="queryTimeout">
        /// The wait time, in seconds, before terminating the execution of the query and throw an error.
        /// </param>
        public static Int32 Update(this IDbConnection dbConnection, String queryString, IDictionary<String, Object> queryParameters, Int32 queryTimeout)
        {
            var affectedRows = 0;

            using (var dbCommand = CreateCommand(dbConnection, queryString, queryParameters, queryTimeout))
            {
                affectedRows = dbCommand.Execute();
            }

            return affectedRows;
        }
    }
}