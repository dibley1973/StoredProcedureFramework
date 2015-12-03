using Dibware.StoredProcedureFramework.Helpers.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class StoredProcedureDbCommandCreator
        : DbCommandCreatorBase
    {
        #region Constructor

        private StoredProcedureDbCommandCreator(DbConnection connection)
            : base(connection)
        {}

        #endregion

        #region Public Members

        /// <summary>
        /// Builds and sets up the command based upon the settings that have 
        /// been previously passed to this builder.
        /// </summary>
        /// <remarks>
        /// Should call into base implementation before executing any addtional code
        /// </remarks>
        public new void BuildCommand()
        {
            base.BuildCommand();
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
        public static StoredProcedureDbCommandCreator CreateStoredProcedureDbCommandCreator(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            var builder = new StoredProcedureDbCommandCreator(connection)
                .WithCommandText(procedureName)
                .WithCommandType(CommandType.StoredProcedure);

            return builder;
        }

        /// <summary>
        /// Adds a command timeout to the builder which will be passed to the command
        /// when it is construted.
        /// </summary>
        /// <param name="commandTimeout">The value of the command timeout.</param>
        /// <returns></returns>
        public new StoredProcedureDbCommandCreator WithCommandTimeout(int commandTimeout)
        {
            base.WithCommandTimeout(commandTimeout);
            return this;
        }

        /// <summary>
        /// Adds the specified parameters to the builder, and these will be added
        /// to the command when it is built.
        /// </summary>
        /// <param name="parameters">The parameters to add to the command.</param>
        /// <returns></returns>
        public new StoredProcedureDbCommandCreator WithParameters(IEnumerable<SqlParameter> parameters)
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
        public new StoredProcedureDbCommandCreator WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }
        
        #endregion

        #region Private Members

        private new StoredProcedureDbCommandCreator WithCommandText(string commandText)
        {
            base.WithCommandText(commandText);
            return this;
        }

        private new StoredProcedureDbCommandCreator WithCommandType(CommandType commandType)
        {
            base.WithCommandType(commandType);
            return this;
        }

        #endregion
    }
}