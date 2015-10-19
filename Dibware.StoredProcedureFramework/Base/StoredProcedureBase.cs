using System;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents the base class that all Stored proedures that have parameters
    /// should inherit from. Contains common stored procedure functionality.
    /// </summary>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <typeparam name="TParameters">The type of the parameters.</typeparam>
    public abstract class StoredProcedureBase<TReturn, TParameters>
        : IStoredProcedure<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class
    {
        #region Fields

        /// <summary>
        /// The object that represents the procedure parameters
        /// </summary>
        private TParameters _parameters;

        /// <summary>
        /// The holds the procedure name
        /// </summary>
        private string _procedureName;

        /// <summary>
        /// The holds the schema name
        /// </summary>
        private string _schemaName;

        #endregion

        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> class.
        ///// Sets the procedure name to match the stored procedure class.
        ///// </summary>
        //private StoredProcedureBase()
        //{
        //    string className = GetType().Name;
        //    SetProcedureName(className);
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(TParameters parameters)
            //: this()
        {
            // Validate arguments
            if (parameters == null) throw new ArgumentNullException("parameters");

            //SetParameters(parameters);
            //SetSchemaName(StoredProcedureDefaults.DefaultSchemaName);

            string procedureName = GetType().Name;
            const string schemaName = StoredProcedureDefaults.DefaultSchemaName;
            InitializeFromParameters(schemaName, procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string procedureName,
            TParameters parameters)
            //: this(parameters)
        {
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (parameters == null) throw new ArgumentNullException("parameters");

            //SetProcedureName(procedureName);
            //SetSchemaName(StoredProcedureDefaults.DefaultSchemaName);

            const string schemaName = StoredProcedureDefaults.DefaultSchemaName;
            InitializeFromParameters(schemaName, procedureName, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> 
        /// class with parameters, schema name and procedure name.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string schemaName,
            string procedureName, TParameters parameters)
            //: this(procedureName, parameters)
        {
            // Validate arguments
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (parameters == null) throw new ArgumentNullException("parameters");

            //SetSchemaName(schemaName);
            InitializeFromParameters(schemaName, procedureName, parameters);
        }

        /// <summary>
        /// Initializes this instance from paremeters. to be called from constructors
        /// </summary>
        private void InitializeFromParameters(string schemaName,
            string procedureName, TParameters parameters)
        {
            // Validate arguments
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");
            if (parameters == null) throw new ArgumentNullException("parameters");

            SetSchemaName(schemaName);
            SetProcedureName(procedureName);
            SetParameters(parameters);

            InitializeFromAttributesInternal();
        }

        /// <summary>
        /// Initializes from attributes internal.
        /// </summary>
        private void InitializeFromAttributesInternal()
        {
            Type type = GetType();

            TrySetProcedureNameFromAttribute(type);
            //TrySetReturnTypeFromAttribute(type);
            TrySetSchemaNameFromAttribute(type);
        }

        #endregion

        #region Members

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public Type ReturnType
        {
            get { return typeof(TReturn); }
        }

        public Type ParametersType
        {
            get { return typeof(TParameters); }
        }

        #endregion

        #region IStoredProcedure<TReturn,TParameter> Members

        //TReturn IStoredProcedure<TReturn, TParameters>.ReturnType
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //TParameters Parameters
        //{
        //    get { throw new NotImplementedException(); }
        //}

        /// <summary>
        /// The object that represents the procedure parameters
        /// </summary>
        public TParameters Parameters
        {
            get { return _parameters; }
        }

        public string ProcedureName
        {
            get { return _procedureName; }
            private set { _procedureName = value; }
        }

        public string SchemaName
        {
            get { return _schemaName; }
            private set { _schemaName = value; }
        }

        #endregion

        #region Methods : Public

        /// <summary>
        /// Ensurefullies the construcuted.
        /// </summary>
        /// <exception cref="System.Exception">
        /// this instance is not fully constrcuted
        /// </exception>
        public void EnsureFullyConstructed()
        {
            if (!IsFullyConstructed())
            {
                string message = ExceptionMessages.StoredProcedureIsNotFullyConstructed;

                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    message);
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
        /// Initializes from attributes.
        /// </summary>
        [ObsoleteAttribute("This method is obsolete. The code it ran is now called " +
                           "internally from this class's constructors. Please remove " +
                           "references to this method call as it will be removed in a" +
                           "later release. ", false)] 
        public void InitializeFromAttributes()
        {
            //InitializeFromAttributesInternal();
            
            ////Type type = typeof(TParameters);
            //Type type = GetType();

            //TrySetProcedureNameFromAttribute(type);
            ////TrySetReturnTypeFromAttribute(type);
            //TrySetSchemaNameFromAttribute(type);
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

        #endregion

        #region Methods : Private

        /// <summary>
        /// Ensures the procedure has a name.
        /// </summary>
        private void EnsureProcedureHasName()
        {
            if (!HasProcedureName())
            {
                string message = ExceptionMessages.StoredProcedureDoesNotHaveName;
                throw ExceptionHelper.CreateStoredProcedureConstructionException(
                    message);
            }
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
        /// Determines whether this instance has a return type.
        /// </summary>
        /// <returns></returns>
        private bool HasReturnType()
        {
            return (ReturnType != null);
        }


        protected void SetParameters(TParameters parameters)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Sets the procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>
        /// This instance
        /// </returns>
        /// <exception cref="System.ArgumentNullException">procedureName</exception>
        public void SetProcedureName(string procedureName)
        {
            // Validate argument
            if (procedureName == null) throw new ArgumentNullException("procedureName");
            if (procedureName == string.Empty) throw new ArgumentOutOfRangeException("procedureName");

            ProcedureName = procedureName;
        }

        /// <summary>
        /// Sets the schema name.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">schemaName</exception>
        public void SetSchemaName(string schemaName)
        {
            // Validate argument
            if (schemaName == null) throw new ArgumentNullException("schemaName");
            if (schemaName == string.Empty) throw new ArgumentOutOfRangeException("schemaName");

            SchemaName = schemaName;
        }

        private void TrySetProcedureNameFromAttribute(Type type)
        {
            var attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
            if (attribute != null)
            {
                SetProcedureName(attribute.Value);
            }
        }

        //private void TrySetReturnTypeFromAttribute(Type type)
        //{
        //    ReturnTypeAttribute attribute = Attribute.GetCustomAttribute(type, typeof(ReturnTypeAttribute)) as ReturnTypeAttribute;
        //    if (attribute != null)
        //    {
        //        SetReturnType(attribute.Returns);
        //    }
        //}

        private void TrySetSchemaNameFromAttribute(Type type)
        {
            SchemaAttribute attribute = Attribute.GetCustomAttribute(type, typeof(SchemaAttribute)) as SchemaAttribute;
            if (attribute != null)
            {
                SetSchemaName(attribute.Value);
            }
        }

        #endregion
    }

}
