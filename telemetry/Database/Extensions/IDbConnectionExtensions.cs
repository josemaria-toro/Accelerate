using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Accelerate.Extensions
{
    /// <summary>
    /// Extension class for <see cref="IDbConnection" />.
    /// </summary>
    internal static class IDbConnectionExtensions
    {
        /// <summary>
        /// Create a new command based on the connection, query string, entity and json serializer options.
        /// </summary>
        /// <param name="connection">
        /// Database connection instance.
        /// </param>
        /// <param name="queryString">
        /// Query string to execute.
        /// </param>
        /// <param name="entity">
        /// Entity to create parameters for.
        /// </param>
        /// <param name="jsonSerializerOptions">
        /// Serializer options to use with properties of type dictionary.
        /// </param>
        internal static IDbCommand CreateCommand(this IDbConnection connection, String queryString, Object entity, JsonSerializerOptions jsonSerializerOptions)
        {
            var command = connection.CreateCommand();

            command.CommandText = queryString;
            command.CommandTimeout = 5;
            command.CommandType = CommandType.Text;

            var properties = entity.GetType()
                                   .GetProperties();

            foreach (var property in properties)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{property.Name.ToLowerInvariant()}";
                parameter.Value = property.GetValue(entity);

                if (property.PropertyType == typeof(IDictionary<String, Object>))
                {
                    var value = (IDictionary<String, Object>)parameter.Value;
                    parameter.Value = JsonSerializer.Serialize(value, jsonSerializerOptions);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    var value = (DateTime)parameter.Value;
                    parameter.Value = value.ToUniversalTime();
                }

                command.Parameters.Add(parameter);
            }

            return command;
        }
    }
}