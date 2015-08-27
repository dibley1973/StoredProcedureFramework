namespace Dibware.StoredProcedureFramework
{
    /// <summary>
    /// Represents the base class that a stored procedures without parameters
    /// should inherit from.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    public abstract class NoParametersStoredProcedureBase<TReturn>
        : StoredProcedureBase<TReturn, NullStoredProcedureParameters>
        where TReturn : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected NoParametersStoredProcedureBase()
            : base(new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected NoParametersStoredProcedureBase(string procedureName)
            : base(procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersStoredProcedureBase(string schemaName, string procedureName)
            : base(schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}