﻿using Dibware.StoredProcedureFramework;
using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    /// <summary>
    /// Represents the base class that a stored procedures without parameters
    /// should inherit from if used in conjunction with Entity Framework.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    public abstract class NoParametersStoredProcedureBaseForEF<TReturn>
        : StoredProcedureBaseForEF<TReturn, NullStoredProcedureParameters>
        where TReturn : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBaseForEF{TReturn}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        protected NoParametersStoredProcedureBaseForEF(DbContext context)
            : base(context, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBaseForEF{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersStoredProcedureBaseForEF(DbContext context,
            string procedureName)
            : base(context, procedureName, new NullStoredProcedureParameters())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoParametersStoredProcedureBaseForEF{TReturn}" />
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        protected NoParametersStoredProcedureBaseForEF(DbContext context,
            string schemaName, string procedureName)
            : base(context, schemaName, procedureName, new NullStoredProcedureParameters())
        {
        }

        #endregion
    }
}