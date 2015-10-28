using Dibware.StoredProcedureFramework;
using Dibware.StoredProcedureFramework.Base;
using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    /// <summary>
    /// Represents the base class that a stored procedures without a return
    /// type should inherit from if used in conjunction with Entity Framework.
    /// </summary>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class NoReturnTypeStoredProcedureBaseForEf<TParameters>
        : StoredProcedureBaseForEf<NullStoredProcedureResult, TParameters>
        where TParameters : class
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoReturnTypeStoredProcedureBaseForEff{TParameters}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="parameters">The parameters.</param>
        protected NoReturnTypeStoredProcedureBaseForEf(DbContext context,
            TParameters parameters)
            : base(context, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoReturnTypeStoredProcedureBaseForEff{TParameters}" />
        /// class with parameters and stored procedure name
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected NoReturnTypeStoredProcedureBaseForEf(DbContext context,
            string procedureName,
            TParameters parameters)
            : base(context, procedureName, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBase{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected NoReturnTypeStoredProcedureBaseForEf(DbContext context,
            string schemaName,
            string procedureName,
            TParameters parameters)
            : base(context, schemaName, procedureName, parameters)
        {
        }

        #endregion
    }
}