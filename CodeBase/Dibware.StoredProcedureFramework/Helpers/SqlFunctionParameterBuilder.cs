using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for building Sql Parameters from a sql function
    /// </summary>
    public class SqlFunctionSqlParameterBuilder<TResultSetType, TParameterType>
        where TResultSetType : class, new()
        where TParameterType : class
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlFunctionSqlParameterBuilder{TResultSetType, TParameterType}"/> 
        /// class with an object that implements <see cref="ISqlFunction "/>.
        /// </summary>
        /// <param name="sqlFunction">The sql function.</param>
        public SqlFunctionSqlParameterBuilder(ISqlFunction<TResultSetType, TParameterType> sqlFunction)
        {
            if (sqlFunction == null) throw new ArgumentNullException("sqlFunction");

            _sqlFunction = sqlFunction;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds the SQL parameters.
        /// </summary>
        public SqlFunctionSqlParameterBuilder<TResultSetType, TParameterType> BuildSqlParameters()
        {
            var noNeedToBuildSqlParamaters = _sqlFunction.HasNullSqlFunctionParameters;
            if (noNeedToBuildSqlParamaters) return this;

            BuildSqlParametersInternal();
            return this;
        }

        /// <summary>
        /// Gets the collection of SqlParameters once the parameters have been 
        /// built, or null if the sql function has HasNullSqlFunctionParameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ICollection<SqlParameter> SqlParameters { get; private set; }

        #endregion

        #region Private Members

        private void BuildSqlParametersInternal()
        {
            var builder = new SqlParametersFromObjectPropertiesBuilder<TParameterType>(_sqlFunction.Parameters);
            builder.BuildSqlParameters();
            SqlParameters = builder.SqlParameters;
        }

        private readonly ISqlFunction<TResultSetType, TParameterType> _sqlFunction;

        #endregion
    }
}
