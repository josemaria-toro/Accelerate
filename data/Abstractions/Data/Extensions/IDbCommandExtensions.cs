using Accelerate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Accelerate.Data.Extensions
{
    /// <summary>
    /// Extension methods for IDbCommand interface.
    /// </summary>
    internal static class IDbCommandExtensions
    {
        /// <summary>
        /// Execute a command to write data into the data source.
        /// </summary>
        /// <param name="dbCommand">
        /// Command object.
        /// </param>
        internal static Int32 Execute(this IDbCommand dbCommand)
        {
            var affectedRows = 0;

            try
            {
                affectedRows = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataException("Error executing query for writing data", ex);
            }

            return affectedRows;
        }
        /// <summary>
        /// Execute a command to read data from the data source.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity to return.
        /// </typeparam>
        /// <param name="dbCommand">
        /// Command object.
        /// </param>
        internal static IEnumerable<TEntity> Execute<TEntity>(this IDbCommand dbCommand) where TEntity : class, IEntity, new()
        {
            var entities = new List<TEntity>();

            try
            {
                using (var dbDataReader = dbCommand.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var entity = dbDataReader.ReadAndMapEntity<TEntity>();
                        entities.Add(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException("Error executing query for reading data", ex);
            }

            return entities;
        }
    }
}