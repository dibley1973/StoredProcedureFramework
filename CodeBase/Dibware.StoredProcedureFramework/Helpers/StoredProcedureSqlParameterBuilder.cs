using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.Validators;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for building Sql Parameters
    /// </summary>
    public class StoredProcedureSqlParameterBuilder<TResultSetType, TParameterType>
        where TResultSetType : class, new()
        where TParameterType : class
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureSqlParameterBuilder{TResultSetType, TParameterType}"/> 
        /// class with an object that implements <see cref="IStoredProcedure{TResultSetType, TParameterType} "/>.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        public StoredProcedureSqlParameterBuilder(IStoredProcedure<TResultSetType, TParameterType> storedProcedure)
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            _storedProcedure = storedProcedure;
        }
        
        #endregion

        #region Public Members

        /// <summary>
        /// Builds the SQL parameters.
        /// </summary>
        public void BuildSqlParameters()
        {
            if(_storedProcedure.HasNullStoredProcedureParameters) return;

            var mappedProperties = _storedProcedure.Parameters.GetType().GetMappedProperties();
            var sqlParameters = SqlParameterHelper.CreateSqlParametersFromPropertyInfoArray(mappedProperties);
            
            PopulateSqlParametersFromProperties(sqlParameters, mappedProperties, _storedProcedure.Parameters);

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

        private static object GetPropertyValueFromParameters(TParameterType parameters, PropertyInfo matchedProperty)
        {
            return matchedProperty.GetValue(parameters);
        }

        private void PopulateSqlParametersFromProperties(
            ICollection<SqlParameter> sqlParameters, 
            PropertyInfo[] properties, 
            TParameterType parameters)
        {
            // Get all input type parameters
            var parametersOfInputDirectionOnly = sqlParameters
                .Where(parameter => parameter.Direction == ParameterDirection.Input)
                .Select(parameter => parameter);

            foreach (SqlParameter sqlParameter in parametersOfInputDirectionOnly)
            {
                PopulateSqlParameterValueFromProperty(properties, parameters, sqlParameter);
            }
        }

        private void PopulateSqlParameterValueFromProperty(PropertyInfo[] properties, 
            TParameterType parameters,
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


        //private void PopulateSqlParametersFromProperties(
        //    ICollection<SqlParameter> sqlParameters,
        //    PropertyInfo[] properties,
        //    IStoredProcedure<TResultSetType, TParameterType> procedure)
        //{
        //    // Get all input type parameters
        //    foreach (SqlParameter sqlParameter in sqlParameters
        //        .Where(p => p.Direction == ParameterDirection.Input)
        //        .Select(p => p))
        //    {
        //        String propertyName = sqlParameter.ParameterName;
        //        PropertyInfo mappedPropertyInfo = properties.FirstOrDefault(p => p.Name == propertyName);
        //        if (mappedPropertyInfo == null) throw new NullReferenceException("Mapped property not found");

        //        // Use the PropertyInfo to get the value from the parameters,
        //        // then validate the value and if validation passes, set it 
        //        object value = mappedPropertyInfo.GetValue(procedure.Parameters);
        //        ValidateValueIsInRangeForSqlParameter(sqlParameter, value);
        //        SetSqlParameterValue(value, sqlParameter);
        //    }
        //}

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

        private readonly IStoredProcedure<TResultSetType, TParameterType> _storedProcedure;

        #endregion
    }
}