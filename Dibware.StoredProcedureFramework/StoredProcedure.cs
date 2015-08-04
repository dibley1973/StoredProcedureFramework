using System;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework
{
    /// <summary>
    /// Represents a Stored Procedure in the database.
    /// </summary>
    public class StoredProcedure
    {
        #region Fields

        /// <summary>
        /// The default schemaName
        /// </summary>
        public const string DefaultSchema = @"dbo";

        #endregion

        #region Properties

        /// <summary>
        /// Gets (or privately sets) the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal DbContext Context { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the schemaName this objects resides
        /// </summary>
        public String SchemaName { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the procedureName of the stored procedure
        /// </summary>
        public String ProcedureName { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure"/> class.
        /// </summary>
        public StoredProcedure()
        {
            SetSchemaName(DefaultSchema);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure" /> class
        /// using the procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <exception cref="System.ArgumentNullException">procedureName</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">procedureName</exception>
        public StoredProcedure(string procedureName)
            : this()
        {
            // Validate parameter
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            SetProcedureName(procedureName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure" /> class
        /// using the procedure name and schema name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <exception cref="System.ArgumentNullException">
        /// procedureName
        /// or
        /// schemaName
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// procedureName
        /// or
        /// schemaName
        /// </exception>
        public StoredProcedure(string procedureName, string schemaName)
            : this(procedureName)
        {
            // Validate parameters
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");

            SetSchemaName(schemaName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure" /> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">
        /// procedureName
        /// or
        /// schemaName
        /// or
        /// context
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// procedureName
        /// or
        /// schemaName
        /// </exception>
        public StoredProcedure(string procedureName, string schemaName, DbContext context)
            : this(procedureName, schemaName)
        {
            // Validate parameters
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (context == null) throw new ArgumentNullException("context");

            Context = context;
        }

        #endregion


        #region Methods

        #region Methods Fluent API

        /// <summary>
        /// Sets the procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">procedureName</exception>
        public StoredProcedure SetProcedureName(string procedureName)
        {
            // Validate parameter
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            ProcedureName = procedureName;
            return this;
        }

        /// <summary>
        /// Sets the schema name.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">schemaName</exception>
        public StoredProcedure SetSchemaName(string schemaName)
        {
            // Validate parameter
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");

            SchemaName = schemaName;
            return this;
        }

        #endregion

        #endregion
    }
}
