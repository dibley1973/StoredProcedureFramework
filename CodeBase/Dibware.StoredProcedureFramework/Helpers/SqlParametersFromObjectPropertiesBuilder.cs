using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /* ***** WORK IN PROGRESS ***** */

    /// <summary>
    /// Responsible for building SqlParameters from the properties of an object
    /// </summary>
    /// <typeparam name="TSourceType">
    /// The type of the source object to build parameters from
    /// </typeparam>
    /// <remarks>
    /// This class is NOT threadsafe
    /// </remarks>
    public class SqlParametersFromObjectPropertiesBuilder<TSourceType>
        where TSourceType : class
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParametersFromObjectPropertiesBuilder{TSourceType}"/> class.
        /// </summary>
        /// <param name="source">The source object to create parameters from.</param>
        /// <exception cref="System.ArgumentNullException">source</exception>
        public SqlParametersFromObjectPropertiesBuilder(TSourceType source)
        {
            if (source == null) throw new ArgumentNullException("source");

            _source = source;
            InstantiateParameters();
        }
        #endregion

        #region Public Members

        /// <summary>
        /// Builds the SQL parameters.
        /// </summary>
        public void BuildSqlParameters()
        {
            if (Source == null) return;

            BuildMappedPropertiesFromSource();
            CreateSqlParametersFromMappedProperties();
            PopulateSqlParametersFromProperties();
        }

        /// <summary>
        /// Gets the collection of SqlParameters once the parameters have been built.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ICollection<SqlParameter> SqlParameters { get; private set; }

        #endregion

        #region Private Members

        private void BuildMappedPropertiesFromSource()
        {
            _mappedProperties = Source.GetType().GetMappedProperties();
        }

        private void ClearSqlParameters()
        {
            SqlParameters.Clear();
        }

        private void CreateSqlParametersFromMappedProperties()
        {
            ClearSqlParameters();

            foreach (PropertyInfo propertyInfo in _mappedProperties)
            {
                SqlParameter sqlParameter = new SqlParameter();

                NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
                sqlParameter.ParameterName = nameAttribute != null
                    ? nameAttribute.Value
                    : propertyInfo.Name;

                var typeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
                sqlParameter.SqlDbType = typeAttribute != null
                    ? typeAttribute.Value
                    : SqlParameterHelper.GetSqlDbType(propertyInfo.PropertyType);

                TrySetSqlParameterDirectionFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterSizeFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterPrecisionFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterScaleFromAttribute(propertyInfo, sqlParameter);

                SqlParameters.Add(sqlParameter);
            }
        }

        private object GetPropertyValueFromParameters(PropertyInfo matchedProperty)
        {
            return matchedProperty.GetValue(Source);
        }

        private void InstantiateParameters()
        {
            SqlParameters = new List<SqlParameter>();
        }


        private void PopulateSqlParametersFromProperties()
        {
            var allInputDirectionParameters = SqlParameters
                .Where(parameter => parameter.Direction == ParameterDirection.Input)
                .Select(parameter => parameter);

            foreach (SqlParameter sqlParameter in allInputDirectionParameters)
            {
                PopulateSqlParameterValueFromProperty(sqlParameter);
            }
        }

        private void PopulateSqlParameterValueFromProperty(SqlParameter sqlParameter)
        {
            String slqParameterName = sqlParameter.ParameterName;
            PropertyInfo matchedProperty = _mappedProperties.FirstOrDefault(property => property.Name == slqParameterName);

            // TODO: investigate if this can ever actually happen!
            //if (matchedProperty == null) throw new NullReferenceException(
            //    string.Format(
            //        ExceptionMessages.NoMappedPropertyFoundForName,
            //        slqParameterName));

            object propertyValue = GetPropertyValueFromParameters(matchedProperty);
            ValidateValueIsInRangeForSqlParameter(sqlParameter, propertyValue);
            SetSqlParameterValue(propertyValue, sqlParameter);
        }

        private void SetSqlParameterValue(object value, SqlParameter sqlParameter)
        {
            bool parameterValueIsNull = (value == null);
            bool parameterValueIsTableValuedParameter = (sqlParameter.SqlDbType == SqlDbType.Structured);

            if (parameterValueIsNull)
            {
                sqlParameter.Value = DBNull.Value;
            }
            else if (!parameterValueIsTableValuedParameter)
            {
                sqlParameter.Value = value;
            }
            else
            {
                sqlParameter.Value = TableValuedParameterHelper.GetTableValuedParameterFromList((IList)value);
            }
        }

        private static void TrySetSqlParameterDirectionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var directionAttribute = propertyInfo.GetAttribute<DirectionAttribute>();
            if (null != directionAttribute) sqlParameter.Direction = directionAttribute.Value;

            // TODO: investigate if default direction needs to be set.
            // default appears to be input any way
            // sqlParameter.Direction = DefaultParameterDirection;
        }

        private void TrySetSqlParameterPrecisionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
            if (null != precisionAttribute) sqlParameter.Precision = precisionAttribute.Value;
        }

        private void TrySetSqlParameterScaleFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
            if (null != scaleAttribute) sqlParameter.Scale = scaleAttribute.Value;
        }

        private void TrySetSqlParameterSizeFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            // TODO: DW-2015-11-18 - Investigate a better solution than the default 
            // size for string parameters when no SizeAttribute has been set. 
            // Previously the framework used to default to ZERO, but this is not 
            // very useful to most callers.
            var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
            sqlParameter.Size = sizeAttribute != null
                ? sizeAttribute.Value
                : DefaultStringSize;
        }

        private void ValidateDecimal(SqlParameter sqlParameter, object value)
        {
            if (value is decimal)
            {
                var validator = new SqlParameterDecimalValueValidator();
                validator.Validate((decimal)value, sqlParameter);
            }
            else
            {
                throw new SqlParameterInvalidDataTypeException(
                    sqlParameter.ParameterName,
                    typeof(decimal), value.GetType());
            }
        }

        private void ValidateString(SqlParameter sqlParameter, object value)
        {
            if (value is string)
            {
                var validator = new SqlParameterStringValueValidator();
                validator.Validate((string)value, sqlParameter);
            }
            else if (value is char[])
            {
                // ... and validate it if it is
                var validator = new SqlParameterStringValueValidator();
                validator.Validate(new string((char[])value), sqlParameter);
            }
            else if (value == null)
            {
                bool parameterIsStringOrNullable = sqlParameter.IsStringOrIsNullable();
                if (parameterIsStringOrNullable) return;

                string message = string.Format(
                    "'{0}' parameter had a null value",
                    sqlParameter.ParameterName);
                throw new SqlNullValueException(message);
            }
            else
            {
                throw new SqlParameterInvalidDataTypeException(
                    sqlParameter.ParameterName,
                    typeof(string), value.GetType());
            }
        }

        private void ValidateValueIsInRangeForSqlParameter(SqlParameter sqlParameter, object value)
        {
            if (sqlParameter.RequiresPrecisionAndScaleValidation()) ValidateDecimal(sqlParameter, value);

            if (sqlParameter.RequiresLengthValidation()) ValidateString(sqlParameter, value);
        }

        public TSourceType Source { get { return _source; } }
        private PropertyInfo[] _mappedProperties;
        private readonly TSourceType _source;
        private const int DefaultStringSize = 8000;

        #endregion
    }
}
