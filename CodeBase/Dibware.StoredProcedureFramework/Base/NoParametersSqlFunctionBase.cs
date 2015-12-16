namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that a sql functions without parameters
    /// should inherit from.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    public abstract class NoParametersSqlFunctionBase<TReturn>
        : SqlFunctionBase<TReturn, NullSqlFunctionParameters>
        where TReturn : new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersSqlFunctionBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a sql function.
        /// </summary>
        protected NoParametersSqlFunctionBase()
            : base(new NullSqlFunctionParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersSqlFunctionBase{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a sql function.
        /// </summary>
        /// <param name="functionName">Name of the procedure.</param>
        protected NoParametersSqlFunctionBase(string functionName)
            : base(functionName, new NullSqlFunctionParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersSqlFunctionBase{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a sql function.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="functionName">Name of the procedure.</param>
        protected NoParametersSqlFunctionBase(string schemaName, string functionName)
            : base(schemaName, functionName, new NullSqlFunctionParameters())
        {
        }

        #endregion
    }
}