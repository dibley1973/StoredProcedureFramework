using Dibware.StoredProcedureFramework.Helpers.Base;
using System;
using System.Data.Common;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class StoredProcedureDbCommandCreator
        : DbCommandCreatorBase
    {
        #region Constructor

        protected StoredProcedureDbCommandCreator(DbConnection connection)
            : base(connection)
        {

        }


        #endregion

        #region Public Members

        public override void BuildCommand()
        {

        }

        #endregion

        #region Public Factory Methods

        public static StoredProcedureDbCommandCreator CreateDbCommandCreator(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            var builder = new StoredProcedureDbCommandCreator(connection)
                .WithProcedureName(procedureName);

            return builder;
        }

        public static StoredProcedureDbCommandCreator CreateDbCommandCreatorWithCommandTimeout(
            DbConnection connection,
            string procedureName,
            int commandTimeout)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            var builder = new StoredProcedureDbCommandCreator(connection)
                .WithProcedureName(procedureName)
                .WithCommandTimeout(commandTimeout);

            return builder;
        }


        #endregion

        #region Private Members

        private StoredProcedureDbCommandCreator WithProcedureName(string value)
        {
            _procedureName = value;
            return this;
        }

        private StoredProcedureDbCommandCreator WithCommandTimeout(int value)
        {
            _commandTimeout = value;
            return this;
        }


        private string _procedureName;

        #endregion
    }
}