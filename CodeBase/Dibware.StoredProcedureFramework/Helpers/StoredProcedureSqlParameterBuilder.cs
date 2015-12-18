using Dibware.StoredProcedureFramework.Contracts;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for building Sql Parameters from a stored procedure
    /// </summary>
    internal class StoredProcedureSqlParameterBuilder<TResultSetType, TParameterType>
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
        public StoredProcedureSqlParameterBuilder<TResultSetType, TParameterType> BuildSqlParameters()
        {
            var noNeedToBuildSqlParamaters = _storedProcedure.HasNullStoredProcedureParameters;
            if (noNeedToBuildSqlParamaters) return this;

            BuildSqlParametersInternal();
            return this;
        }

        /// <summary>
        /// Gets the collection of SqlParameters once the parameters have been 
        /// built, or null if the stored procedure has HasNullStoredProcedureParameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ICollection<SqlParameter> SqlParameters { get; private set; }

        #endregion

        #region Private Members

        private void BuildSqlParametersInternal()
        {
            var builder = new SqlParametersFromObjectPropertiesBuilder<TParameterType>(_storedProcedure.Parameters);
            builder.BuildSqlParameters();
            SqlParameters = builder.SqlParameters;
        }

        private readonly IStoredProcedure<TResultSetType, TParameterType> _storedProcedure;

        #endregion
    }
}