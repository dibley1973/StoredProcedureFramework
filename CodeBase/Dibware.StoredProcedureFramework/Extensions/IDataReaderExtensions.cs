using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Data;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Extensions
{
    internal static class IDataReaderExtensions
    {
        /// <summary>
        /// Gets the data type of the specified column by name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">fieldName</exception>
        private static Type GetFieldType(this IDataReader instance,
            string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) throw new ArgumentNullException("fieldName");

            var ordinal = instance.GetOrdinal(fieldName);
            return instance.GetFieldType(ordinal);
        }

        /// <summary>
        /// Read data for the current result row from a reader into a destination object, by the name
        /// of the properties on the destination object.
        /// </summary>
        /// <param name="reader">data reader holding return data</param>
        /// <param name="targetObject">object to populate</param>
        /// <param name="dtoListItemTypePropertyInfos">properties list to copy from result set row 'reader' to object 'targetObject'</param>
        public static void ReadRecord(this IDataReader reader, object targetObject, PropertyInfo[] dtoListItemTypePropertyInfos)
        {
            string fieldName = "";

            foreach (PropertyInfo listItemPropertyInfo in dtoListItemTypePropertyInfos)
            {
                try
                {
                    fieldName = listItemPropertyInfo.Name;

                    // TODO: Placeholder for handling StreamOutputs, here
                    // TODO: investigate breaking this out into a dedicated "RecordReader" class if complexity grows

                    var fieldData = reader[fieldName];
                    if (fieldData is DBNull)
                    {
                        HandleDbNullValues(reader, targetObject, listItemPropertyInfo, fieldName);
                    }
                    else
                    {
                        listItemPropertyInfo.SetValue(targetObject, fieldData, null);
                    }
                }
                catch (Exception ex)
                {
                    string returnTypeName = targetObject.GetType().Name;
                    HandleMissingField(ex, fieldName, returnTypeName);
                    HandleArgumentException(ex, fieldName, returnTypeName);
                    HandleOtherExceptions(ex, fieldName, returnTypeName);
                }
            }
        }

        private static void HandleArgumentException(Exception ex, string fieldName, string returnTypeName)
        {
            if (!(ex is ArgumentException)) return;

            string message = string.Format(
                ExceptionMessages.IncorrectReturnType, fieldName, returnTypeName);

            throw new InvalidCastException(message, ex);
        }

        private static void HandleDbNullValues(IDataReader reader,
            object targetObject,
            PropertyInfo propertyInfo,
            string fieldName)
        {
            var propertyType = propertyInfo.PropertyType;
            var isNullableType = (propertyType == typeof(string) ||
                (Nullable.GetUnderlyingType(propertyType) != null));
            if (isNullableType)
            {
                propertyInfo.SetValue(targetObject, null, null);
            }
            else
            {
                Type targetObjectType = reader.GetFieldType(fieldName);
                throw new NullableFieldTypeException(fieldName, propertyType, targetObjectType);
            }
        }

        /// <summary>
        /// Handles the missing field.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="returnTypeName">Name of the return type.</param>
        /// <exception cref="System.MissingFieldException">Thrown if the specified exception is IndexOutOfRangeException</exception>
        private static void HandleMissingField(Exception ex,
            string fieldName, string returnTypeName)
        {
            if (!(ex is IndexOutOfRangeException)) return;

            string message = string.Format(ExceptionMessages.FieldNotFoundForName,
                fieldName, returnTypeName);

            throw new MissingFieldException(message, ex);
        }

        /// <summary>
        /// Handles the other exceptions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="returnTypeName">Name of the return type.</param>
        private static void HandleOtherExceptions(Exception ex, string fieldName, string returnTypeName)
        {
            string message = string.Format(
                ExceptionMessages.ProcessingReturnColumnError, fieldName, returnTypeName);

            throw (Exception)Activator.CreateInstance(ex.GetType(), message, ex);
        }
    }
}