
namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    /// <summary>
    /// Represents the most basic of stored procedures. This stored
    /// procedure does not have any parameters, neither does it return
    /// any result, it just performs an action.
    /// </summary>
    internal class MostBasicStoredProcedure
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MostBasicStoredProcedure"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public MostBasicStoredProcedure(NullStoredProcedureParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MostBasicStoredProcedure"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public MostBasicStoredProcedure(string procedureName, NullStoredProcedureParameters parameters)
            : base(procedureName, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MostBasicStoredProcedure"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public MostBasicStoredProcedure(string schemaName, string procedureName, NullStoredProcedureParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
        }

        #endregion
    }
}