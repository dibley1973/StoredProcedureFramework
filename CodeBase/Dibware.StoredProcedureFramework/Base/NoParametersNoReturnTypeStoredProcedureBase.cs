namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that a stored procedures without parameters
    /// or a return type should inherit from.
    /// </summary>
    public abstract class NoParametersNoReturnTypeStoredProcedureBase
        : StoredProcedureBase<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected NoParametersNoReturnTypeStoredProcedureBase()
            : base(new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersNoReturnTypeStoredProcedureBase"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersNoReturnTypeStoredProcedureBase(string procedureName)
            : base(procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersNoReturnTypeStoredProcedureBase"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersNoReturnTypeStoredProcedureBase(string schemaName, string procedureName)
            : base(schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}