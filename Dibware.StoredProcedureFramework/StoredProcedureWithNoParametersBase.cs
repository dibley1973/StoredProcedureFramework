namespace Dibware.StoredProcedureFramework
{
    /// <summary>
    /// Represents the base class that all stored procedures without parameters
    /// should inherit from.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    public abstract class StoredProcedureWithNoParametersBase<TReturn>
        : StoredProcedureBase<TReturn, NullStoredProcedureParameters>
        where TReturn : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected StoredProcedureWithNoParametersBase()
            : base(new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        protected StoredProcedureWithNoParametersBase(string procedureName)
            : base(procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersBase{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected StoredProcedureWithNoParametersBase(string schemaName, string procedureName)
            : base(schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}