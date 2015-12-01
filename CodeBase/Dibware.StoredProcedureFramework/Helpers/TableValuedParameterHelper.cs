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
        private const List<SqlDataRecord> DefaultValueForEmptyList = null;

        /// <summary>
        /// Do the work of converting a source data object to SqlDataRecords
        /// using the parameter attributes to create the itemList valued parameter definition
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        internal static IEnumerable<SqlDataRecord> GetTableValuedParameterFromList(IList itemList) // TODO consider changing to IEnumerable
        {
            List<SqlDataRecord> recordList = new List<SqlDataRecord>();
            List<SqlMetaData> columnList = new List<SqlMetaData>();
            Dictionary<String, String> mapping = new Dictionary<string, string>();
            Type listTypeUnderlyingType = GetListTypeUnderlyingType(itemList.GetType());
            PropertyInfo[] mappedProperties = listTypeUnderlyingType.GetMappedProperties();

            foreach (PropertyInfo propertyInfo in mappedProperties)
            {
                var name = GetNamefromAttributeOrPropertyName(propertyInfo);
                mapping.Add(name, propertyInfo.Name);
                CreateAndAddSqlMetaDataColumn(propertyInfo, name, columnList);
            }

            if (itemList.Count == 0) return DefaultValueForEmptyList;

            foreach (object item in itemList)
            {
                CreateAndAddSqlDataRecord(columnList, mappedProperties, mapping, item, recordList);
            }

            return recordList;
        }

        #region Methods : private or protected

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
            SqlDataRecord record = new SqlDataRecord(columnList.ToArray());
            
            for (int index = 0; index < columnList.Count(); index += 1)
            {
                var valueOfMatchedProperty = mappedProperties
                    .First(propertyInfo => propertyInfo.Name == mapping[columnList[index].Name])
                    .GetValue(item, null);

                record.SetValue(index, valueOfMatchedProperty);
            }

            recordList.Add(record);
        }

        private static void CreateAndAddSqlMetaDataColumn(PropertyInfo propertyInfo,
            string name, ICollection<SqlMetaData> columnlist)
        {                      
            var columnSqlDbType = GetColumnSqlDbTypefromAttributeOrClr(propertyInfo);

            SqlMetaData columnMetaData;
            switch (columnSqlDbType)
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
                    var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
                    int size = (null == sizeAttribute) ? DefaultSizeAttribute : sizeAttribute.Value;
                    columnMetaData = new SqlMetaData(name, columnSqlDbType, size);
                    break;

                case SqlDbType.Decimal:
                    var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
                    Byte precision = (null == precisionAttribute)
                        ? (byte)DefaultDecimalParameterPrecision
                        : precisionAttribute.Value;

                    var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
                    Byte scale = (null == scaleAttribute)
                        ? (byte)DefaultDecimalParameterScale
                        : scaleAttribute.Value;

                    columnMetaData = new SqlMetaData(name, columnSqlDbType, precision, scale);
                    break;

                default:
                    columnMetaData = new SqlMetaData(name, columnSqlDbType);
                    break;
            }

            columnlist.Add(columnMetaData);
        }

        private static string GetNamefromAttributeOrPropertyName(PropertyInfo propertyInfo)
        {
            // Get the propery column name to property name mapping. The default 
            // name is property name, override of parameter name by attribute if available
            NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
            var name = (nameAttribute == null)
                ? propertyInfo.Name
                : nameAttribute.Value;
            return name;
        }

        private static SqlDbType GetColumnSqlDbTypefromAttributeOrClr(PropertyInfo propertyInfo)
        {
            // The default type is the property CLR type, but override if ParameterDbTypeAttribute if available     
            ParameterDbTypeAttribute dbTypeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
            var columnType = (dbTypeAttribute != null)
                ? dbTypeAttribute.Value
                : SqlParameterHelper.GetSqlDbType(propertyInfo.PropertyType);
            return columnType;
        }

        private static Type GetListTypeUnderlyingType(Type listType)
        {
            Type underlyingType = null;
            foreach (Type interfaceTypes in listType.GetInterfaces())
            {
                if (interfaceTypes.IsGenericType && 
                    interfaceTypes.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    underlyingType = interfaceTypes.GetGenericArguments()[0];
                }
            }
            return underlyingType;
        }

        #endregion

        #region Fields

        private const int DefaultDecimalParameterPrecision = 10;
        private const int DefaultDecimalParameterScale = 2;
        private const int DefaultSizeAttribute = 50;

        #endregion
    }
}