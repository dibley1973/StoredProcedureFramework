﻿using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Resources;
using Dibware.StoredProcedureFramework.StoredProcAttributes;
using System;

namespace Dibware.StoredProcedureFramework
{
    public abstract class StoredProcedureBase<TReturn, TParameters>
        : IStoredProcedure<TReturn, TParameters>
        where TReturn : class
        where TParameters : class
    {
        #region Fields

        /// <summary>
        /// The default schema name
        /// </summary>
        public const string DefaultSchemaName = @"dbo";

        /// <summary>
        /// The dot identifier
        /// </summary>
        public const string DotIdentifier = @".";

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

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}"/> class.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(TParameters parameters)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> class.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string procedureName,
            TParameters parameters)
            : this(parameters)
        {
            _procedureName = procedureName;
            _schemaName = DefaultSchemaName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureBase{TReturn, TParameters}" /> class.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        protected StoredProcedureBase(string schemaName,
            string procedureName, TParameters parameters)
            : this(procedureName, parameters)
        {
            _schemaName = schemaName;
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
        public void EnsureFullyConstrucuted()
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
        public void InitializeFromAttributes()
        {
            //Type type = typeof(TParameters);
            Type type = GetType();

            TrySetProcedureNameFromAttribute(type);
            //TrySetReturnTypeFromAttribute(type);
            TrySetSchemaNameFromAttribute(type);
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
        /// Determines whether [has return type].
        /// </summary>
        /// <returns></returns>
        private bool HasReturnType()
        {
            return (ReturnType != null);
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