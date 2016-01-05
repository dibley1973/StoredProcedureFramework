using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dibware.Helpers.Validation;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.SqlServer.Server;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for building Table Value Paramerters as a an Enumerable
    /// collection of SqlDataRecords from a strongly typed list of items.
    /// </summary>
    internal class TableValuedParameterBuilder
    {
        #region Fields

        private const int DefaultDecimalParameterPrecision = 10;
        private const int DefaultDecimalParameterScale = 2;
        private const int DefaultSizeAttribute = 50;
        private const List<SqlDataRecord> DefaultValueForEmptyList = null;
        private readonly IList _tableValueParameterList;
        private readonly List<SqlMetaData> _columnList;
        private readonly Dictionary<String, String> _mapping;
        private readonly Type _listTypeUnderlyingType;
        private List<SqlDataRecord> _tableValueParameters;
        private PropertyInfo[] _mappedProperties;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TableValuedParameterBuilder"/> 
        /// class with a new list of .
        /// </summary>
        /// <param name="tableValueParameterList">The item list.</param>
        public TableValuedParameterBuilder(IList tableValueParameterList)
        {
            if (tableValueParameterList == null) throw new ArgumentNullException("tableValueParameterList");

            _tableValueParameterList = tableValueParameterList;
            _listTypeUnderlyingType = tableValueParameterList.GetUnderlyingType();
            _columnList = new List<SqlMetaData>();
            _mapping = new Dictionary<string, string>();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds the parameters.
        /// </summary>
        public TableValuedParameterBuilder BuildParameters()
        {
            if (HasEmptyItemList)
            {
                _tableValueParameters = DefaultValueForEmptyList;
            }
            else
            {
                BuildMappedProperties();
                BuildMappingsFromMappedProperties();
                BuildTableValueParameters();
            }
            return this;
        }

        /// <summary>
        /// Gets the table value parameters.
        /// </summary>
        /// <value>
        /// The table value parameters.
        /// </value>
        public IEnumerable<SqlDataRecord> TableValueParameters
        {
            get { return _tableValueParameters; }
        }

        #endregion

        #region Private Members

        private void BuildMappedProperties()
        {
            _mappedProperties = _listTypeUnderlyingType.GetMappedProperties();
        }

        private void BuildMappingsFromMappedProperties()
        {
            ClearMappings();
            ClearColumnList();

            foreach (PropertyInfo propertyInfo in _mappedProperties)
            {
                AddPropertyNameMapping(propertyInfo);
                CreateAndAddSqlMetaDataColumn(propertyInfo);
            }
        }

        private void AddPropertyNameMapping(PropertyInfo propertyInfo)
        {
            var name = propertyInfo.GetNamefromAttributeOrPropertyName();
            _mapping.Add(name, propertyInfo.Name);
        }

        private void BuildTableValueParameters()
        {
            ClearParametersIfNotNullAndNotEmpty();
            InstantiateParametersIfNull();
            CreateAndAddSqlDataRecords();
        }

        private void ClearColumnList()
        {
            _columnList.Clear();
        }

        private void ClearMappings()
        {
            _mapping.Clear();
        }

        private void ClearParametersIfNotNullAndNotEmpty()
        {
            if (_tableValueParameters != null && _tableValueParameters.Count > 0)
            {
                _tableValueParameters.Clear();
            }
        }

        private void CreateAndAddSqlDataRecords()
        {
            foreach (object item in _tableValueParameterList)
            {
                CreateAndAddSqlDataRecord(item);
            }
        }

        private void CreateAndAddSqlMetaDataColumn(PropertyInfo propertyInfo)
        {
            var columnMetaData = CreateColumnMetaData(propertyInfo);
            _columnList.Add(columnMetaData);
        }

        private static SqlMetaData CreateColumnMetaData(PropertyInfo propertyInfo)
        {
            SqlMetaData columnMetaData;
            var columnSqlDbType = propertyInfo.GetColumnSqlDbTypefromAttributeOrClr();
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
                    columnMetaData = CreateTextColumnMetaData(propertyInfo, columnSqlDbType);
                    break;

                case SqlDbType.Decimal:
                    columnMetaData = CreateDecimalColumnMetaData(propertyInfo, columnSqlDbType);
                    break;

                default:
                    columnMetaData = CreateDefaultColumnMetaData(propertyInfo, columnSqlDbType);
                    break;
            }
            return columnMetaData;
        }

        private static SqlMetaData CreateDecimalColumnMetaData(PropertyInfo propertyInfo, SqlDbType columnSqlDbType)
        {
            var name = propertyInfo.GetNamefromAttributeOrPropertyName();
            var precision = GetColumnPrecision(propertyInfo);
            var scale = GetColumnScale(propertyInfo);

            return new SqlMetaData(name, columnSqlDbType, precision, scale);
        }

        private static SqlMetaData CreateDefaultColumnMetaData(PropertyInfo propertyInfo, SqlDbType columnSqlDbType)
        {
            var name = propertyInfo.GetNamefromAttributeOrPropertyName();

            return new SqlMetaData(name, columnSqlDbType);
        }

        private static SqlMetaData CreateTextColumnMetaData(PropertyInfo propertyInfo, SqlDbType columnSqlDbType)
        {
            var name = propertyInfo.GetNamefromAttributeOrPropertyName();
            var size = GetColumnSize(propertyInfo);

            return new SqlMetaData(name, columnSqlDbType, size);
        }

        private void CreateAndAddSqlDataRecord(object item)
        {
            Ensure<object>.IsNotNull(item, "item");

            var record = GetSqlDataRecord(item);

            _tableValueParameters.Add(record);
        }

        private object GetValueOfMappedProperty(object item, int index)
        {
            var valueOfMatchedProperty = _mappedProperties
                .First(propertyInfo => propertyInfo.Name == _mapping[_columnList[index].Name])
                .GetValue(item);
            return valueOfMatchedProperty;
        }

        private static byte GetColumnScale(PropertyInfo propertyInfo)
        {
            var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
            Byte scale = (null == scaleAttribute)
                ? (byte)DefaultDecimalParameterScale
                : scaleAttribute.Value;
            return scale;
        }

        private static byte GetColumnPrecision(PropertyInfo propertyInfo)
        {
            var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
            Byte precision = (null == precisionAttribute)
                ? (byte)DefaultDecimalParameterPrecision
                : precisionAttribute.Value;
            return precision;
        }

        private static int GetColumnSize(PropertyInfo propertyInfo)
        {
            var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
            int size = (null == sizeAttribute) ? DefaultSizeAttribute : sizeAttribute.Value;
            return size;
        }

        private SqlDataRecord GetSqlDataRecord(object item)
        {
            SqlDataRecord record = new SqlDataRecord(_columnList.ToArray());

            for (int index = 0; index < _columnList.Count(); index += 1)
            {
                var valueOfMappedProperty = GetValueOfMappedProperty(item, index);
                record.SetValue(index, valueOfMappedProperty);
            }
            return record;
        }

        private bool HasEmptyItemList
        {
            get { return _tableValueParameterList.Count == 0; }
        }

        private void InstantiateParametersIfNull()
        {
            _tableValueParameters = new List<SqlDataRecord>();
        }

        #endregion
    }
}
