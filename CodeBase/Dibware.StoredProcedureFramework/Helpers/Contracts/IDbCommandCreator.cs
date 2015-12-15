using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers.Contracts
{
    /// <summary>
    /// Defines the expected members of a DbCommand creator
    /// </summary>
    internal interface IDbCommandCreator
    {
        IDbCommand Command { get; }
        IDbCommandCreator BuildCommand();
        void WithCommandTimeout(int commandTimeout);
        void WithParameters(IEnumerable<SqlParameter> parameters);
        void WithTransaction(SqlTransaction transaction);
    }
}