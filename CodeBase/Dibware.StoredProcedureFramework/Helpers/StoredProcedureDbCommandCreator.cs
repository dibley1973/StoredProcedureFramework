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
        /// Builds the command.
        /// </summary>
        /// <remarks>
        /// Calls into base implementation before
        /// </remarks>
        public new void BuildCommand()
        {
            base.BuildCommand();
            LoadCommandParametersIfAnyExist();
        }

        #endregion

        #region Public Factory Methods

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

        #endregion

        #region Private Members

        private new StoredProcedureDbCommandCreator WithCommandText(string commandText)
        {
            base.WithCommandText(commandText);
            return this;
        }

        public new StoredProcedureDbCommandCreator WithCommandTimeout(int value)
        {
            base.WithCommandTimeout(value);
            return this;
        }

        private new StoredProcedureDbCommandCreator WithCommandType(CommandType commandType)
        {
            base.WithCommandType(commandType);
            return this;
        }

        public new StoredProcedureDbCommandCreator WithParameters(IEnumerable<SqlParameter> parameters)
        {
            base.WithParameters(parameters);
            return this;
        }

        public new StoredProcedureDbCommandCreator WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }
        
        #endregion
    }
}