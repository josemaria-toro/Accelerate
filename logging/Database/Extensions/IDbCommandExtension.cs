using System;
using System.Data;

namespace Accelerate.Extensions
{
    /// <summary>
    /// Extension methods for IDbCommand interface.
    /// </summary>
    internal static class IDbCommandExtensions
    {
        /// <summary>
        /// Add parameter to command.
        /// </summary>
        /// <param name="command">
        /// Command instance.
        /// </param>
        /// <param name="parameterName">
        /// Parameter name.
        /// </param>
        /// <param name="parameterValue">
        /// Parameter value.
        /// </param>
        internal static void AddParameter(this IDbCommand command, String parameterName, Object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }
    }
}