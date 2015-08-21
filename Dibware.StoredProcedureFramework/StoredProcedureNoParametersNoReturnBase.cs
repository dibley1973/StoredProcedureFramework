
namespace Dibware.StoredProcedureFramework
{
    public abstract class StoredProcedureNoParametersNoReturnBase
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected StoredProcedureNoParametersNoReturnBase()
            : base(new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureNoParametersNoReturnBase"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        protected StoredProcedureNoParametersNoReturnBase(string procedureName)
            : base(procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureNoParametersNoReturnBase"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected StoredProcedureNoParametersNoReturnBase(string schemaName, string procedureName)
            : base(schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}