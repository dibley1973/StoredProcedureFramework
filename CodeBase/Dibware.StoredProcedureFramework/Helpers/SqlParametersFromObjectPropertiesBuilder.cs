using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Validators;

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
            BuildMappedPropertiesFromSource();
            
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds the SQL parameters.
        /// </summary>
        public void BuildSqlParameters()
        {
            if (_source == null) return;

            var sqlParameters = SqlParameterHelper.CreateSqlParametersFromPropertyInfoArray(_mappedProperties);
            PopulateSqlParametersFromProperties(sqlParameters);

            Parameters = sqlParameters;
        }

        /// <summary>
        /// Gets the collection of SqlParameters once the parameters have been built.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ICollection<SqlParameter> Parameters { get; private set; }

        #endregion

        #region Private Members

        private void BuildMappedPropertiesFromSource()
        {
            _mappedProperties = _source.GetType().GetMappedProperties();
        }

        private static object GetPropertyValueFromParameters(TSourceType source, PropertyInfo matchedProperty)
        {
            return matchedProperty.GetValue(source);
        }

        private void PopulateSqlParametersFromProperties(
            ICollection<SqlParameter> sqlParameters)
        {
            var allInputDirectionParameters = sqlParameters
                .Where(parameter => parameter.Direction == ParameterDirection.Input)
                .Select(parameter => parameter);

            foreach (SqlParameter sqlParameter in allInputDirectionParameters)
            {
                PopulateSqlParameterValueFromProperty(_mappedProperties, _source, sqlParameter);
            }
        }

        private void PopulateSqlParameterValueFromProperty(PropertyInfo[] properties,
            TSourceType parameters,
            SqlParameter sqlParameter)
        {
            String slqParameterName = sqlParameter.ParameterName;
            PropertyInfo matchedProperty = properties.FirstOrDefault(property => property.Name == slqParameterName);
            if (matchedProperty == null) throw new NullReferenceException(
                string.Format(
                    ExceptionMessages.NoMappedPropertyFoundForName,
                    slqParameterName));

            object propertyValue = GetPropertyValueFromParameters(parameters, matchedProperty);
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

        private static void ValidateDecimal(SqlParameter sqlParameter, object value)
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

        private static void ValidateString(SqlParameter sqlParameter, object value)
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

        private readonly TSourceType _source;
        private PropertyInfo[] _mappedProperties;

        #endregion
    }
}
