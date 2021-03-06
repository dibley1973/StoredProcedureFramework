﻿using System;
using System.Data;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Helpers.Base
{
    /// <summary>
    /// This is the base class which all DbCommand creators for all Sql Functions
    /// should inherit from.
    /// </summary>
    internal abstract class SqlFunctionDbCommandCreatorBase
        : DbCommandCreatorBase
    {
        #region Constructor

        protected SqlFunctionDbCommandCreatorBase(IDbConnection connection)
            : base(connection)
        { }

        #endregion

        #region Private and protected Members

        /// <summary>
        /// Provides the format string for the command's command text. Must be overridden
        /// </summary>
        /// <value>
        /// The function command format.
        /// </value>
        protected abstract string FunctionCommandTextFormat { get; }

        /// <summary>
        /// Sets the command text for the command using the string format set in 
        /// the <see cref="FunctionCommandTextFormat"/> property.
        /// </summary>
        protected override void SetCommandTextForCommand()
        {
            string parameters;
            if (HasParameters)
            {
                var parametersArray = Parameters.Select(parameter => @"@" + parameter.ParameterName).ToArray();
                parameters = string.Join(",", parametersArray);
            }
            else
            {
                parameters = string.Empty;
            }
            string functionCommandText = String.Format(
                FunctionCommandTextFormat,
                CommandText,
                parameters);

            Command.CommandText = functionCommandText;
        }

        #endregion
    }
}