using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Contains functions to help when using Sql Server Table Value Parameters
    /// </summary>
    internal static class TableValuedParameterHelper
    {
        private const int DefaultDecimalParameterPrecision = 10;
        private const int DefaultDecimalParameterScale = 2;

        /// <summary>
        /// Get the underlying class type for lists, etc. that implement IEnumerable<>.
        /// </summary>
        /// <param name="listType"></param>
        /// <returns></returns>
        private static Type GetUnderlyingType(Type listType)
        {
            Type basetype = null;
            foreach (Type interfaceTypes in listType.GetInterfaces())
            {
                if (interfaceTypes.IsGenericType && interfaceTypes.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    basetype = interfaceTypes.GetGenericArguments()[0];
                }
            }
            return basetype;
        }

        /// <summary>
        /// Do the work of converting a source data object to SqlDataRecords
        /// using the parameter attributes to create the itemList valued parameter definition
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        internal static IEnumerable<SqlDataRecord> GetTableValuedParameterFromList(IList itemList) // TODO consider changing to IEnumerable
        {
            // Get the object type underlying our itemList
            Type listObjectType = GetUnderlyingType(itemList.GetType());

            // Create a list of SqlDataRecord which will be populated and returned to the caller
            List<SqlDataRecord> recordList = new List<SqlDataRecord>();

            // Create a list of column definitions
            List<SqlMetaData> columnList = new List<SqlMetaData>();

            // Get all mapped properties of the objects in the list
            PropertyInfo[] mappedProperties = listObjectType.GetMappedProperties();

            // Generate the SqlMetaData for each property/column and add it to a dictionary
            Dictionary<String, String> mapping = new Dictionary<string, string>();
            foreach (PropertyInfo propertyInfo in mappedProperties)
            {
                CreateAndAddSqlMetaDataColumn(propertyInfo, mapping, columnList);
            }

            // Load each object in the input data itemList into sql data records
            foreach (object item in itemList)
            {
                CreateAndAddSqlDataRecord(columnList, mappedProperties, mapping, item, recordList);
            }

            // Return the list of data records
            return recordList;
        }

        /// <summary>
        /// Creates and adds the SQL data record to the specified recordList.
        /// </summary>
        /// <param name="columnList">The column list.</param>
        /// <param name="mappedProperties">The mapped properties.</param>
        /// <param name="mapping">The mapping.</param>
        /// <param name="item">The item.</param>
        /// <param name="recordList">The record list.</param>
        private static void CreateAndAddSqlDataRecord(List<SqlMetaData> columnList,
            PropertyInfo[] mappedProperties, Dictionary<string, string> mapping, object item,
            List<SqlDataRecord> recordList)
        {
            // Create the sql data record using the column definition
            SqlDataRecord record = new SqlDataRecord(columnList.ToArray());
            for (int index = 0; index < columnList.Count(); index += 1)
            {
                // locate the value of the matching property
                var value = mappedProperties
                    .First(propertyInfo => propertyInfo.Name == mapping[columnList[index].Name])
                    .GetValue(item, null);

                // set the value
                record.SetValue(index, value);
            }

            // add the sql data record to our output list
            recordList.Add(record);
        }

        /// <summary>
        /// Creates and adds the SQL meta data column for the current property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="mapping">The mapping.</param>
        /// <param name="columnlist">The columnlist.</param>
        private static void CreateAndAddSqlMetaDataColumn(PropertyInfo propertyInfo,
            Dictionary<string, string> mapping, List<SqlMetaData> columnlist)
        {
            // Get the propery column name to property name mapping. The default 
            // name is property name, override of parameter name by attribute if available
            NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
            string name = (nameAttribute == null)
                ? propertyInfo.Name
                : nameAttribute.Value;
            mapping.Add(name, propertyInfo.Name);

            // The default type is the property CLR type, but override if ParameterDbTypeAttribute if available                
            ParameterDbTypeAttribute dbTypeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
            SqlDbType columnType = (dbTypeAttribute != null)
                ? dbTypeAttribute.Value
                : SqlParameterHelper.GetSqlDbType(propertyInfo.PropertyType);

            // Create metadata column definition, handling specific dimension attributes if available
            SqlMetaData column;
            switch (columnType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.Image:
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.NText:
                case SqlDbType.VarBinary:
                    // Get column size
                    var sa = propertyInfo.GetAttribute<SizeAttribute>();
                    int size = (null == sa) ? 50 : sa.Value;
                    column = new SqlMetaData(name, columnType, size);
                    break;

                case SqlDbType.Decimal:
                    // Get column precision and scale from attributes if available
                    var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
                    Byte precision = (null == precisionAttribute)
                        ? (byte)DefaultDecimalParameterPrecision
                        : precisionAttribute.Value;

                    var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
                    Byte scale = (null == scaleAttribute)
                        ? (byte)DefaultDecimalParameterScale
                        : scaleAttribute.Value;

                    column = new SqlMetaData(name, columnType, precision, scale);
                    break;

                default:
                    column = new SqlMetaData(name, columnType);
                    break;
            }

            // Add metadata to column list
            columnlist.Add(column);
        }
    }
}