using Accelerate.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Accelerate.Data.Extensions
{
    /// <summary>
    /// Extension methods for IDataReader interface.
    /// </summary>
    internal static class IDataReaderExtensions
    {
        /// <summary>
        /// Read an entity from the data source.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity to return.
        /// </typeparam>
        /// <param name="dataReader">
        /// Data reader object.
        /// </param>
        internal static TEntity ReadAndMapEntity<TEntity>(this IDataReader dataReader) where TEntity : class, IEntity, new()
        {
            var entity = Activator.CreateInstance<TEntity>();
            var typeProperties = typeof(TEntity).GetProperties()
                                                .ToList();

            for (var fieldIndex = 0; fieldIndex < dataReader.FieldCount; fieldIndex++)
            {
                var columnName = dataReader.GetName(fieldIndex);
                var propertyInfo = dataReader.SetValueByAttribute(fieldIndex, entity, columnName, typeProperties);

                if (propertyInfo == null)
                {
                    propertyInfo = dataReader.SetValueByProperty(fieldIndex, entity, columnName, typeProperties);
                }

                if (propertyInfo != null)
                {
                    typeProperties.Remove(propertyInfo);
                }
            }

            return entity;
        }
        /// <summary>
        /// Set the value of property based on ColumnAttribute.
        /// </summary>
        /// <param name="dataReader">
        /// Data reader object.
        /// </param>
        /// <param name="fieldIndex">
        /// Index of field in the data reader.
        /// </param>
        /// <param name="entity">
        /// Entity where set the value of property.
        /// </param>
        /// <param name="columnName">
        /// Name of column.
        /// </param>
        /// <param name="typeProperties">
        /// Collection of entity properties.
        /// </param>
        private static PropertyInfo SetValueByAttribute(this IDataReader dataReader, Int32 fieldIndex, IEntity entity, String columnName, IEnumerable<PropertyInfo> typeProperties)
        {
            PropertyInfo propertyInfo = null;

            foreach (var typeProperty in typeProperties)
            {
                try
                {
                    var columnAttribute = (ColumnAttribute)typeProperty.GetCustomAttributes(true)
                                                                       .FirstOrDefault(x => x.GetType() == typeof(ColumnAttribute));

                    if (columnAttribute != null && columnAttribute.Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        typeProperty.SetValue(entity, dataReader.IsDBNull(fieldIndex) ? null : dataReader.GetValue(fieldIndex));
                        propertyInfo = typeProperty;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Error setting value of column '{columnName}'", ex);
                }
            }

            return propertyInfo;
        }
        /// <summary>
        /// Set the value of property based on property match.
        /// </summary>
        /// <param name="dataReader">
        /// Data reader object.
        /// </param>
        /// <param name="fieldIndex">
        /// Index of field in the data reader.
        /// </param>
        /// <param name="entity">
        /// Entity where set the value of property.
        /// </param>
        /// <param name="columnName">
        /// Name of column.
        /// </param>
        /// <param name="typeProperties">
        /// Collection of entity properties.
        /// </param>

        private static PropertyInfo SetValueByProperty(this IDataReader dataReader, Int32 fieldIndex, IEntity entity, String columnName, IEnumerable<PropertyInfo> typeProperties)
        {
            PropertyInfo propertyInfo = null;

            try
            {
                var typeProperty = typeProperties.FirstOrDefault(x => x.Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase));

                if (typeProperty != null)
                {
                    if (!dataReader.IsDBNull(fieldIndex))
                    {
                        var value = dataReader.GetValue(fieldIndex);
                        typeProperty.SetValue(entity, Convert.ChangeType(value, typeProperty.PropertyType));
                    }

                    propertyInfo = typeProperty;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error setting value of property '{columnName}'", ex);
            }

            return propertyInfo;
        }
    }
}