using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for processing outout parameter values
    /// </summary>
    internal class OutputParameterValueProcessor<TReturnType, TParameterType>
        where TReturnType : class, new()
        where TParameterType : class
    {
        #region Fields

        private readonly IEnumerable<SqlParameter> _sqlParameters;
        private IEnumerable<SqlParameter> _outputSqlParameters;
        private readonly IStoredProcedure<TReturnType, TParameterType> _storedProcedure;
        private TParameterType _storedProcedureParameters;
        private PropertyInfo[] _mappedProperties;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputParameterValueProcessor{TReturnType, TParameterType}"/> class.
        /// </summary>
        /// <param name="sqlParameters">The SQL parameters.</param>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <exception cref="System.ArgumentNullException">
        /// sqlParameters
        /// or
        /// storedProcedure
        /// </exception>
        public OutputParameterValueProcessor(IEnumerable<SqlParameter> sqlParameters, IStoredProcedure<TReturnType, TParameterType> storedProcedure)
        {
            if (storedProcedure == null) throw new ArgumentNullException("storedProcedure");

            _sqlParameters = sqlParameters;
            _storedProcedure = storedProcedure;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Providing there are output parameters to process, processses them.
        /// </summary>
        public void Processs()
        {
            if (!HasParametersToProcess) return;

            IdentifyOutputParameters();
            if (!HasOutputParametersToProcess) return;

            BuildMappedPropertiesFromParameterType();
            SetStoredProcedureParameters();
            SetOutputParameterValuesFromOutputSqlParameters();
        }

        #endregion

        #region Private Members

        private void BuildMappedPropertiesFromParameterType()
        {
            _mappedProperties = typeof(TParameterType).GetMappedProperties();
        }

        private bool HasParametersToProcess
        {
            get { return _sqlParameters != null && _sqlParameters.Any(); }
        }

        private bool HasOutputParametersToProcess
        {
            get { return _outputSqlParameters != null && _outputSqlParameters.Any(); }
        }

        private void IdentifyOutputParameters()
        {
            _outputSqlParameters = _sqlParameters
                .Where(sqlParameter => sqlParameter.Direction == ParameterDirection.InputOutput
                    || sqlParameter.Direction == ParameterDirection.Output)
                .Select(sqlParameter => sqlParameter);
        }

        private void SetStoredProcedureParameters()
        {
            _storedProcedureParameters = _storedProcedure.Parameters;
        }

        private void SetOutputParameterValuesFromOutputSqlParameters()
        {
            foreach (SqlParameter outputParameter in _outputSqlParameters)
            {
                SetOutputParameterValueFromOutputSqlParameter(outputParameter);
            }
        }

        private void SetOutputParameterValueFromOutputSqlParameter(SqlParameter outputParameter)
        {
            String propertyName = outputParameter.ParameterName;
            PropertyInfo matchedMappedProperty = _mappedProperties.FirstOrDefault(
                property => property.Name == propertyName);

            if (matchedMappedProperty != null)
            {
                matchedMappedProperty.SetValue(_storedProcedureParameters, outputParameter.Value, null);
            }
        }

        #endregion
    }
}