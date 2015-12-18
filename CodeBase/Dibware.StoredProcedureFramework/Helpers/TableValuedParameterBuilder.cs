using Dibware.Helpers.Validation;
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
    /// Responsible for building Table Value Paramerters as a an Enumerable
    /// collection of SqlDataRecords from a strongly typed list of items.
    /// </summary>
    public class TableValuedParameterBuilder
    {
        #region Fields

        private const int DefaultDecimalParameterPrecision = 10;
        private const int DefaultDecimalParameterScale = 2;
        private const int DefaultSizeAttribute = 50;
        private const List<SqlDataRecord> DefaultValueForEmptyList = null;
        private readonly IList _itemList;
        private List<SqlDataRecord> _tableValueParameters;
        private List<SqlMetaData> _columnList = new List<SqlMetaData>();
        private Dictionary<String, String> _mapping = new Dictionary<string, string>();
        private readonly Type _listTypeUnderlyingType;
        private PropertyInfo[] _mappedProperties;
        private readonly bool _hasEmptyItemList;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TableValuedParameterBuilder"/> 
        /// class with a new list of .
        /// </summary>
        /// <param name="itemList">The item list.</param>
        public TableValuedParameterBuilder(IList itemList)
        {
            if (itemList == null) throw new ArgumentNullException("itemList");

            _itemList = itemList;
            _listTypeUnderlyingType = GetListTypeUnderlyingType(itemList.GetType());
            _hasEmptyItemList = _itemList.Count == 0;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds the parameters.
        /// </summary>
        public TableValuedParameterBuilder BuildParameters()
        {
            if (_hasEmptyItemList)
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
                var name = GetNamefromAttributeOrPropertyName(propertyInfo);
                _mapping.Add(name, propertyInfo.Name);
                CreateAndAddSqlMetaDataColumn(propertyInfo, name);
            }
        }

        private void BuildTableValueParameters()
        {
            ClearParametersIfNotNullAndNotEmpty();
            InstantiateParametersIfNull();

            foreach (object item in _itemList)
            {
                Ensure<object>.IsNotNull(item, "item");

                CreateAndAddSqlDataRecord(item);
            }
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

        private void CreateAndAddSqlMetaDataColumn(PropertyInfo propertyInfo,
            string name)
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

            _columnList.Add(columnMetaData);
        }

        private void CreateAndAddSqlDataRecord(object item)
        {
            if (item == null) throw new ArgumentNullException("item");

            SqlDataRecord record = new SqlDataRecord(_columnList.ToArray());

            for (int index = 0; index < _columnList.Count(); index += 1)
            {
                var valueOfMatchedProperty = _mappedProperties
                    .First(propertyInfo => propertyInfo.Name == _mapping[_columnList[index].Name])
                    .GetValue(item);

                record.SetValue(index, valueOfMatchedProperty);
            }

            _tableValueParameters.Add(record);
        }

        // TODO: Consider moving this to PropertyInfoExtensions
        private static SqlDbType GetColumnSqlDbTypefromAttributeOrClr(PropertyInfo propertyInfo)
        {
            // The default type is the property CLR type, but override if ParameterDbTypeAttribute if available     
            ParameterDbTypeAttribute dbTypeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
            SqlDbType columnType = (dbTypeAttribute != null)
                ? dbTypeAttribute.Value
                : ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(propertyInfo.PropertyType);

            return columnType;
        }

        // TODO: Consider moving this to PropertyInfoExtensions
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

        // TODO consider moving to TypeExtensions
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

        private void InstantiateParametersIfNull()
        {
            _tableValueParameters = new List<SqlDataRecord>();
        }

        #endregion
    }
}
