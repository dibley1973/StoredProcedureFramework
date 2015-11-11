namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that a stored procedures without a return
    /// type should inherit from.
    /// </summary>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class NoReturnTypeStoredProcedureBase<TParameters>
        : StoredProcedureBase<NullStoredProcedureResult, TParameters>
        where TParameters : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoReturnTypeStoredProcedureBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected NoReturnTypeStoredProcedureBase(TParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoReturnTypeStoredProcedureBase{TReturn}"/> 
        /// class with parameters and stored procedure name
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected NoReturnTypeStoredProcedureBase(string procedureName,
            TParameters parameters)
            : base(procedureName, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected NoReturnTypeStoredProcedureBase(string schemaName,
            string procedureName,
            TParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
        }

        #endregion
    }
}