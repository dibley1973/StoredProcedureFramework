using Dibware.StoredProcedureFramework.Helpers.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class SqlScalarFunctionDbCommandCreator
        : SqlFunctionDbCommandCreatorBase 
    {
        #region Constructor

        private SqlScalarFunctionDbCommandCreator(IDbConnection connection)
            : base(connection)
        { }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds and sets up the command based upon the settings that have 
        /// been previously passed to this builder.
        /// </summary>
        /// <remarks>
        /// Should call into base implementation before executing any addtional code
        /// </remarks>
        public new SqlScalarFunctionDbCommandCreator BuildCommand()
        {
            base.BuildCommand();
            return this;
        }

        #endregion

        #region Public Factory Methods

        /// <summary>
        /// Creates the stored procedure database command creator.
        /// </summary>
        /// <param name="connection">
        /// The connection to be passed to the command when it is constructed.
        /// </param>
        /// <param name="procedureName">
        /// The name of the stored procedure for which the commmand is to call.
        /// </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public static SqlScalarFunctionDbCommandCreator CreateSqlFunctionDbCommandCreator(
            IDbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            var builder = new SqlScalarFunctionDbCommandCreator(connection)
                .WithCommandText(procedureName)
                .WithCommandType(CommandType.Text);

            return builder;
        }

        /// <summary>
        /// Adds a command timeout to the builder which will be passed to the command
        /// when it is construted.
        /// </summary>
        /// <param name="commandTimeout">The value of the command timeout.</param>
        /// <returns></returns>
        public SqlScalarFunctionDbCommandCreator WithCommandTimeout(int? commandTimeout)
        {
            if (!commandTimeout.HasValue) throw new ArgumentNullException("commandTimeout");

            base.WithCommandTimeout(commandTimeout.Value);
            return this;
        }

        /// <summary>
        /// Adds the specified parameters to the builder, and these will be added
        /// to the command when it is built.
        /// </summary>
        /// <param name="parameters">The parameters to add to the command.</param>
        /// <returns></returns>
        public new SqlScalarFunctionDbCommandCreator WithParameters(IEnumerable<SqlParameter> parameters)
        {
            base.WithParameters(parameters);
            return this;
        }

        /// <summary>
        /// Adds the specified transaction to the builder, and these will be added
        /// to the command when it is built.
        /// </summary>
        /// <param name="transaction">The transaction to add to teh command.</param>
        /// <returns></returns>
        public new SqlScalarFunctionDbCommandCreator WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }

        #endregion

        #region Private Members

        protected override string FunctionCommandTextFormat
        {
            get { return "SELECT {0} ({1})"; }
        }

        private new SqlScalarFunctionDbCommandCreator WithCommandText(string commandText)
        {
            base.WithCommandText(commandText);
            return this;
        }

        private new SqlScalarFunctionDbCommandCreator WithCommandType(CommandType commandType)
        {
            base.WithCommandType(commandType);
            return this;
        }

        #endregion
    }
}
