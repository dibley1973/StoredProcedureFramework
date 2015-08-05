using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;

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
        /// <summary>
        /// The dot identifier
        /// </summary>
        public const string DotIdentifier = @".";

        #endregion

        #region Properties

        /// <summary>
        /// Command Behavior for
        /// </summary>
        public CommandBehavior CommandBehavior { get; set; }

        /// <summary>
        /// Gets or sets the wait time before terminating the attempt to 
        /// execute this procedure against a command and generating an error.
        /// </summary>
        /// <returns>The time in seconds to wait for the procedure to execute.</returns>
        public int? CommandTimeout { get; set; }

        /// <summary>
        /// Gets (or privately sets) the DBContext.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal DbContext Context { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the procedure Parameters.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public ICollection<SqlParameter> Parameters { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the type of the parameters.
        /// </summary>
        /// <value>
        /// The type of the parameters.
        /// </value>
        public Type ParametersType { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the procedureName of the stored procedure
        /// </summary>
        public String ProcedureName { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the type to return.
        /// </summary>
        /// <value>
        /// The type of the return.
        /// </value>
        public Type ReturnType { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the schemaName this objects resides
        /// </summary>
        public String SchemaName { get; private set; }

        #endregion

        #region Constructors and initialization

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
            // Validate argument
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
            // Validate arguments
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
            // Validate arguments
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (context == null) throw new ArgumentNullException("context");

            Context = context;
        }

        #endregion

        #region Methods



        private void BuildParameters(Type parametersType)
        {
            // Create or clear parameters
            CreateOrClearParameters();

            // Get a list of all properties from the parameters type
            // that are not decorated with the "NotMapped" attribute
            var mappedProperties = parametersType.GetMappedProperties();

            foreach (PropertyInfo propertyInfo in mappedProperties)
            {
                // create parameter and store default name - property name
                SqlParameter sqlParameter = new SqlParameter();

                // Get the name of the parameter. Attributes override the name so try and get this first
                Name nameAttribute = propertyInfo.GetAttribute<StoredProcAttributes.Name>();
                string parameterName = (nameAttribute != null ? nameAttribute.Value : propertyInfo.Name);

                //TODO: complete this below!
                //// save direction (default is input)
                //var dir = propertyInfo.GetAttribute<StoredProcAttributes.Direction>();
                //if (null != dir)
                //    sqlParameter.Direction = dir.Value;

                //// save size
                //var size = propertyInfo.GetAttribute<StoredProcAttributes.Size>();
                //if (null != size)
                //    sqlParameter.Size = size.Value;

                //// save database type of parameter
                //var parmtype = propertyInfo.GetAttribute<StoredProcAttributes.ParameterType>();
                //if (null != parmtype)
                //    sqlParameter.SqlDbType = parmtype.Value;

                //// save user-defined type name
                //var typename = propertyInfo.GetAttribute<StoredProcAttributes.TypeName>();
                //if (null != typename)
                //    sqlParameter.TypeName = typename.Value;

                //// save precision
                //var precision = propertyInfo.GetAttribute<StoredProcAttributes.Precision>();
                //if (null != precision)
                //    sqlParameter.Precision = precision.Value;

                //// save scale
                //var scale = propertyInfo.GetAttribute<StoredProcAttributes.Scale>();
                //if (null != scale)
                //    sqlParameter.Scale = scale.Value;

                // add the parameter
                Parameters.Add(sqlParameter);
            }


            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates or clears the parameters.
        /// </summary>
        private void CreateOrClearParameters()
        {
            if (Parameters == null)
            {
                Parameters = new List<SqlParameter>();
            }
            else
            {
                // Clear the current parameters
                Parameters.Clear();
            }
        }

        /// <summary>
        /// Ensurefullies the construcuted.
        /// </summary>
        /// <exception cref="System.Exception">
        /// this instance is not fully constrcuted
        /// </exception>
        public void EnsureFullyConstrucuted()
        {
            if (!IsFullyConstructed())
            {
                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    ExceptionMessages.StoredProcedureIsNotFullyConstructed);
            }
        }

        /// <summary>
        /// Ensures the procedure has a name.
        /// </summary>
        private void EnsureProcedureHasName()
        {
            if (!HasProcedureName())
            {
                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    ExceptionMessages.StoredProcedureDoesNotHaveName);
            }
        }

        /// <summary>
        /// Gets the name of the two part.
        /// </summary>
        /// <returns></returns>
        public string GetTwoPartName()
        {
            EnsureProcedureHasName();

            return String.Format(
                FormatStrings.TwoPartNameformat, SchemaName, ProcedureName);
        }

        /// <summary>
        /// Determines whether this instance has a valid context.
        /// </summary>
        /// <returns></returns>
        private bool HasValidContext()
        {
            return Context != null;
        }

        /// <summary>
        /// Determines if this instance has a procedure name
        /// </summary>
        /// <returns></returns>
        private bool HasProcedureName()
        {
            return !String.IsNullOrEmpty(ProcedureName);
        }

        /// <summary>
        /// Determines whether [has return type].
        /// </summary>
        /// <returns></returns>
        private bool HasReturnType()
        {
            return (ReturnType != null);
        }

        /// <summary>
        /// Determines if the procedure is fully constructed and in a valid 
        /// state which can be called and executed
        /// </summary>
        /// <returns></returns>
        public bool IsFullyConstructed()
        {
            return HasProcedureName() && HasReturnType();
        }

        #region Methods Fluent API

        /// <summary>
        /// Sets the type of the parameters.
        /// </summary>
        /// <param name="parametersType">Type of the parameters.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">parametersType</exception>
        public StoredProcedure SetParametersType(Type parametersType)
        {
            // Validate argument
            if (parametersType == null) throw new ArgumentNullException("parametersType");

            ParametersType = parametersType;

            BuildParameters(parametersType);

            return this;
        }

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
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            ProcedureName = procedureName;
            return this;
        }

        /// <summary>
        /// Sets the type of the return.
        /// </summary>
        /// <param name="returnType">Type of the return.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">returnType</exception>
        public StoredProcedure SetReturnType(Type returnType)
        {
            // Validate argument
            if (returnType == null) throw new ArgumentNullException("returnType");

            ReturnType = returnType;
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
            // Validate argument
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");

            SchemaName = schemaName;
            return this;
        }

        #endregion

        #endregion
    }
}