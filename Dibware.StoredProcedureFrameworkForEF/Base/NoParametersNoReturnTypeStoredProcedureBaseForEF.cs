using Dibware.StoredProcedureFramework;
using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    /// <summary>
    /// Represents the base class that a stored procedures without parameters
    /// or a return type should inherit from if used in conjunction with Entity Framework.
    /// </summary>
    public abstract class NoParametersNoReturnTypeStoredProcedureBaseForEF
        : StoredProcedureBaseForEF<NullStoredProcedureResult, NullStoredProcedureParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersNoReturnTypeStoredProcedureBaseForEF{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        protected NoParametersNoReturnTypeStoredProcedureBaseForEF(DbContext context)
            : base(context, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersNoReturnTypeStoredProcedureBaseForEF"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersNoReturnTypeStoredProcedureBaseForEF(DbContext context,
            string procedureName)
            : base(context, procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersNoReturnTypeStoredProcedureBaseForEF"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersNoReturnTypeStoredProcedureBaseForEF(DbContext context,
            string schemaName, string procedureName)
            : base(context, schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}
