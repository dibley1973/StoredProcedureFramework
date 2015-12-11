using Dibware.StoredProcedureFramework.Helpers.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class UserDefinedFunctionDbCommandCreator
        : DbCommandCreatorBase
    {
        #region Fields

        public const string CommandTextFormat = "SELECT {0}();";

        #endregion

        #region Constructor

        private UserDefinedFunctionDbCommandCreator(IDbConnection connection)
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
        public new UserDefinedFunctionDbCommandCreator BuildCommand()
        {
            base.BuildCommand();
            return this;
        }

        #endregion

        #region Public Factory Methods

        /// <summary>
        /// Creates the user defined function database command creator.
        /// </summary>
        /// <param name="connection">
        /// The connection to be passed to the command when it is constructed.
        /// </param>
        /// <param name="functionName">
        /// The name of the user defined function for which the commmand is to call.
        /// </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public static UserDefinedFunctionDbCommandCreator CreateUserDefinedFunctionDbCommandCreator(
            IDbConnection connection,
            string functionName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(functionName)) throw new ArgumentNullException("functionName");

            var builder = new UserDefinedFunctionDbCommandCreator(connection)
                .WithFunctionName(functionName)
                .WithCommandType(CommandType.Text);

            return builder;
        }

        /// <summary>
        /// Adds a command timeout to the builder which will be passed to the command
        /// when it is construted.
        /// </summary>
        /// <param name="commandTimeout">The value of the command timeout.</param>
        /// <returns></returns>
        public UserDefinedFunctionDbCommandCreator WithCommandTimeout(int? commandTimeout)
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
        public new UserDefinedFunctionDbCommandCreator WithParameters(IEnumerable<SqlParameter> parameters)
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
        public new UserDefinedFunctionDbCommandCreator WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }

        #endregion

        #region Private Members

        private new UserDefinedFunctionDbCommandCreator WithCommandType(CommandType commandType)
        {
            base.WithCommandType(commandType);
            return this;
        }

        private UserDefinedFunctionDbCommandCreator WithFunctionName(string functionName)
        {
            WithCommandText(GetCommandText(functionName));
            return this;
        }

        private string GetCommandText(string functionName)
        {
            var commandText = String.Format(CommandTextFormat, functionName);
            return commandText;
        }


        #endregion
    }
}
